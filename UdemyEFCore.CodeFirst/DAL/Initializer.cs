using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    public class Initializer
    {
        public static IConfigurationRoot configuration; // appsettings.json dosyasını okumak için
        public static void Initialize()
        {
            configuration = new ConfigurationBuilder() 
                // Frameworkun ConfigurationBuilder sınıfı ile appsettings.json dosyasının nerede olduğunu bulup okuma işlemini yapıyoruz.
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",optional:true,reloadOnChange:true)
                .Build();
        }
    }
}
