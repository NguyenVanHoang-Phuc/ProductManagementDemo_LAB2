using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class MyStoreDBContext : DbContext
    {
        
            public MyStoreDBContext() { }
            public DbSet<AccountMember> AccountMembers { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStoreDB"));
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Entity AccountMember
                modelBuilder.Entity<AccountMember>()
                    .Property(member => member.MemberPassword)
                    .IsRequired();

                modelBuilder.Entity<AccountMember>()
                    .Property(member => member.FullName)
                    .IsRequired();

                modelBuilder.Entity<AccountMember>()
                    .Property(member => member.MemberRole)
                    .IsRequired();

                modelBuilder.Entity<AccountMember>().HasData(
                        new AccountMember { MemberID = "PS0001", MemberPassword = "@1", FullName = "Administrator", EmailAddress = "admin@CompanyName.com", MemberRole = 1 },
                        new AccountMember { MemberID = "PS0002", MemberPassword = "@2", FullName = "Staff", EmailAddress = "staff@CompanyName.com", MemberRole = 2 },
                        new AccountMember { MemberID = "PS0003", MemberPassword = "@3", FullName = "Member 1", EmailAddress = "member1@CompanyName.com", MemberRole = 3 },
                        new AccountMember { MemberID = "PS0004", MemberPassword = "@3", FullName = "Member 2", EmailAddress = "member2@CompanyName.com", MemberRole = 3 }
                    );

                // Entity Category
                modelBuilder.Entity<Category>()
                    .Property(category => category.CategoryName)
                    .IsRequired();

                modelBuilder.Entity<Category>().HasData
                    (
                        new Category { CategoryID = 1, CategoryName = "Beverages" },
                        new Category { CategoryID = 2, CategoryName = "Condiments" },
                        new Category { CategoryID = 3, CategoryName = "Confections" },
                        new Category { CategoryID = 4, CategoryName = "Dairy Products" },
                        new Category { CategoryID = 5, CategoryName = "Grains/Cereals" },
                        new Category { CategoryID = 6, CategoryName = "Meat/Poultry" },
                        new Category { CategoryID = 7, CategoryName = "Produce" },
                        new Category { CategoryID = 8, CategoryName = "Seafood" }
                    );

                // Entity Product
                modelBuilder.Entity<Product>()
                    .Property(product => product.ProductName)
                    .IsRequired();

                modelBuilder.Entity<Product>()
                    .Property(product => product.UnitsInStock)
                    .IsRequired();

                modelBuilder.Entity<Product>()
                    .Property(product => product.UnitPrice)
                    .IsRequired()
                    .HasColumnType("money");

                modelBuilder.Entity<Product>()
                    .HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryID);

                modelBuilder.Entity<Product>().HasData(
                        new Product { ProductID = 1, ProductName = "Chai", CategoryID = 3, UnitsInStock = 12, UnitPrice = 18.00m },
                        new Product { ProductID = 2, ProductName = "Chang", CategoryID = 1, UnitsInStock = 23, UnitPrice = 19.00m },
                        new Product { ProductID = 3, ProductName = "Aniseed Syrup", CategoryID = 2, UnitsInStock = 23, UnitPrice = 10.00m },
                        new Product { ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", CategoryID = 2, UnitsInStock = 34, UnitPrice = 22.00m },
                        new Product { ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", CategoryID = 2, UnitsInStock = 45, UnitPrice = 21.35m },
                        new Product { ProductID = 6, ProductName = "Grandma's Boysenberry Spread", CategoryID = 2, UnitsInStock = 21, UnitPrice = 25.00m },
                        new Product { ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", CategoryID = 7, UnitsInStock = 22, UnitPrice = 30.00m },
                        new Product { ProductID = 8, ProductName = "Northwoods Cranberry Sauce", CategoryID = 2, UnitsInStock = 10, UnitPrice = 40.00m },
                        new Product { ProductID = 9, ProductName = "Mishi Kobe Niku", CategoryID = 6, UnitsInStock = 12, UnitPrice = 97.00m },
                        new Product { ProductID = 10, ProductName = "Ikura", CategoryID = 8, UnitsInStock = 13, UnitPrice = 31.00m }
                    );
            }
        
    }
}
