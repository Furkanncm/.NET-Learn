using Microsoft.EntityFrameworkCore;
using Relations;
using Relations.DAL;
using System.Security.Cryptography.X509Certificates;

Initializerr.Initialize();
using (var context = new AppDbContext())
{
    // _AddCategory(context);
    //context.SaveChanges();
    //OneToOne(context);
    //_AddToMany(context);
    //_removeItem(context);
    //_eagerLoading(context);
    // _explictLoading(context);
    //_tablePerHierarchy(context);
    //_keyless(context);
    //_indexes(context);
    //_clientAndServerSide(context);
    //_innerJoin(context);
    //_leftJoin(context);



    

   


}

void _outerJoin(AppDbContext context)
{
    newJoinItem item = new newJoinItem("None", "None", 0);

    var response = _rightJoin(context, item);
    response.ForEach(e =>
    {
        Console.WriteLine($"{e.CategoryName} => {e.ProductName} : {e.ProductPrice}");
    });

    Console.WriteLine("-------------");
    newJoinItem newJoinItem = new newJoinItem("None", "None", 0);

    var result = _leftJoin(context, newJoinItem);
    result.ForEach(e =>
    {
        Console.WriteLine($"{e.CategoryName} => {e.ProductName} : {e.ProductPrice}");
    });
    Console.WriteLine("---------------------+");
    var outerJoin = response.Union(result).ToList();
    outerJoin.ForEach(e =>
    {
        Console.WriteLine($"{e.CategoryName} => {e.ProductName} : {e.ProductPrice}");
    });

}
List<newJoinItem> _rightJoin(AppDbContext context, newJoinItem item)
{
    var rightJoin = (from c in context.Categories
                     join p in context.Products on c.Id equals p.CategoryId into proList
                     from productList in proList.DefaultIfEmpty()
                     select new newJoinItem  (
                      c.Name,
                      productList != null ? productList.Name : null,
                     productList != null ? (int?)productList.Price : null
                     )).ToList();

    return rightJoin;
}

List<newJoinItem> _leftJoin(AppDbContext context, newJoinItem item)
{
    var leftJoin = (from p in context.Products
                    join c in context.Categories on p.CategoryId equals c.Id into categoryList
                    from category in categoryList.DefaultIfEmpty()
                        select new newJoinItem(
                        category != null ? category.Name : null,
                        p.Name,
                        p.Price
                        )
                    ).ToList();
    return leftJoin;
}

void _innerJoin(AppDbContext context)
{
    var result = context.Categories.Join(context.Products, c => c.Id, p => p.CategoryId, (c, p) => new
    {
        CategoryName = c.Name,
        ProductName = p.Name,
        ProductPrice = p.Price
    }).ToList();

    result.ForEach(e =>
    {
        Console.WriteLine($"{e.CategoryName} => {e.ProductName} : {e.ProductPrice}");
    });
    Console.WriteLine("---------------------------------------------------");

    var result2 = (from c in context.Categories
                   join p in context.Products on c.Id equals p.CategoryId
                   select new
                   {
                       CategoryName = c.Name,
                       ProductName = p.Name,
                       ProductPrice = p.Price
                   }).ToList();

    result2.ForEach(e =>
    {
        Console.WriteLine($"{e.CategoryName} => {e.ProductName} : {e.ProductPrice}");
    });

    Console.WriteLine("---------------------------------------------------");

    var productResult = context.Products
        .Join(context.Categories, p => p.CategoryId, c => c.Id, (p, c) => new
        { c, p })
        .Join(context.ProdcutFeatures, x => x.p.Id, pf => pf.Id, (x, pf) => new
        {
            CategoryName = x.c.Name,
            ProductName = x.p.Name,
            PfName = pf.Name
        }).ToList();

    productResult.ForEach(e =>
    {
        Console.WriteLine($"{e.CategoryName} => {e.ProductName} : {e.PfName}");
    });

    Console.WriteLine("---------------------------------------------------");

    var productResult2 = (from p in context.Products
                          join c in context.Categories on p.CategoryId equals c.Id
                          join pf in context.ProdcutFeatures on p.Id equals pf.Id
                          select new
                          {
                              Value = pf.Value,
                              CategoryName = c.Name,
                              ProductName = p.Name
                          }).ToList();

    productResult2.ForEach(e =>
    {
        Console.WriteLine($"{e.CategoryName} => {e.ProductName} : {e.Value}");
    });
}

