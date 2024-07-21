using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProdcutFeature> ProdcutFeatures { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> TheacerList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           Initializerr.Initialize();
            optionsBuilder.UseSqlServer(Initializerr.Configuration.GetConnectionString("SqlConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasOne(x => x.Category)
            //    .WithMany(x => x.Products)
            //    .HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().Property(x => x.PriceKdv).HasComputedColumnSql("[Price]*[KDV]");
            base.OnModelCreating(modelBuilder);
        }


    }
}
