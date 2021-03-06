using System;
using System.ComponentModel.DataAnnotations;

namespace Core2022.Framework.Entity
{
    public class BaseOrmModel
    {
        [Key]
        public Guid KeyId { get; set; }
        public Guid CreateUserKeyId { get; set; }
        public Guid UpdateUserKeyId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Version { get; set; }
        public bool IsDelete { get; set; }
    }
}
