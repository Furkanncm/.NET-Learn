using Microsoft.EntityFrameworkCore;
using UdemyEFCore.databaseFirst.DAL;

DbContextInitializer.Builder();

using (var _context = new AppDbContext())
{
    var products = await _context.Products.ToListAsync();

    products.ForEach(p =>
    {
        Console.WriteLine($"{p.Id}: {p.Name}:{p.Price}::{p.Stock}");
    });
}