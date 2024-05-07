using BreastCancerAPI.DTOs;
using BreastCancerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreastCancerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddClientDetails")]
        public async Task<IActionResult> AddClientDetails(ClientDetailsDto clientDetails)
        {
            using var stream = new MemoryStream();
            await clientDetails.CTscan.CopyToAsync(stream);
            await clientDetails.Mammogram.CopyToAsync(stream);
            await clientDetails.MRLTest.CopyToAsync(stream);

            var client = new ClientDetails
            {
                Name = clientDetails.Name,
                PhoneNumber = clientDetails.PhoneNumber,
                CTscan = stream.ToArray(),
                Mammogram = stream.ToArray(),
                MRLTest = stream.ToArray()
            };

            if (client is null)
                return BadRequest("Please add the values!");

            await _context.AddAsync(client);
            _context.SaveChanges();

            return Ok(client);
        }
    }
}
