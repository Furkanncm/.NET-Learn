using Microsoft.EntityFrameworkCore;
using UdemyEFCore.CodeFirst.DAL;

//Initializer.Initialize(); 
// appsettings.json dosyasında ki connection stringi okuymak için

using (var context = new AppDbContext())
// her AppDbContext nesnesi oluşturulduğunda veritabanı bağlantısı açılır ve nesne işi bitince kapatılır
{
    var Names= await context.MyProducts.Select(p => p.Name).ToListAsync();
    // MyProducts tablosundan Name alanını al ve listeye çevir

    Names.ForEach(n => Console.WriteLine(n));
    // Name'lerin olduğu listeyi ekrana yazdır

}