using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.databaseFirst.DAL
{
    public class DbContextInitializer  
    {
        public static IConfigurationRoot configuration; //appsettings.json dosyasını okumak için
        public static DbContextOptionsBuilder<AppDbContext> optionsBuilder;  // Veri tabanı bağlantı ayarları


        public static void Builder()
        {
            var JSONFile = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);
            configuration=JSONFile.Build();
            optionsBuilder=new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlConnectionString"));
        }
    }

}
