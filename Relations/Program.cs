using Microsoft.EntityFrameworkCore;
using Relations;
using Relations.DAL;

Initializerr.Initialize();
using (var context= new AppDbContext())
{
    // _AddCategory(context);
    //context.SaveChanges();
    //OneToOne(context);
    //_AddToMany(context);
    //_removeItem(context);
    //_eagerLoading(context);
    _explictLoading(context);

}



// ilk başta değilde sonradan eklenen bir tablo için kullanılır.
void _explictLoading(AppDbContext context)
{
    var category =context.Categories.First(e => e.Id == 3);
    //
    //
    //
    if (true)
    {
        // 1-N ilişkide N olan tablodan veri çekmek için kullanılır.
        // Bir kategorinin Birden fazla ürünü olduğu için Collection kullanılır.
        context.Entry(category).Collection(e => e.Products).Load();
        category.Products.ForEach(e =>
        {
            Console.WriteLine(e.Name);
        });
    }

    var product=context.Products.First(e => e.Id == 4);

    if (true)
    {
        // 1-1 ilişkide 1 olan tablodan veri çekmek için kullanılır.
        context.Entry(product).Reference(e=>e.ProdcutFeature).Load();

        Console.WriteLine(product.ProdcutFeature.Name);
    }
}

// Include ile birbirine bağlı olan tablolardan istediklerimizi çekebiliriz.
void _eagerLoading(AppDbContext context)
{
    //var category = context.Categories.Add(new Category
    //{

    //    Name = "Category 2",
    //    Products= new List<Product>
    // {
    //     new Product
    //     {
    //         Name="Product 2",
    //         Price=100,
    //         KDV=18,
    //         ProdcutFeature = new ProdcutFeature
    //         {
    //             Name="Feature 2",
    //             Value="Value 2"
    //         }
    //     },
    //     new Product
    //     {
    //         Name="Product 3",
    //         Price=200,
    //         KDV=18,
    //         ProdcutFeature = new ProdcutFeature
    //         {
    //             Name="Feature 3",
    //             Value="Value 3"
    //         },


    //     }


    // }

    //});
    //context.SaveChanges();

    var categoreyWithProducts = context.Categories.Include(c=>c.Products).ThenInclude(p=>p.ProdcutFeature).First(e=>e.Id==3);
    //if (categoreyWithProducts!=null)
    //{
    //    foreach (var item in categoreyWithProducts.Products)
    //    {
    //        Console.WriteLine($"{item.Name}: {item.CreatedTime}");
    //    }

    foreach (var pfItem in categoreyWithProducts.Products)
    // Select ile ProductFeature tablosundan veri çekilir.
    {
        Console.WriteLine($"{pfItem.Name}: {pfItem.ProdcutFeature.Value}");
    }

    //    categoreyWithProducts.Products.ForEach(e =>
    //    {
    //        Console.WriteLine(e.ProdcutFeature.Name); ;
    //    });
    //}
    //else
    //{
    //    Console.WriteLine("Category not found");
    //}

    //var prodcutFeature = context.ProdcutFeatures.Include(e => e.Product).ThenInclude(e => e.Category).First();

    //Console.WriteLine(prodcutFeature.Name);
    //Console.WriteLine(prodcutFeature.Product.Name);
    //Console.WriteLine(prodcutFeature.Product.Category.Name);

    //foreach (var item in context.Categories.Include(e => e.Products).ThenInclude(e => e.ProdcutFeature).ToList())
    //{
    //    Console.WriteLine(item.Name);
    //    foreach (var product in item.Products)
    //    {
    //        Console.WriteLine(product.Name);
    //        Console.WriteLine(product.ProdcutFeature.Name);
    //    }
    //}
    //var product = context.Products.Include(c=>c.Category).Include(pf=>pf.ProdcutFeature).First(e=>e.Id==4);
    //Console.WriteLine(product.Name);

    // Include ile birbirine bağlı olan tablolardan istediklerimizi çekebiliriz.
    //foreach (var item in context.Products.Include(c => c.Category).ToList())
    //{
    //    Console.WriteLine(item.Name);
    //    Console.WriteLine(item.Category.Name);
    //}
}
void _removeItem(AppDbContext context)
{
    context.Categories.Remove(context.Categories.First(e => e.Name == "Category 2"));
    context.SaveChanges();
}
void _AddToMany(AppDbContext context)
{
    //context.Students.Add(new Student
    //{
    //    Name = "Student 1",
    //    TeacherList = new List<Teacher>
    //    {
    //        new Teacher
    //        {
    //            Name = "Teacher 1"
    //        },
    //        new Teacher
    //        {
    //            Name = "Teacher 2"
    //        }
    //    }

    //});

    //context.TheacerList.Add(new Teacher
    //{
    //    Name="Teacher 3",
    //    Students = new List<Student>
    //    {
    //        new Student
    //        {
    //            Name = "Student 2"
    //        },
    //        new Student
    //        {
    //            Name = "Student 3"
    //        }
    //    }
    //});
    var student = context.Students.First(e => e.Name=="Student 1");
    if (student != null)
    {
        student.TeacherList.AddRange(

     new List<Teacher>
     {
            new Teacher
            {
                Name = "Teacher 4"
            },
            new Teacher
            {
                Name = "Teacher 5"
            }
     }
     );

    }

    context.SaveChanges();
    Console.WriteLine("Kaydedildi");
}
void OneToOne(AppDbContext context)
{
    context.Authors.Add(new Author
    {
        Name = "Author 2",
    }
    );
    context.SaveChanges();
    var author = context.Authors.First(e => e.Name=="Author 2");

    var book = context.Books.Add(new Book
    {
        Name = "Book 2",
        PageCount = 200,
        Authors = author
    });
    context.SaveChanges();
}
void _AddItem(AppDbContext context , Category category)
{
    var existingProduct = context.Products.FirstOrDefault(e=>e.Name=="Product 4");
    if(existingProduct==null)
    {
        var product = new Product
        {
            Name = "Product 4",
            Category = category
        };
        context.Products.Add(product);
        context.SaveChanges();
        Console.WriteLine("Product Added");
    }
    else
    {
        Console.WriteLine("Product is already taken");
    }
}
void _AddCategory(AppDbContext context)
{
    var existingCategory = context.Categories.FirstOrDefault(x => x.Name == "Category 2");
    if (existingCategory == null)
    {
        var category = new Category
        {
            Name = "Category 2"
        };
        context.Categories.Add(category);

        _AddItem(context, category);
        context.SaveChanges();
    }
    else
    {
        Console.WriteLine("Category is already taken");
        _AddItem(context, existingCategory);
    }
    context.SaveChanges();
}
