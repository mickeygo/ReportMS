using System.Messaging;

namespace Gear.Infrastructure.Bus
{
    /// <summary>
    /// 用于构建 MSMQ 总线的参数类
    /// </summary>
    public class MsmqBusOptions
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值，该值指示此 MessageQueue 对来自“消息队列”队列的消息是否有独占接收访问权。
        /// </summary>
        public bool SharedModeDenyReceive { get; set; }

        /// <summary>
        /// 获取或设置一个 <see cref="System.Boolean"/> 值，表示是否要创建并使用一个连接缓存
        /// </summary>
        public bool EnableCache { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示队列的访问模式
        /// </summary>
        public QueueAccessMode QueueAccessMode { get; set; }

        /// <summary>
        /// 获取或设置队列的路径。"." 表示为本地计算机
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 获取或设置一个 Boolean 值，用来指示当发送或接受消息时，内部事务将被使用
        /// </summary>
        public bool UseInternalTransaction { get; set; }

        /// <summary>
        /// 获取或设置一个格式器，用于序列化一个要写入队列的对象或从读取的消息主体中反序列化一个对象
        /// </summary>
        public IMessageFormatter MessageFormatter { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>MSMQBusOptions</c>实例
        /// </summary>
        /// <param name="path">获取或设置队列的路径。"." 表示为本地计算机</param>
        /// <param name="sharedModeDenyReceive">获取或设置一个<see cref="System.Boolean"/>值，该值指示此 MessageQueue 对来自“消息队列”队列的消息是否有独占接收访问权。</param>
        /// <param name="enableCache">获取或设置一个 <see cref="System.Boolean"/> 值，表示是否要创建并使用一个连接缓存</param>
        /// <param name="queueAccessMode">获取或设置一个值，该值指示队列的访问模式</param>
        /// <param name="useInternalTransaction">获取或设置一个 Boolean 值，用来指示当发送或接受消息时，内部事务将被使用</param>
        /// <param name="messageFormatter">获取或设置一个格式器，用于序列化一个要写入队列的对象或从读取的消息主体中反序列化一个对象</param>
        public MsmqBusOptions(string path, bool sharedModeDenyReceive, bool enableCache, QueueAccessMode queueAccessMode, bool useInternalTransaction, IMessageFormatter messageFormatter)
        {
            this.SharedModeDenyReceive = sharedModeDenyReceive;
            this.EnableCache = enableCache;
            this.QueueAccessMode = queueAccessMode;
            this.Path = path;
            this.UseInternalTransaction = useInternalTransaction;
            this.MessageFormatter = messageFormatter;
        }

        /// <summary>
        /// 初始化一个新的<c>MSMQBusOptions</c>实例
        /// </summary>
        /// <param name="path">获取或设置队列的路径。"." 表示为本地计算机</param>
        public MsmqBusOptions(string path)
            : this(path, false, false, QueueAccessMode.SendAndReceive, false, new XmlMessageFormatter())
        { }

        /// <summary>
        /// 初始化一个新的<c>MSMQBusOptions</c>实例
        /// </summary>
        /// <param name="path">获取或设置队列的路径。"." 表示为本地计算机</param>
        /// <param name="useInternalTransaction">获取或设置一个 Boolean 值，用来指示当发送或接受消息时，内部事务将被使用</param>
        public MsmqBusOptions(string path, bool useInternalTransaction)
            : this(path, false, false, QueueAccessMode.SendAndReceive, useInternalTransaction, new XmlMessageFormatter())
        { }

        #endregion
    }
}
