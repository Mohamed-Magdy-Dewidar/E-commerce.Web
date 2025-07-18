using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ServiceAbstraction;

namespace Service
{
    public class FileSystemAttachmentService : IAttachmentService
    {
        private readonly string _rootPath;

        private readonly IUnitOfWork _UnitOfWork;

        public FileSystemAttachmentService(IWebHostEnvironment env , IUnitOfWork unitOfWork)
        {
            _rootPath = Path.Combine(env.WebRootPath);
            _UnitOfWork = unitOfWork;
        }

     

        public async Task<string> UploadAsync(IFormFile file, string subFolder)
        {

            var folderPath = Path.Combine(_rootPath, "images", subFolder);            
            if(!Directory.Exists(folderPath))
                 Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            var fullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/{subFolder}/{fileName}"; // Relative path to serve later
        }


        public async Task<bool> Delete(int id)
        {

            var product = await _UnitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            if (product is null || string.IsNullOrWhiteSpace(product.PictureUrl))
                return false;

            string productImage = product.PictureUrl;
            var fullPath = Path.Combine(_rootPath, productImage.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            
            if(fullPath.Contains("images"))
                fullPath = fullPath.Replace("images", "Images");

            if (fullPath.Contains("products"))
                fullPath = fullPath.Replace("products", "Products");


            // ✅ Fix: Decode URL-encoded characters (e.g. %20 => space)
            fullPath = Uri.UnescapeDataString(fullPath);
            // the path from app 
            // D:\\Backend\\C# Code\\API\\E-commerce.Web\\E-commerce.Web\\wwwroot\\Images\\Products\\a24330c7-aa8b-4fe5-9d96-da68417ad37evolume_5_convergence%20(1).png
            //the path from the file system
            // D:\Backend\C# Code\API\E-commerce.Web\E-commerce.Web\wwwroot\Images\Products\a24330c7-aa8b-4fe5-9d96-da68417ad37evolume_5_convergence (1).png
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

      
    }
}
