using Microsoft.AspNetCore.Mvc;

namespace ZoneFranche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly string _uploadFolderPath;

        public ImageController()
        {
            _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\logoEntreprises");
            if (!Directory.Exists(_uploadFolderPath))
            {
                Directory.CreateDirectory(_uploadFolderPath);
            }
        }

        [HttpPost("upload/{name}")]
        public async Task<IActionResult> UploadImage(string name)
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file == null || file.Length == 0)
                    return BadRequest("File not selected");
                var fileName = name;
                var filePath = Path.Combine(_uploadFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok($"Image uploaded successfully. File Path: {filePath}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("download/{name}")]
        public IActionResult GetLogo(string name)
        {
            var filePath = Path.Combine(_uploadFolderPath, name);
            return PhysicalFile(filePath, "image/jpeg");
        }
    }
}
