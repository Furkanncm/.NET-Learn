using Example.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new YalinStajContext())
{
    var exp = await context.ExampleTables.ToListAsync();

    exp.ForEach(e =>
    {
        Console.WriteLine($"{e.Id}->{e.Name} {e.SurName}");
    });

}