using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration { get; set; }

        static ConfigurationHelper()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static string GetConnectionString(string name) => Configuration.GetConnectionString(name);
    }
}
