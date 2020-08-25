namespace FIT5032_W04_CodeFirst.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new FIT5032_W04_CodeFirst())
            {
                var units = new Unit[3];
                units[0] = new Unit { Name = "FIT5032", Description = "Web Application Development" };
                units[1] = new Unit { Name = "FIT9131", Description = "Programming with Java" };
                units[2] = new Unit { Name = "FIT9132", Description = "Database" };
                for (int i = 0; i < units.Length; i++)
                {
                    db.Units.Add(units[i]);
                }

                var student = new Student { FirstName = "Christian", LastName = "Pan", Units = units };
                db.Students.Add(student);
                db.SaveChanges();
                Console.WriteLine("Data has been saved to database");
            }
        }
    }
    public class FIT5032_W04_CodeFirst : DbContext
    {
        // Your context has been configured to use a 'FIT5032_W04_CodeFirst' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FIT5032_W04_CodeFirst.Models.FIT5032_W04_CodeFirst' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FIT5032_W04_CodeFirst' 
        // connection string in the application configuration file.
        public FIT5032_W04_CodeFirst()
            : base("name=FIT5032_W04_CodeFirst")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Unit> Units { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Unit[] Units { get; set; }
    }

    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}