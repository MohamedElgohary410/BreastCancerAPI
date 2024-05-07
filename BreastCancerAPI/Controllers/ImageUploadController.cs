using BreastCancerAPI.DTOs;
using BreastCancerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreastCancerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ImageUploadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImageUploadController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(PictureDto picture)
        {
            using var stream = new MemoryStream();
            await picture.Img.CopyToAsync(stream);

            var file = new FileUpload
            {
                ImgName = stream.ToArray()
            };

            if (file is null)
                return BadRequest("Image not found!");

            await _context.AddAsync(file);
            _context.SaveChanges();

            return Ok(file);
        }
    }

}
