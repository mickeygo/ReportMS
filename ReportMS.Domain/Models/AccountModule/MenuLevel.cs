namespace ReportMS.Domain.Models.AccountModule
{
    /// <summary>
    /// 菜单目录级别
    /// 目前只设置父级菜单和第一级子菜单
    /// </summary>
    public enum MenuLevel
    {
        /// <summary>
        /// 父菜单
        /// </summary>
        Parent = 0,

        /// <summary>
        /// 子菜单
        /// </summary>
        Children = 1
    }
}
