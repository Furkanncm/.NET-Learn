using Microsoft.EntityFrameworkCore;
using UdemyEFCore.databaseFirstByScaffold.Models;

using (var context = new UdemyEfcoreDatabaseFirstContext())
{
    var products = await context.Products.ToListAsync();

    products.ForEach(p =>
    {
    Console.WriteLine($"{p.Id}->{p.Name}");
    });
}