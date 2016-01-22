namespace Gear.Infrastructure.Generators
{
    /// <summary>
    /// 表示身份标识生成器的接口
    /// </summary>
    public interface IIdentityGenerator
    {
        /// <summary>
        /// 生成身份标识
        /// </summary>
        /// <returns></returns>
        object Generate();
    }
}
