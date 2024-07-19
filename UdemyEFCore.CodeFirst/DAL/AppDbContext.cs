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
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
            Initializer.Initialize();
            optionsBuilder.UseSqlServer(Initializer.configuration.GetConnectionString("SqlConnectionString"));
        }

       // Fluent Api Yöntemi ile Entity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToTable("Product Table");
            //modelBuilder.Entity<Product>().HasKey(p => p.Id); // Primary Key
            //modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            //modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(200).IsRequired().IsFixedLength();
            //// Maksimum ve minimum 200 karakter olacak ve boş geçilemez
            //base.OnModelCreating(modelBuilder);


            ////1-N ilişki kuruldu.
            //// has ile başlanır ve withone ile devam eder.
            //modelBuilder.Entity<Category>().HasMany(p => p.Products).WithOne(c => c.Category).HasForeignKey(f => f.CategoryId);
            
            //// 1-1 ilişki
            //modelBuilder.Entity<Product>().HasOne(p => p.ProductFeature).WithOne(p => p.Product).
            //    HasForeignKey<ProductFeature>(x => x.ProductId);  
            ////Bire bir ilişki de foreign keyin hangi entityde tutulacağını belirtir. 
            //modelBuilder.Entity<Product>().HasOne(p => p.ProductFeature).WithOne(p => p.Product).HasForeignKey<ProductFeature>(p => p.Id);
           
            //// N-N ilişki
            modelBuilder.Entity<Student>().HasMany(p=>p.Teachers).WithMany(p=>p.Students).UsingEntity(j => j.ToTable("StudentTeacher"));
        }
        public override int SaveChanges()
        {
            ChangeTracker.Entries().ToList().ForEach(e =>
            // ChangeTracker takip edilen yani ramde olan entityleri seçer.
            {
                if (e.Entity is Product product)
                {
                    if (e.State==EntityState.Added|| e.State==EntityState.Modified)
                    {
                        product.CreatedTime=DateTime.Now;
                    }
                }

            });
            return base.SaveChanges();
        }
    }
}
