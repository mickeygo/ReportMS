namespace Gear.Utility.Serialization
{
    /// <summary>
    /// Json 序列化选项
    /// </summary>
    public class JsonOptions
    {
        #region Private Fields

        private string _defaultDateFormatString = "yyyy-MM-dd HH:mm:ss";
        private int _defaultMaxDepth = 100;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置序列化的日期格式，默认为 "yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public string DateFormatString
        {
            get { return this._defaultDateFormatString; }
            set { this._defaultDateFormatString = value; }
        }

        /// <summary>
        /// 获取或设置读 Json 的最大嵌套，超过限制时会引发异常，默认为 100
        /// </summary>
        public int MaxDepth
        {
            get { return this._defaultMaxDepth; }
            set { this._defaultMaxDepth = value; }
        }

        #endregion
    }
}
