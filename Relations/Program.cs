using Relations;
using Relations.DAL;

Initializerr.Initialize();
using (var context= new AppDbContext())
{
    // _AddCategory(context);
    //OneToOne(context);
    //_AddToMany(context);
    //_removeItem(context);
    
        
   
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
