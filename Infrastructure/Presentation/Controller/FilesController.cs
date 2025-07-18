using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;




namespace Presentation.Controller
{
    public class FilesController(IServiceManager _serviceManager) : ApiBaseController
    {


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] string FolderName)
        {
            var path = await _serviceManager.AttachmentService.UploadAsync(file, FolderName);

            if (string.IsNullOrEmpty(path))
            {
                return StatusCode(500, "File upload failed.");
            }

            var fullUrl = $"{Request.Scheme}://{Request.Host}{path}";
            return Ok(new { imageUrl = fullUrl });
        }

        //[HttpPost("upload")]
        //public async Task<IActionResult> Upload(IFormFile file, string FolderName)
        //{ 
        //    var path = await _serviceManager.AttachmentService.UploadAsync(file, FolderName);
        //    var fullUrl = $"{Request.Scheme}://{Request.Host}{path}";
        //    return Ok(new { imageUrl = fullUrl });
        //}




        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceManager.AttachmentService.Delete(id);
            return result 
                ? NoContent() 
                : NotFound(new { message = "File not found" });
        }


    }
}
