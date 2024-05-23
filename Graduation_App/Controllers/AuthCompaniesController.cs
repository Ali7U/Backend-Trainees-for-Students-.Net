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
    public class AuthCompaniesController : ControllerBase
    {
        public static Company company = new Company();
        
        private readonly MyAppDbContext _context;
        private readonly IConfiguration _configuration;
        
        public AuthCompaniesController(MyAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(AddCompanyDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);


           company.CompanyId = request.CompanyId;
           company.CompanyName = request.CompanyName;
           company.AboutCompany = request.AboutCompany;
           company.Description = company.Description;
           company.CompanyLogo = request.CompanyLogo;
           company.Location = request.Location;
           company.ContactInformation = request.ContactInformation;
           company.Industry = request.Industry;
           company.Password = passwordHash;


            _context.Companies.Add(company);
            await _context.SaveChangesAsync();


            return Ok(company);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(LoginCompanyDTO request)
        {
            
            var company = _context.Companies.FirstOrDefault(x => x.CompanyName == request.CompanyName);
            if (company is null  )
            {
                return BadRequest(new {Message ="wrong email or password"});
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Password, company.Password))
            {
                return BadRequest(new {Message ="wrong password"});
            }

            string token = CreateToken(company);

// #pragma warning disable CS8601 // Possible null reference assignment.
            var companyDTO = new LoginCompanyDTO 
            {
                CompanyName = company.CompanyName,
                Password = company.Password,
                token = token

            };

            return Ok(companyDTO);
        }

        private string CreateToken(Company company)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("CompanyId", company.CompanyId.ToString()),
                new Claim("CompanyName", company.CompanyName)
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