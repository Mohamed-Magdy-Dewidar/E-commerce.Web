using Microsoft.AspNetCore.Http;




namespace ServiceAbstraction
{
    public interface IAttachmentService
    {
        Task<string> UploadAsync(IFormFile file, string subFolder);

        public Task<bool> Delete(int id);
    }
}
