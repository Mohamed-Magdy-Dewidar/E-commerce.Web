using System.Net;
using AdminDashBoard.AttachmentServices;
using AdminDashBoard.MappingProfiles;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule; // For ApplicationUser
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Persistence.Data.Contexts;
using Persistence.Identity;
using Persistence.Respositories;
using StackExchange.Redis;



namespace AdminDashBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews(options =>
            {
                // make sure that the anti forgery token is validated for all post requests
                // this is a security feature to prevent CSRF attacks
                // this will add the anti forgery token to all post requests
                // and validate it on the server side
                // allow post requests to be validated and only accepted if the token is valid which is from the application
                // not allow postman or any other tool to send post requests
                // must implement IFilterMetadata to validate the token
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            });



            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            // Configure Identity and Shared Database
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });





            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Home/Index";
            //    //options.AccessDeniedPath = "/Auth/AccessDenied";
            //});






            // Configure Identity (User + Role)
            // Why AddIdentity<>() instead of AddIdentityCore<>() ?
            //AddIdentityCore<>() only registers basic UserManager and leaves out SignInManager, RoleManager, cookie schemes, etc.
            //AddIdentity<>() is for MVC applications with login / logout support.
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>()
            .AddDefaultTokenProviders();


            //builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddAutoMapper(typeof(ProductProfile));
            builder.Services.AddHttpClient<IAttachmentService, AttachmentService>();



            var app = builder.Build();

            // Configure middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication & Authorization            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();

        }
    }
}
