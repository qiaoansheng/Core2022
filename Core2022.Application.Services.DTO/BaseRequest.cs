using System;

namespace Core2022.Application.Services.DTO
{
    public class BaseRequest
    {
        public Guid KeyId { get; set; }
        public Guid CreateUserKeyId { get; set; }
        public Guid UpdateUserKeyId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Version { get; set; }
        public bool IsDelete { get; set; }
    }
}
