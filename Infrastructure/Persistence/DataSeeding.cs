using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using Persistence.Identity;
using Service;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _context , UserManager<ApplicationUser> _userManager
        , RoleManager<IdentityRole> _roleManager , StoreIdentityDbContext _identityDbContext ) : IDataSeeding
    {
        public async Task IdentitySeedDataAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "mohamedMagdy@gmail.com",
                        DisplayName = "mego",
                        PhoneNumber = "09123456789",
                        UserName = "mohamedMagdy",
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "salmaMohamed@gmail.com",
                        DisplayName = "solly",
                        PhoneNumber = "09123456789",
                        UserName = "salmaMohamed",
                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(User02, "Admin");

                    await _identityDbContext.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

            }
        }

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

            // seed products
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

            //seed Delivery
            if (!_context.Set<DeliveryMethod>().Any())
            {
                var DeliveryMethodData = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);

                if (DeliveryMethods?.Count > 0)
                {
                    await _context.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
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
