namespace AdminDashBoard.AttachmentServices
{
    public interface IAttachmentService
    {
        string? UploadFile(IFormFile file, string folderName);
        Task<string?> UploadFileAsync(IFormFile file , string FolderName);

        public Task<bool> DeleteFileAsync(int id);
    }

}
