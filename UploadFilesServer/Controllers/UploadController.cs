using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFilesServer.Context;
using UploadFilesServer.Models;

namespace UploadFilesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ImageContext _context;

        public UploadController(ImageContext context)
        {
            _context = context;
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public IActionResult Upload([FromForm] IFormFile file)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var id = Guid.NewGuid();
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, id.ToString()+".jpg");
                var filepath = Path.Combine(folderName, fileName);
                var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
               
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
            
                    file.CopyTo(stream);
                }

                var image = new Image
                {
                    Id = id,
                    ImgPath = filepath
                };
                _context.Add(image);
                _context.SaveChanges();
                Console.WriteLine(filepath);

                return Ok(new {filepath});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}