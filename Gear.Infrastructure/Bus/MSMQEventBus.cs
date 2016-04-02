using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using Gear.Infrastructure.Events;

namespace Gear.Infrastructure.Bus
{
    /// <summary>
    /// Message Queue 消息队列事件总线
    /// </summary>
    public class MSMQEventBus : DisposableObject, IEventBus
    {
        #region Private Fields
        private readonly Guid id = Guid.NewGuid();
        private volatile bool committed = true;
        private readonly bool useInternalTransaction;
        private readonly MessageQueue messageQueue;
        private readonly object sync = new object();
        private readonly Queue<object> mockQueue = new Queue<object>();
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>MSMQEventBus</c>实例
        /// </summary>
        /// <param name="path">消息队列路径</param>
        public MSMQEventBus(string path)
        {
            var options = new MSMQBusOptions(path);
            this.messageQueue = new MessageQueue(path,
                options.SharedModeDenyReceive,
                options.EnableCache, options.QueueAccessMode)
            {
                Formatter = options.MessageFormatter
            };
            this.useInternalTransaction = options.UseInternalTransaction && messageQueue.Transactional;
        }

        /// <summary>
        /// 初始化一个新的<c>MSMQEventBus</c>实例
        /// </summary>
        /// <param name="path">消息队列路径</param>
        /// <param name="useInternalTransaction">是否使用内部事务</param>
        public MSMQEventBus(string path, bool useInternalTransaction)
        {
            var options = new MSMQBusOptions(path, useInternalTransaction);
            this.messageQueue = new MessageQueue(path,
                options.SharedModeDenyReceive,
                options.EnableCache, options.QueueAccessMode)
            {
                Formatter = options.MessageFormatter
            };
            this.useInternalTransaction = options.UseInternalTransaction && messageQueue.Transactional;
        }

        /// <summary>
        /// 初始化一个新的<c>MSMQEventBus</c>实例
        /// </summary>
        /// <param name="options">构建消息队列总线的选项实例</param>
        public MSMQEventBus(MSMQBusOptions options)
        {
            this.messageQueue = new MessageQueue(options.Path,
                options.SharedModeDenyReceive,
                options.EnableCache, options.QueueAccessMode)
            {
                Formatter = options.MessageFormatter
            };
            this.useInternalTransaction = options.UseInternalTransaction && messageQueue.Transactional;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">要发送的消息对象</param>
        /// <param name="transaction">消息队列事务</param>
        private void SendMessage<TMessage>(TMessage message, MessageQueueTransaction transaction)
           where TMessage : class, IEvent
        {
            var msmqMessage = new Message(message);
            if (this.useInternalTransaction)
            {
                if (transaction == null)
                    throw new ArgumentNullException("transaction");

                messageQueue.Send(msmqMessage, transaction);
            }
            else
            {
                messageQueue.Send(msmqMessage, MessageQueueTransactionType.Automatic);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">要发送的消息对象</param>
        /// <param name="transaction">消息队列事务</param>
        private void SendMessage(object message, MessageQueueTransaction transaction)
        {
            var sendMessageMethod = (from m in this.GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                                     let methodName = m.Name
                                     let generic = m.IsGenericMethod
                                     where generic &&
                                           methodName == "SendMessage"
                                     select m).First();
            var evntType = message.GetType();
            sendMessageMethod.MakeGenericMethod(evntType).Invoke(this, new [] { message, transaction });
        }

        #endregion

        #region IBus Members

        /// <summary>
        /// 获取总线的全局唯一标识
        /// </summary>
        public Guid ID
        {
            get { return this.id; }
        }

        /// <summary>
        /// 将指定的消息发布到总线
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息的类型</typeparam>
        /// <param name="message">要发布的消息</param>
        public void Publish<TMessage>(TMessage message) where TMessage : class, IEvent
        {
            lock (this.sync)
            {
                this.mockQueue.Enqueue(message);
                this.committed = false;
            }
        }

        /// <summary>
        /// 将指定的消息集合发布到总线
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息的类型</typeparam>
        /// <param name="messages">要发布的消息</param>
        public void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IEvent
        {
            lock (this.sync)
            {
                foreach (var message in messages)
                    this.mockQueue.Enqueue(message);
                this.committed = false;
            }
        }

        /// <summary>
        /// 清除要提交的消息
        /// </summary>
        public void Clear()
        {
            lock (this.sync)
            {
                this.mockQueue.Clear();
            }
        }

        #endregion

        /// <summary>
        ///  获取一个<see cref="System.Boolean"/>值，表示是否支持分布式事务
        /// </summary>
        public bool DistributedTransactionSupported
        {
            get { return true; }
        }

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示是否已提交事件
        /// </summary>
        public bool Committed
        {
            get { return this.committed; }
        }

        /// <summary>
        /// 提交事件
        /// </summary>
        public void Commit()
        {
            lock (this.sync)
            {
                if (this.useInternalTransaction)
                {
                    using (var transaction = new MessageQueueTransaction())
                    {
                        try
                        {
                            transaction.Begin();
                            while (this.mockQueue.Any())
                            {
                                var message = this.mockQueue.Dequeue();
                                this.SendMessage(message, transaction);
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Abort();
                            throw;
                        }
                    }
                }
                else
                {
                    while (this.mockQueue.Any())
                    {
                        var message = this.mockQueue.Dequeue();
                        this.SendMessage(message, null);
                    }
                }
            }
        }

        /// <summary>
        /// 回滚事件
        /// </summary>
        public void Rollback()
        {
            this.committed = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.messageQueue != null)
                {
                    this.messageQueue.Close();
                    this.messageQueue.Dispose();
                }
            }
        }
    }
}
