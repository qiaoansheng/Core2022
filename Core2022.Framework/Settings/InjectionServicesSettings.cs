using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Settings
{
    public class InjectionServicesSettings
    {
        public InjectionServicesSettings(IConfiguration configuration)
        {
            AssemblyStrings = configuration.GetSection("AssemblyStrings").GetChildren()
                .Select(i => i.Value).ToList();
        }
        public List<string> AssemblyStrings { get; set; }
    }
}
