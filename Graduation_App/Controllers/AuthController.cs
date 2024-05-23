using Graduation_App.Models;
using Graduation_App.Models.Entities;
using Graduation_App.Models.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Graduation_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly MyAppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(MyAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(AddUserDTO request)
        {
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Role = "Student";

            user.UserId = request.UserId;
            user.EmailAddress = request.EmailAddress;
            user.Password = request.Password;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.DateOfBirth = request.DateOfBirth;
            user.Gender = request.Gender;
            user.PhoneNumber = request.PhoneNumber;
            user.Gpa = request.Gpa;
            user.Major = request.Major;
            user.Skills = request.Skills;
            user.LinkedInProfile = request.LinkedInProfile;
            user.GitHubProfile = request.GitHubProfile;
            user.ResumeCv = request.ResumeCv;
            user.Portfolio = request.Portfolio;
            user.Role = request.Role;


            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(LoginUserDTO request)
        {
            var user = _context.Users.FirstOrDefault(x => x.EmailAddress == request.Email);
            if (user is null  )
            {
                return BadRequest(new {Message ="wrong email or password"});

            }

            if(request.LoginPassword != user.Password){
                return BadRequest(new {Message ="wrong password"});

            }

            string token = CreateToken(user);

            var userDTO = new LoginUserDTO 
            {
                Email = user.EmailAddress,
                LoginPassword = user.Password,
                token = token

            };
            
            return Ok(userDTO);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("Email", user.EmailAddress),
                new Claim("Role", user.Role),
            };

           

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("secret:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        } 
    }
}
