using Microsoft.Extensions.Configuration;
using System.IO;

namespace Relations
{
    public class Initializerr
    {
        public static IConfigurationRoot Configuration;

        public static void Initialize()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}
