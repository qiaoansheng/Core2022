using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Application.Services.DTO
{
    public class ResponseDto<T>
    {
        public int Status { get; set; }
        public string Info { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
}
