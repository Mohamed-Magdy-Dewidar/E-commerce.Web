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


        public void SeedData()
        {

            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            // must seed the data of brands and types firstly as they are foreign keys for the products

            if (!_context.ProductBrands.Any())
            {
                var ProductBrandsData = File.ReadAllText(@"../Infrastructure/Persistence/Data/DataSeed/brands.json");
                var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);
                if (ProductBrands != null && ProductBrands.Count > 0)
                {
                    _context.ProductBrands.AddRange(ProductBrands);
                }
            }
            if (!_context.ProductTypes.Any())
            {
                var ProductTypesData = File.ReadAllText(@"../Infrastructure/Persistence/Data/DataSeed/types.json");
                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                if (ProductTypes != null && ProductTypes.Count > 0)
                {
                    _context.ProductTypes.AddRange(ProductTypes);
                }
            }
            if (!_context.Products.Any())
            {
                var ProductsData = File.ReadAllText(@"../Infrastructure/Persistence/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products != null && Products.Count > 0)
                {
                    _context.Products.AddRange(Products);
                }
            }


            _context.SaveChanges();




        }

        //public void SeedData()
        //{
        //    if (_context.Database.GetPendingMigrations().Any())
        //    {
        //        _context.Database.Migrate();
        //    }

        //    // Base path to your seed data
        //    var seedDataPath = Path.Combine(Directory.GetCurrentDirectory(),
        //                                  "Infrastructure", "Persistence", "Data", "SeedData");

        //    if (!_context.ProductBrands.Any())
        //    {
        //        var brandsPath = Path.Combine(seedDataPath, "brands.json");
        //        if (File.Exists(brandsPath))
        //        {
        //            var ProductBrandsData = File.ReadAllText(brandsPath);
        //            var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);
        //            if (ProductBrands != null && ProductBrands.Count > 0)
        //            {
        //                _context.ProductBrands.AddRange(ProductBrands);
        //            }
        //        }
        //        else
        //        {
        //            throw new FileNotFoundException($"Brands seed file not found at: {brandsPath}");
        //        }
        //    }

        //    if (!_context.ProductTypes.Any())
        //    {
        //        var typesPath = Path.Combine(seedDataPath, "types.json");
        //        if (File.Exists(typesPath))
        //        {
        //            var ProductTypesData = File.ReadAllText(typesPath);
        //            var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
        //            if (ProductTypes != null && ProductTypes.Count > 0)
        //            {
        //                _context.ProductTypes.AddRange(ProductTypes);
        //            }
        //        }
        //        else
        //        {
        //            throw new FileNotFoundException($"Types seed file not found at: {typesPath}");
        //        }
        //    }

        //    if (!_context.Products.Any())
        //    {
        //        var productsPath = Path.Combine(seedDataPath, "products.json");
        //        if (File.Exists(productsPath))
        //        {
        //            var ProductsData = File.ReadAllText(productsPath);
        //            var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
        //            if (Products != null && Products.Count > 0)
        //            {
        //                _context.Products.AddRange(Products);
        //            }
        //        }
        //        else
        //        {
        //            throw new FileNotFoundException($"Products seed file not found at: {productsPath}");
        //        }
        //    }

        //    _context.SaveChanges();
        //}

    }
}
