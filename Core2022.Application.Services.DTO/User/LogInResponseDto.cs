using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Application.Services.DTO.User
{
    public class LogInResponseDto : BaseResponseDto
    {
        public int MyProperty { get; set; }
        public string token { get; set; }
    }
}
