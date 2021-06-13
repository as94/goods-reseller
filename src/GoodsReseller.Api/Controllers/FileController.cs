using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(
            [Required] [FromForm] FileUpload fileUpload,
            CancellationToken cancellationToken)
        {
            var path = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "assets",
                fileUpload.FileName.ToLower());

            await using var fileStream = System.IO.File.Create(path);
            await fileUpload.FileContent.CopyToAsync(fileStream, cancellationToken);
            
            return Ok();
        }
    }
}