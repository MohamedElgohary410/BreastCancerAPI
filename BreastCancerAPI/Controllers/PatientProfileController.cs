using BreastCancerAPI.DTOs;
using BreastCancerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreastCancerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddPatientProfileDetails")]
        public async Task<IActionResult> AddPatientProfileDetails(PatientProfileDto patientProfile)
        {
            using var stream = new MemoryStream();
            await patientProfile.PatientImage.CopyToAsync(stream);

            var patientDetails = new PatientProfile
            {
                PatientImage = stream.ToArray(),
                PatientName = patientProfile.PatientName,
                PhoneNumber = patientProfile.PhoneNumber,
                NationalId = patientProfile.NationalId,
                Age = patientProfile.Age,
                Stage = patientProfile.Stage,
                Treatment = patientProfile.Treatment,
                DrFollow = patientProfile.DrFollow
            };

            if (patientDetails is null)
                return BadRequest("Image not found!");

            await _context.AddAsync(patientDetails);
            _context.SaveChanges();

            return Ok(patientDetails);
        }
    }
}
