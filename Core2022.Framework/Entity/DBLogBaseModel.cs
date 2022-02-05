using System;

namespace Core2022.Framework.Entity
{
    public class DBLogBaseModel
    {
        /// <summary>
        /// 操作keyId
        /// 线程内唯一，用来标识当前线程内进行的操作
        /// </summary>
        public string OperatorKeyId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OptionType { get; set; }

        /// <summary>
        /// 操作人KeyId
        /// </summary>
        public Guid OperatorUserkeyId { get; set; }

        /// <summary>
        /// 操作人名字
        /// </summary>
        public string OperatorUserName { get; set; }

        /// <summary>
        /// 主键KeyId
        /// </summary>
        public Guid PrimaryKeyId { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string CulumnName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 开始时间(开始计时)
        /// </summary>
        public DateTime StartTimer { get; set; }

        /// <summary>
        /// 结束时间(结束计时)
        /// </summary>
        public DateTime EndTimer { get; set; }

        /// <summary>
        /// 费时
        /// </summary>
        public string TakeTime { get; set; }
    }
}
