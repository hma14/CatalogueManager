namespace CatalogueManager.Migrations
{
    using Catalogue.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CatalogueManager.Models.CatalogueContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CatalogueManager.Models.CatalogueContext context)
        {
            context.Categories.AddOrUpdate(x => x.Id,
                new Category() { Id = 1, ParentCategoryId = 0, CategoryName = "TABLETS" },
                new Category() { Id = 2, ParentCategoryId = 0, CategoryName = "LAPTOPS" },
                new Category() { Id = 3, ParentCategoryId = 0, CategoryName = "DESKTOPS" },
                new Category() { Id = 4, ParentCategoryId = 0, CategoryName = "APPLE" },
                new Category() { Id = 5, ParentCategoryId = 0, CategoryName = "MONITORS" },
                new Category() { Id = 6, ParentCategoryId = 0, CategoryName = "MEMORY & STORAGE" },
                new Category() { Id = 7, ParentCategoryId = 0, CategoryName = "PRINTERS & SCANNERS" },
                new Category() { Id = 8, ParentCategoryId = 0, CategoryName = "NETWORKING" },
                new Category() { Id = 9, ParentCategoryId = 1, CategoryName = "Apple iPad" },
                new Category() { Id = 10, ParentCategoryId = 2, CategoryName = "Ultrabooks" },
                new Category() { Id = 11, ParentCategoryId = 3, CategoryName = "Apple Computers" }
                );

            context.Products.AddOrUpdate(x => x.Id,
            new Product()
            {
                Id = 1,
                Name = "Apple iPad Air 2",
                Description = "Apple iPad Air 2 64GB With Wi-Fi - Gold",
                Price = 646.99M,
                CategoryId = 11
            },
            new Product()
            {
                Id = 2,
                Name = "ASUS Zenbook",
                Description = "ASUS Zenbook UX305 13.3\" Ultrabook - Gold",
                Price = 899.99M,
                CategoryId = 10
            },
            new Product()
            {
                Id = 3,
                Name = "Apple MacBook Air",
                Description = "Apple MacBook Air 11\" Dual - Core Intel Core i5",
                Price = 1049.99M,
                CategoryId = 9
            }
            );
        }
    }
}
