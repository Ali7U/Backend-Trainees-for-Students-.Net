using Graduation_App.Models;
using Graduation_App.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeCourseController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public TraineeCourseController(MyAppDbContext context)
        {
            _context = context;   
        }
        [HttpGet]
        public async Task<ActionResult<Traineecourse>> GetAllTrainiees()
        {
            var trainiees = await _context.Traineecourses
                .Include(t => t.Applications)
                .ToListAsync();
            return Ok(trainiees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Traineecourse>>> GetTrainee(int id)
        {
            var trainee = await _context.Traineecourses
                .Include(t => t.Applications)
                .FirstOrDefaultAsync(i => i.TraineeCourseId == id);
            if (trainee is null)
                return NotFound("Sorry, the compant not found!");


            return Ok(trainee);
        }

        [HttpPost]
        public async Task<ActionResult<Traineecourse>> CreateTraineeCourse(AddTraineecourseDTO traineeCourseDTO)
        {
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addTrainee = new Traineecourse()
            {
                CompanyId = traineeCourseDTO.CompanyId,
                TraineeTitle = traineeCourseDTO.TraineeTitle,
                TraineeDescription = traineeCourseDTO.TraineeDescription,
                ExpectationsFromStudents = traineeCourseDTO.ExpectationsFromStudents,
                Gparequirement = traineeCourseDTO.Gparequirement,
                StartDate = traineeCourseDTO.StartDate,
                EndDate = traineeCourseDTO.EndDate,
                MaxUsers = traineeCourseDTO.MaxUsers,
            };

            // var application = new Application
            // {
            //     TraineeCourseId = applicationDTO.TraineeCourseId,
            //     UserId = applicationDTO.UserId,
            //     Status = applicationDTO.Status
            // };

            // addTrainee.Applications.Add(application);

            _context.Traineecourses.Add(addTrainee);
            await _context.SaveChangesAsync();

            return Ok(addTrainee);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Traineecourse>> UpdateTraineeCourse(UpdateTraineecourseDTO traineeCourseDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var findCourse = await _context.Traineecourses.FindAsync(id);

            if (findCourse == null)
            {
                return NotFound("Sorry, the company doesn't exist");
            }


            findCourse.CompanyId = traineeCourseDTO.CompanyId;
            findCourse.TraineeTitle = traineeCourseDTO.TraineeTitle;
            findCourse.TraineeDescription = traineeCourseDTO.TraineeDescription;
            findCourse.ExpectationsFromStudents = traineeCourseDTO.ExpectationsFromStudents;
            findCourse.Gparequirement = traineeCourseDTO.Gparequirement;
            findCourse.StartDate = traineeCourseDTO.StartDate;
            findCourse.EndDate = traineeCourseDTO.EndDate;
            findCourse.MaxUsers = traineeCourseDTO.MaxUsers;

            await _context.SaveChangesAsync();

            return Ok(findCourse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Traineecourse>>> DeleteTrainee(int id)
        {
            var trainee = await _context.Traineecourses.FindAsync(id);
            if (trainee is null)
                return NotFound("Sorry, the compant not found!");

            _context.Traineecourses.Remove(trainee);
            await _context.SaveChangesAsync();
            return Ok(trainee);
        }
    }
}
