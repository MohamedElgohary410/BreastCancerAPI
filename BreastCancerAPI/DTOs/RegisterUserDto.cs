using System.ComponentModel.DataAnnotations;

namespace JWT_ITI.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string  Username { get; set; }

        [Required]
        public string  Password { get; set; }

        [Required]
        [Compare("Password")]
        public string  ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
