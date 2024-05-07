using BreastCancerAPI.Models;
using JWT_ITI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_ITI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(UserManager<ApplicationUser> userManager,
        IConfiguration configuration) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = userDto.Username,
                    Email = userDto.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                    return Ok("Account created successfully!");
                return BadRequest(result.Errors.ToList().ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                // check if username and password is already in the data base or not
                var user = await _userManager.FindByNameAsync(userDto.Username);

                if (user is not null)
                {
                    // check if the password is right 
                    // password of the dto must be the same with the password what is in the database
                    var result = await _userManager.CheckPasswordAsync(user, userDto.Password);
                    if (result)
                    {
                        // token claims 
                        var claims = new List<Claim>();
                        claims.AddRange(new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        });

                        // get Roles, admin or user?
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        // security key of SigningCredentials
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                        // get Credentials 
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        // create token
                        var jwtSecurityToken = new JwtSecurityToken
                            (
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: credentials
                            );

                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                            Expiration = jwtSecurityToken.ValidTo
                        });
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }

                return Unauthorized();
            }
            return Unauthorized(); // 401 unauthorized
        }


    }
}
