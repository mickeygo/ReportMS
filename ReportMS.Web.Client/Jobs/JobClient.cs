using System;
using System.Collections.Generic;
using Gear.Utility.Schedule.Jobs;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// Job 定时器
    /// </summary>
    public partial class JobClient : IJobClient
    {
        private static bool registed;
        private static readonly Lazy<List<Type>> subscribers = new Lazy<List<Type>>();
        private readonly IJobClientContext context = new JobClientContext();
        private readonly object _sync = new object();

        #region Ctor

        private JobClient()
        {
            this.Container();
            this.Init();
        }

        #endregion

        #region IJobClient Members

        void IJobClient.Start()
        {
            if (registed)
                return;
            
            foreach (var task in context.Tasks)
                JobFactory.Defalut.AddTask(task.Item1, task.Item2);
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            // Register all jobs that implement the ReportMS.Web.Client.Jobs.ISubScriber interface.
            lock (_sync)
            {
                foreach (var subscriber in subscribers.Value)
                {
                    var job = (ISubScriber) Activator.CreateInstance(subscriber);
                    this.context.AddTask(() => job.Subscribe(), job.Schedule);
                }

                registed = true;
            }
        }

        #endregion

        /// <summary>
        /// 注册订阅者
        /// </summary>
        /// <typeparam name="T">实现了 ISubScriber 接口</typeparam>
        public static void Register<T>() where T : ISubScriber, new()
        {
            subscribers.Value.Add(typeof (T));
        }

        /// <summary>
        /// 启动 Job
        /// </summary>
        public static void Start()
        {
            IJobClient jobClient = new JobClient();
            jobClient.Start();
        }
    }
}
