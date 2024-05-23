using Graduation_App.Models;
using Graduation_App.Models.Entities;
using Graduation_App.Models.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public ApplicationController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Application>>> GetALlApplications()
        {
            var applications = await _context.Applications.ToListAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id) 
        { 
            var application = await _context.Applications.FindAsync(id);

            if (application is null)
            {
                return NotFound("The application not found");
            }

            return Ok(application);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<AddApplicationDTO>> GetApplicationByUserId(int userId)
        {
            var application = await _context.Applications
                .Where(a => a.UserId == userId)
                .Select(a => new AddApplicationDTO
                {
                    TraineeCourseId = a.TraineeCourseId,
                    UserId = a.UserId,
                    Status = a.Status
                })
                .ToListAsync();

                 if (application == null || !application.Any())
                {
                    return NotFound("No applications found for the specified user.");
                }

            return Ok(application);
        }

        [HttpGet("TraineeCourse/{traineeCourseId}")]
        public async Task<ActionResult<UserDto>> GetUsersIdByTraineeId(int traineeCourseId)
        {
            var users = await _context.Applications
                .Where(a => a.TraineeCourseId == traineeCourseId)
                .Include(a => a.User) 
                .GroupBy(a => a.User)
                .Select(g => new UserDto
                {
                    UserId = g.Key.UserId,
                    FirstName = g.Key.FirstName,
                    LastName = g.Key.LastName,
                    LinkedInProfile = g.Key.LinkedInProfile,
                    GitHubProfile = g.Key.GitHubProfile,
                    Portfolio = g.Key.Portfolio,
                    Skills = g.Key.Skills,
                    Applications = g.Select(a => new ApplicationDto
                    {
                        ApplicationId = a.ApplicationId,
                        TraineeCourseId = a.TraineeCourseId,
                        UserId = a.UserId,
                        Status = a.Status
                    }).ToList()
                })
                .ToListAsync();

                  if (users == null || users.Count == 0)
                {
                    return NotFound("No users found for the specified trainee course.");
                }

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<Application>> CreateApplication(AddApplicationDTO addApplicationDTO)
        {

            addApplicationDTO.Status = "Pending";
            var createApplication = new Application
            {
                TraineeCourseId = addApplicationDTO.TraineeCourseId,
                UserId = addApplicationDTO.UserId,
                Status = addApplicationDTO.Status,
            };

            _context.Applications.Add(createApplication);
            await _context.SaveChangesAsync();

            if(createApplication is null)
            {
                return BadRequest("Sorry, wrong information");
            }

            return Ok(createApplication);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> UpdateApplication(UpdateApplicationDTO updateApplication,int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application is null)
            {
                return NotFound("The application not found");
            }

            application.TraineeCourseId = updateApplication.TraineeCourseId;
            application.UserId = updateApplication.UserId;
            application.Status = updateApplication.Status;

            await _context.SaveChangesAsync();

            return Ok(application);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Application>> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application is null)
            {
                return NotFound("The application not found");
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return Ok("The application was removed");
        }
    }
}
