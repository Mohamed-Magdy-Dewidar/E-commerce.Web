using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _context) : IDataSeeding
    {


        public async Task SeedDataAsync()
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await _context.Database.MigrateAsync();
            }

            bool hasChanges = false;

            // Seed brands first as they are foreign keys for products
            if (!_context.ProductBrands.Any()) // No need for AnyAsync() here - explanation below
            {
                var productBrandsData = File.OpenRead(@"../Infrastructure/Persistence/Data/DataSeed/brands.json");
                var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);

                if (productBrands?.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(productBrands);
                    hasChanges = true;
                }


            }

            // Seed types next as they are also foreign keys
            if (!_context.ProductTypes.Any())
            {
                var productTypesData = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/DataSeed/types.json");
                var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);

                if (productTypes?.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(productTypes);
                    hasChanges = true;
                }
            }

            // Finally seed products
            if (!_context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
