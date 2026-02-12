using E_Commerce.App.Domain.Contract;
using E_Commerce.App.Domain.Entities.Product;
using E_Commerce.App.Infrastructre.presistent._Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent
{
    internal class StoreContextIntializer(StoreDbContext dbContext) : IStroreContextIntializer
    {
        public async Task SeedData(string ContenRootpath)
        {
            if (!dbContext.Brands.Any())
            {
                var path = Path.Combine(ContenRootpath, "Seeds", "brands.json");

                var brandData = await File.ReadAllTextAsync(path);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);


                if (brands?.Count > 0)
                {

                    await dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Categories.Any())
            {
                var path = Path.Combine(ContenRootpath, "Seeds", "Categories.json");

                var CategoriesData = await File.ReadAllTextAsync(path);
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);


                if (Categories?.Count > 0)
                {

                    await dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Products.Any())
            {
                var path = Path.Combine(ContenRootpath, "Seeds", "Products.json");

                var ProductsData = await File.ReadAllTextAsync(path);
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);


                if (Products?.Count > 0)
                {

                    await dbContext.Set<Product>().AddRangeAsync(Products);
                    await dbContext.SaveChangesAsync();
                }
            }

        }

        public async Task UpdateDateBase()
        {
            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (PendingMigrations.Any())
                await dbContext.Database.MigrateAsync();
        }
    }
}
