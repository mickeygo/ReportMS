using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Gear.Infrastructure.Events;

namespace Gear.Infrastructure.Bus
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public class EventBus : DisposableObject, IEventBus
    {
        private readonly Guid id = new Guid();
        private readonly ThreadLocal<Queue<object>> messageQueue = new ThreadLocal<Queue<object>>(() => new Queue<object>());
        private readonly IEventAggregator aggregator;
        private readonly ThreadLocal<bool> committed = new ThreadLocal<bool>(() => true);
        private readonly MethodInfo publishMethod;

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>EventBus</c>实例
        /// </summary>
        /// <param name="aggregator">事件聚合器</param>
        public EventBus(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
            this.publishMethod = (from m in aggregator.GetType().GetMethods()
                let parameters = m.GetParameters()
                let methodName = m.Name
                where methodName == "Publish" &&
                      parameters != null &&
                      parameters.Length == 1
                select m).First();
        }

        #endregion

        #region IBus Members

        /// <summary>
        /// 获取事件总线的全局唯一标识
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
            this.messageQueue.Value.Enqueue(message);
            this.committed.Value = false;
        }

        /// <summary>
        /// 将指定的消息集发布到总线
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息的类型</typeparam>
        /// <param name="messages">要发布的消息集</param>
        public void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IEvent
        {
            foreach (var message in messages)
                this.Publish(message);
        }

        /// <summary>
        /// 清除要提交的消息
        /// </summary>
        public void Clear()
        {
            this.messageQueue.Value.Clear();
            this.committed.Value = true;
        }

        #endregion

        #region 

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示是否支持分布式事务
        /// </summary>
        public bool DistributedTransactionSupported
        {
            get { return false; }
        }

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示是否已提交事件
        /// </summary>
        public bool Committed
        {
            get { return this.committed.Value; }
        }

        /// <summary>
        /// 提交事件
        /// </summary>
        public void Commit()
        {
            while (messageQueue.Value.Any())
            {
                var evnt = messageQueue.Value.Dequeue();
                var evntType = evnt.GetType();
                var method = this.publishMethod.MakeGenericMethod(evntType);
                method.Invoke(aggregator, new [] { evnt });
            }

            this.committed.Value = true;
        }

        /// <summary>
        /// 回滚事件
        /// </summary>
        public void RollBack()
        {
            this.Clear();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.messageQueue.Dispose();
                this.committed.Dispose();
            }
        }
    }
}
