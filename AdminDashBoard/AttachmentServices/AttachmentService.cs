
namespace AdminDashBoard.AttachmentServices
{
    public class AttachmentService(HttpClient client , IConfiguration configuration) : IAttachmentService
    {
        private readonly int _maxFileSize = 4*1024*1024;

        private IReadOnlyList<string> _allowedPicturesExtensions = new List<string>
        {
            ".jpg", ".jpeg", ".png", ".gif" , ".webp"
        };


        public async Task<bool> DeleteFileAsync(int id)
        {
            var baseUrl = configuration.GetSection("Urls")["BaseUrl"]?.TrimEnd('/');
            var response = await client.DeleteAsync($"{baseUrl}/api/Files/Delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Delete failed: {response.StatusCode} - {errorContent}");
                return false;
            }

            return true;
        }



        public string? UploadFile(IFormFile file, string folderName)
        {

            /**
                  1. Check Extension 
                  2. Check Size 
                  3. Get Located Folder Path
                  4. Make Attachment Name Unique -- GUID
                  5. Get File Path
                  6. Create File Stream To Copy File [Unmanaged]
                  7. Use Stream To Copy File 
                  8. Return FileName To Store In Database                                       
             **/

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();


            if (file.Length > _maxFileSize || file.Length == 0)
                return null;

            if (!_allowedPicturesExtensions.Contains(extension))
                return null;

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\images", folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = $"{Guid.NewGuid()}-{file.FileName}";

            var filePath = Path.Combine(folderPath, fileName);


            //6. Create File Stream To Copy File [Unmanaged]
            using FileStream fs = new FileStream(path: filePath, mode: FileMode.Create);
            //7. Use Stream To Copy File
            file.CopyTo(fs);

            //8. Return path To Store In Database
            return Path.Combine("images/products/", fileName);

        }


        public async Task<string?> UploadFileAsync(IFormFile file, string folderName)
        {
            using var content = new MultipartFormDataContent();
            using var stream = file.OpenReadStream();

            content.Add(new StreamContent(stream), "file", file.FileName);
            content.Add(new StringContent(folderName), "FolderName");

            var baseUrl = configuration.GetSection("Urls")["BaseUrl"]?.TrimEnd('/');
            var response = await client.PostAsync($"{baseUrl}/api/Files/upload", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(); // <-- ADD THIS
                Console.WriteLine($"Upload failed: {response.StatusCode} - {errorContent}");
                return null;
            }


            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            if (result is null || !result.ContainsKey("imageUrl"))
                return null;

            var imageUrl = result["imageUrl"];

            // Convert full URL to relative path (e.g., /Images/products/... → Images/products/...)
            var uri = new Uri(imageUrl);
            var relativePath = uri.AbsolutePath.TrimStart('/');

            return relativePath;

        }


    }
}
