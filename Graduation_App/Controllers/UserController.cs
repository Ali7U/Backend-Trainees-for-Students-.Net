using Graduation_App.Models;
using Graduation_App.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public UserController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _context.Users
                .Include(u => u.Applications)
                .ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Applications)
                .FirstOrDefaultAsync(i => i.UserId == id);

            if(user is null)
            {
                return NotFound("The user not found!");
            }

            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(AddUserDTO user)
        {

            user.Role = "Student";
            var addUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Gpa = user.Gpa,
                Major = user.Major,
                Skills = user.Skills,
                ResumeCv = user.ResumeCv,
                Portfolio = user.Portfolio,
                LinkedInProfile = user.LinkedInProfile,
                GitHubProfile = user.GitHubProfile,
                Role = user.Role,
            };
            
            _context.Users.Add(addUser);
            await _context.SaveChangesAsync();
            
            if (user is null) {
                return BadRequest("Sorry, you send wrong information");
            }

            return Ok(addUser);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(UpdateUserDTO updateUserDTO, int id)
        {
            var findUser = await _context.Users.FindAsync(id);
            updateUserDTO.Role = "Student";

            findUser.FirstName = updateUserDTO.FirstName;
            findUser.LastName = updateUserDTO.LastName;
            findUser.DateOfBirth = updateUserDTO.DateOfBirth;
            findUser.EmailAddress = updateUserDTO.EmailAddress;
            findUser.Password = updateUserDTO.Password;
            findUser.Gender = updateUserDTO.Gender;
            findUser.PhoneNumber = updateUserDTO.PhoneNumber;
            findUser.Gpa = updateUserDTO.Gpa;
            findUser.Major = updateUserDTO.Major;
            findUser.Skills = updateUserDTO.Skills;
            findUser.ResumeCv = updateUserDTO.ResumeCv;
            findUser.Portfolio = updateUserDTO.Portfolio;
            findUser.LinkedInProfile = updateUserDTO.LinkedInProfile;
            findUser.GitHubProfile = updateUserDTO.GitHubProfile;
            findUser.Role = updateUserDTO.Role;

            await _context.SaveChangesAsync();

            if (findUser is null)
            {
                return BadRequest("Sorry, you send wrong information");
            }

            return Ok(findUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                return NotFound("The user not found!");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Remove user seccessfully");
        }
    }
}
