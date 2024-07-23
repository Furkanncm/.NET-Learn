using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Relations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class AppDbContext:DbContext
    {
        // Base alınan sınıf dbset ile set edilirse sadece base alınan sınıfın tablosu oluşur.
        // Bir sütunda employee veya manager olduğunun verileri tutulur.
       // public DbSet<Person> People { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Productfulls> Productfulls{ get; set; }
        public DbSet<ProductFulls> ProductFulls { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProdcutFeature> ProdcutFeatures { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> TheacerList { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<xd> xds { get; set; }

       

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

            // TPT yaklaşımı: Her sınıfın ayrı tablosu olur.
            // Base'deki ID'ler ile diğer tabloların ID'leri aynı olur.

            //modelBuilder.Entity<Person>().ToTable("Person");
            //modelBuilder.Entity<Employee>().ToTable("Employees");
            //modelBuilder.Entity<Manager>().ToTable("Managers");


            // Owned type sütunlara isim 
            //modelBuilder.Entity<Employee>().OwnsOne(x => x.Person, p =>
            //{
            //    p.Property(x => x.Name).HasColumnName("EmployeeName");
            //    p.Property(x => x.Surname).HasColumnName("EmployeeSurname");
            //    p.Property(x => x.BirthDate).HasColumnName("EmployeeBirthDate");
            //});

            //modelBuilder.Entity<Manager>().OwnsOne(x => x.Person, p =>
            //{
            //    p.Property(x => x.Name).HasColumnName("ManagerName");
            //    p.Property(x => x.Surname).HasColumnName("ManagerSurname");
            //    p.Property(x => x.BirthDate).HasColumnName("ManagerBirthDate");
            //});

            // Keyless entity
            modelBuilder.Entity<ProductFulls>().HasNoKey();
            modelBuilder.Entity<xd>().HasNoKey();
         

            // Entity Property
            //modelBuilder.Entity<Product>().Property(x=>x.Barcode).IsUnicode(false);
            //modelBuilder.Entity<Product>().Ignore(x => x.KDV);
            //modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("NVARCHAR");

            // Index

            //Name'i getiren bir sorgu yazıldığında nickname'i de getirir.
            modelBuilder.Entity<Author>().HasIndex(x => x.Name).IncludeProperties(x => new { x.Nickname });
            modelBuilder.Entity<Author>().HasIndex(x => new { x.Name, x.Nickname });
            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IncludeProperties(x=> new
            {
                newPrice=x.Price,
                newBarcode=x.Barcode
            });

            modelBuilder.Entity<Product>().HasCheckConstraint("DiscountCheck", "[Price]>[DiscountPrice]");
           
            modelBuilder.Entity<Productfulls>().HasNoKey();
            modelBuilder.Entity<Productfulls>().ToView("furkanView");

            base.OnModelCreating(modelBuilder);
        }


    }
}
