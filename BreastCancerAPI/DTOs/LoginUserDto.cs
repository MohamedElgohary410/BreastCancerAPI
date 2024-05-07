using System.ComponentModel.DataAnnotations;

namespace JWT_ITI.DTOs
{
    public class LoginUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
