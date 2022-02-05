namespace Core2022.Framework.Entity
{
    public class DBLogUpdateModel : DBLogBaseModel
    {
        /// <summary>
        /// 原始值
        /// </summary>
        public object OriginalValue { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        public object CurrentValue { get; set; }

    }
}
