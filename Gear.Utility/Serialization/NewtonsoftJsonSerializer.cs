using Newtonsoft.Json;

namespace Gear.Utility.Serialization
{
    /// <summary>
    /// 基于 NewtonsoftJson 的 Json 序列器
    /// </summary>
    public class NewtonsoftJsonSerializer
    {
        #region Private Fields

        private readonly JsonSerializerSettings settings;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>NewtonsoftJson</c>实例。
        /// 使用默认的 JsonOptions 选项设置
        /// </summary>
        public NewtonsoftJsonSerializer()
        {
            var options = new JsonOptions();
            this.settings = new JsonSerializerSettings
            {
                DateFormatString = options.DateFormatString,
                MaxDepth = options.MaxDepth
            };
        }

        /// <summary>
        /// 初始化一个新的<c>NewtonsoftJson</c>实例。
        /// </summary>
        /// <param name="options">Json 序列化选项</param>
        public NewtonsoftJsonSerializer(JsonOptions options)
        {
            this.settings = new JsonSerializerSettings
            {
                DateFormatString = options.DateFormatString,
                MaxDepth = options.MaxDepth
            };
        }

        /// <summary>
        /// 初始化一个新的<c>NewtonsoftJson</c>实例。
        /// </summary>
        /// <param name="settings">Json 序列化设置</param>
        public NewtonsoftJsonSerializer(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 虚拟对象
        /// </summary>
        /// <param name="data">要虚拟的字符串</param>
        /// <returns>Json 格式的字符串</returns>
        public string Serialize(object data)
        {
            if (data == null)
                return null;
            
            if (this.settings != null)
                return JsonConvert.SerializeObject(data, this.settings);

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">要反序列化的对象类型</typeparam>
        /// <param name="value">要反序列化的字符串值</param>
        /// <returns>要反序列化的对象实例</returns>
        public T Deserialize<T>(string value)
        {
            if (this.settings != null)
                return JsonConvert.DeserializeObject<T>(value, this.settings);

            return JsonConvert.DeserializeObject<T>(value);
        }

        #endregion
    }
}
