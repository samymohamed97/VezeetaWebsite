using Core.DTO;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;

        }
        [HttpPost("register")]
        public async Task<IActionResult> RegistrationAsync([FromForm]RegisterDTO registerDTO)
        {
            using var datastrem = new MemoryStream();
            await registerDTO.Image.CopyToAsync(datastrem);
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = new ApplicationUser();
                AppUser.FirstName = registerDTO.FirstName;
                AppUser.LastName = registerDTO.LastName;
                AppUser.Email = registerDTO.Email;
                AppUser.Phone = registerDTO.Phone;
                AppUser.Gender = registerDTO.Gender;
                AppUser.DateOfBirth = registerDTO.DateOfBirth;
                AppUser.Image = datastrem.ToArray();

                IdentityResult result = await userManager.CreateAsync(AppUser, registerDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Created");
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDTO userDTO)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(userDTO.Email);
                if (user == null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userDTO.Password);
                    if (found)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
                        //claims.Add(new Claim(ClaimTypes.Email, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        //Create Token
                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],                
                            audience: config["JWT:ValidAudience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCredentials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration = myToken.ValidTo
                        });
                    }
                    return Unauthorized();
                }
            }
            return Unauthorized();
        }
   
    }
}
