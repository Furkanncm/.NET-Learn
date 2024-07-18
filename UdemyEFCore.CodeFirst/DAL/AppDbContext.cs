using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> MyProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
            Initializer.Initialize();
            optionsBuilder.UseSqlServer(Initializer.configuration.GetConnectionString("SqlConnectionString"));
        }

        public override int SaveChanges()
        {
            ChangeTracker.Entries().ToList().ForEach(e =>
            {
                if (e.Entity is Product product)
                {
                    if (e.State==EntityState.Added)
                    {
                        product.CreatedTime=DateTime.Now;
                    }
                }

            });
            return base.SaveChanges();
        }
    }
}