void _clientAndServerSide(AppDbContext context)
{

    //context.PhoneNumbers.AddRange(new PhoneNumber
    //{
    //    Name = "Furkan",
    //    Number = "1234567890"
    //},
    //new PhoneNumber
    //{
    //    Name = "Betül",
    //    Number = "9876543210"
    //}
    //); 


    //CLIENT AND SERVER SIDE    
    //ToList metodu dahil oraya kadar Server tarafında çalışır.
    //ToList metodu sonrası Client tarafında çalışır.
    var Numbers = context.PhoneNumbers.ToList().Select(x => new
    {
        numberWithoutZero = formatPhone(x.Number)
    }).ToList();

    Numbers.ForEach(e =>
    {
        Console.WriteLine(e.numberWithoutZero);
    });
    context.SaveChanges();
}

String formatPhone(String value)
{
    return value.Substring(1, value.Length-1);
}

void _indexes (AppDbContext context)
{

    //Selectdeki veriler mesela Nickname özelliği IncludeProperty ile dahil edilmeli.
    //Edilmez ise Index'ın bir anlamı olmaz.Çünkü tabloya bir kez daha sorgu atılır.
    //context.Authors.Where(x => x.Name=="Author 2").Select(x => new { newName = x.Name, newNickname = x.Nickname });
    //context.Products.Where(x => x.Name == "Product 4").Select(x=> new
    //{
    //    // Bu 3 özellik IncludeProperty ile dahil edildiği için sadece name sorgusu ile bu özellikler de çekilir.
    //    newName=x.Name,
    //    newPrice=x.Price,
    //    newBarcode=x.Barcode
    //});

    context.Products.Add(new Product
    {
        Name = "Product 4",
        Price = 100,
        KDV = 18,
        Barcode = 123456,
        DiscountPrice= 50,
        ProdcutFeature = new ProdcutFeature
        {
            Name = "Feature 4",
            Value = "Value 4"
        },
        Category = new Category
        {
            Name = "Category 4"
        }

    });
    context.SaveChanges();
}

//Keyless entitylerde data eklenmez fakat veri çekilebilir ve okunabilir.
void _keyless(AppDbContext context)
{
    //context.Categories.Add(new Category
    //{
    //    Name = "Category 4",
    //    Products = new List<Product>
    //    {
    //        new Product
    //        {
    //            Name = "Product 1",
    //            Price = 100,
    //            KDV = 18,
    //            ProdcutFeature = new ProdcutFeature
    //            {
    //                Name = "Feature 1",
    //                Value = "Value 1"
    //            }
    //        },
    //        new Product
    //        {
    //            Name = "Product 2",
    //            Price = 200,
    //            KDV = 18,
    //            ProdcutFeature = new ProdcutFeature
    //            {
    //                Name = "Feature 2",
    //                Value = "Value 2"
    //            },
    //        } }
    //});
    context.SaveChanges();

    var productFulles = context.ProductFulls.FromSqlRaw(@"select p.Id ""Product_Id"", c.Name ""CategoryName"" ,p.Name ""ProductName"",p.Price,pf.Value from Products p
join Categories c on p.CategoryId=c.Id
join ProdcutFeatures pf on p.Id=pf.Id").ToList();
    Console.WriteLine(productFulles.Count);
    productFulles.ForEach(
        e =>
        {

            Console.WriteLine($"{e.Product_Id} {e.CategoryName} {e.ProductName} {e.Price} {e.Value}");
        });
}
void _tablePerHierarchy(AppDbContext context)
{
    //var employee = context.Employees.Add(new Employee
    //{
    //    Name = "Employee 2",
    //    Surname = "Surname 2",
    //    Salary = 1000


    //});
    //var manager = context.Managers.Add(new Manager
    //{
    //    Name = "Manager 2",
    //    Surname = "Surname 2",
    //    Department = "IT"
    //});

    //context.SaveChanges();


    
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
