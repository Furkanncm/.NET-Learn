using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using UdemyEFCore.CodeFirst.DAL;

//Initializer.Initialize(); 
// appsettings.json dosyasında ki connection stringi okuymak için

using (var context = new AppDbContext())
// her AppDbContext nesnesi oluşturulduğunda veritabanı bağlantısı açılır ve nesne işi bitince kapatılır
{
    _OneToManyAdd(context);
    //await _ChangeTrackerAsync(context);
    //await _WriteAllProductsAsync(context);
    // await  _ProductAddAsync(context);
    //await _ProductRemoveAsync(context);
    //await _A ddProductAsync(context);
    // await _ChangeTrackerWithAddProduct(context);
    //await _UpdateProduct(context);
    //await _DbSetProp(context);

}


void _existanceCheck(AppDbContext context, Category category)
{
    var existingProducts= context.MyProducts.FirstOrDefault(p=>p.Name=="Steel Series");

    if(existingProducts == null) // ürün yoksa ekle varsa kullanıcıyı bildir.
    {
        category.Products.Add(new Product
        {
            Name= "Steel Series",
            Description="Steel Series Mouse",
            Price=100,
            Barcode=123456789,
            Stock=100
        });
    }
    else
    {
        Console.WriteLine("Logitech Mouse ürünü zaten var");
    }

}
void _OneToManyAdd(AppDbContext context)
{ //var telefonCategory = new Category {Name= "Telefon", Description="Telefon Kategorisi"};
    
    var existingCategory= context.Categories.FirstOrDefault(c=>c.Name=="Mouse");
    if (existingCategory == null)
    {
        var mouseCategory = new Category
        {
            Name= "Mouse",
            Description="Mouse Kategorisi"
        };
        context.Categories.Add(mouseCategory);
        _existanceCheck(context,mouseCategory);
        
        //Console.WriteLine("if e girdi");
    }
    else
    {
       _existanceCheck(context,existingCategory);
       // Console.WriteLine("else girdi");
    }
    
    context.SaveChanges();

    
    context.SaveChanges();
    //var product = new Product
    // {
    //     Name = "Samsung S10",
    //     Description = "2019 Model Telefon",
    //     Price = 8000,
    //     Stock = 100,
    //     Barcode = 123456789,
    //     Category=telefonCategory
    // };

    // telefonCategory.Products.Add(new Product()
    // {
    //     Name = "Samsung S21",
    //     Description = "2021 Model Telefon",
    //     Price = 8000,
    //     Stock = 100,
    //     Barcode = 123456789,

    // });

    //laptopCategory.Products.Add(new Product()
    //{
    //    Name= "Monster Abra A7",
    //    Description="Monster Laptop",
    //    Price=20000,
    //    Stock=50,
    //    Barcode=123933789

    //});

    //laptopCategory.Products.Add(new Product()
    //{
    //    Name="Monster Abra A6",
    //    Description="Monster Laptop",
    //    Price=15000,
    //    Stock=50,
    //    Barcode=123933789


    //});

    //context.Categories.Add(laptopCategory);
    //  context.Categories.Add(telefonCategory);

    // context.MyProducts.Add(product);


}
async Task _ChangeTrackerAsync(AppDbContext context)
{
    await context.MyProducts.ToListAsync(); // MyProducts tablosundaki tüm verileri getir

    context.ChangeTracker.Entries().ToList().ForEach(e => // ChangeTracker ile takip edilen tüm entityleri döngüye al
    {
        if (e.Entity is Product MyProduct) // Eğer entity Product ise MyProduct'a ata
        {
            MyProduct.Price=10000;
            context.SaveChanges(); // Veri tabanını güncelle
            Console.WriteLine($"{MyProduct.Id}->{MyProduct.Name} : {MyProduct.Description} : State -> {e.State}");
        }
    });
}
async Task _WriteAllProductsAsync(AppDbContext context)
{
    var products = await context.MyProducts.AsNoTracking().ToListAsync(); 
    // AsNoTracking -> Veritabanından sadece veri çekilirken takip etme memoryi verimli kullan.
    // Düzenleme silme vs işlem olmayacaksa kullanılır.

    foreach (var product in products)
    {
        var state = context.Entry(product).State;
        Console.WriteLine($"{product.Id}->{product.Name} : {product.Description} : State -> {state}");
    }



    var Names = await context.MyProducts.Select(p => p.Name).ToListAsync();
    // MyProducts tablosundan Name alanını al ve listeye çevir

    Names.ForEach(n => Console.WriteLine(n));
    // Name'lerin olduğu listeyi ekrana yazdır
}
async Task _ProductAddAsync(AppDbContext context)
{
    var newProduct = new Product
    {
        Name = "Samsung S5",
        Description = "2015 Model Telefon",
        Price = 2000,
        Stock = 50,
        Barcode= 123456789
    };
    Console.WriteLine(context.Entry(newProduct).State); // State -> Detached Henüz memoryde değil ve veritabanında yok

    await context.MyProducts.AddAsync(newProduct);

    Console.WriteLine(context.Entry(newProduct).State); // State -> Added memoryde var ama veritabanında yok

    await context.SaveChangesAsync();

    Console.WriteLine(context.Entry(newProduct).State); // State -> Unchanged Veri tabanıyla eşleşti

}
async Task _ProductRemoveAsync(AppDbContext context)
{
    var specialProduct = await context.MyProducts.FirstAsync();
    Console.WriteLine(context.Entry(specialProduct).State); // State -> Unchanged
    specialProduct.Price = 1000;
    Console.WriteLine(context.Entry(specialProduct).State); // State -> Modified
    await context.SaveChangesAsync();
    Console.WriteLine(context.Entry(specialProduct).State); // State -> Unchanged veri tabanındaki veri ile eşleştiği için
    context.Remove(specialProduct);
    Console.WriteLine(context.Entry(specialProduct).State); // State -> Deleted
    await context.SaveChangesAsync();
    Console.WriteLine(context.Entry(specialProduct).State); // State -> Detached veritabanından silindiği için


}
async Task _AddProductAsync(AppDbContext context)
{
    var newProduct = new Product
    {
        Name = "Samsung S6",
        Description = "2016 Model Telefon",
        Price = 3000,
        Stock = 100,
        Barcode = 123456789,
        CreatedTime= DateTime.Now
    };

    context.MyProducts.Add(newProduct);
    context.SaveChanges();
}
async Task _ChangeTrackerWithAddProduct(AppDbContext context)
{
    var product = new Product
    {
        Name="Samsung S8",
        Description="2018 Model Telefon",
        Price=5000,
        Stock=400,
        Barcode=11356789
    };

    await context.MyProducts.AddAsync(product);
    context.ChangeTracker.Entries().ToList().ForEach(e =>
    {
        if(e.Entity is Product p)
        {
            Console.WriteLine($"{p.Id} -> {p.Name}: {p.Description} {p.Price} {p.CreatedTime}");
        }
    });

    context.SaveChanges();

}
async Task _UpdateProduct(AppDbContext context)
{
    var value= context.MyProducts.Update(
        new Product
        {
            Id=10,
            Name="Samsung S20",
            Description="2020 Model Telefon",
            Price=16000,
            Barcode=123456789,
            Stock=5200
        }
        );
    Console.WriteLine($"{value.Entity.Name}");
    context.SaveChanges();
}
async Task _DbSetProp(AppDbContext context)
{
    var product1 = context.MyProducts.First();
    Console.WriteLine(product1.Name);
    var message = context.Entry(product1).State;
    Console.WriteLine(message);

    var products = await context.MyProducts.Where(e => e.Price > 2000).ToListAsync(); 
    // Where filtreleme yapar geriye liste döndürür.
    foreach (var p in products)
    {
        Console.WriteLine(p.Description);
    }

    var product2 = context.MyProducts.SingleAsync(e => e.Id==6);
    // SingleAsync sorgusu sonucunda tek bir veri döner. Bulamadığında hata fırlatsın istersek Single kullanırız.
    Console.WriteLine(product2.Result.Name);
    var product3 = context.MyProducts.Find(6);
    Console.WriteLine(product3.Name);
    // Find metodu o tablonun primary keyine göre veri getirir ekstradan belirmeye gerek yoktur. Bulamazsa null döndürür.


}