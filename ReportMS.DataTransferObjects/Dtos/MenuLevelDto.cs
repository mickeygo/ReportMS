namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 菜单目录级别 Dto 对象.
    /// 目前只设置父级菜单和第一级子菜单
    /// </summary>
    public enum MenuLevelDto
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
