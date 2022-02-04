using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Entity
{
    public class BaseOrmModel
    {
        [Key]
        public Guid KeyId { get; set; }
    }
}
