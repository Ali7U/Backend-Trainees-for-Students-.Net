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
    public class CompanyController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public CompanyController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetAllCompanies()
        {
            var companies = await _context.Companies
                .Include(c => c.Traineecourses)
                .ToListAsync();

            return Ok(companies);
        }

       [HttpGet("{companyId}")]
        public async Task<ActionResult<List<CompanyDto>>> GetCompany(int companyId)
        {
            var company = await _context.Companies
                .Include(c => c.Traineecourses)
                .ThenInclude(c => c.Applications)
                .FirstOrDefaultAsync(i => i.CompanyId == companyId);

            if (company == null)
            {
                return NotFound("Company not found");
            }

            var companyDto = new CompanyDto
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                Traineecourses = company.Traineecourses.Select(tc => new TraineeDto
                {
                    TraineeCourseId = tc.TraineeCourseId,
                    TraineeTitle = tc.TraineeTitle,
                    CompanyId = tc.CompanyId,
                    TraineeDescription = tc.TraineeDescription,
                    ExpectationsFromStudents = tc.ExpectationsFromStudents,
                    Gparequirement = tc.Gparequirement,
                    StartDate = tc.StartDate,
                    EndDate = tc.EndDate,
                    MaxUsers = tc.MaxUsers,
                    Applications = tc.Applications.Select(a => new AppDto
                    {
                        ApplicationId = a.ApplicationId,
                        TraineeCourseId = a.TraineeCourseId ?? 0, // Provide a default value if null
                        UserId = a.UserId,
                        Status = a.Status
                    }).ToList()
                }).ToList()
            };

            return Ok(new List<CompanyDto> { companyDto });
        }

        [HttpPost]
        public async Task<ActionResult<Company>> AddCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return Ok(await _context.Companies.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Company>> UpdateCompany(Company updatedCompany)
        {
            var dbCompany = await _context.Companies.FindAsync(updatedCompany.CompanyId);
            if (dbCompany is null)
            {
                return NotFound("Company not Found!");
            }
            dbCompany.CompanyName = updatedCompany.CompanyName;
            dbCompany.CompanyLogo = updatedCompany.CompanyLogo;
            dbCompany.AboutCompany = updatedCompany.AboutCompany;
            dbCompany.Industry = updatedCompany.Industry;
            dbCompany.Location = updatedCompany.Location;
            dbCompany.ContactInformation = updatedCompany.ContactInformation;
            dbCompany.Password = updatedCompany.Password;
            dbCompany.Traineecourses = updatedCompany.Traineecourses;

            await _context.SaveChangesAsync();

            return Ok(await _context.Companies.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            var dbCompany = await _context.Companies.FindAsync(id);

            if (dbCompany is null)
            {
                return NotFound("Company not Found!");
            }

            _context.Companies.Remove(dbCompany);
            await _context.SaveChangesAsync();
            return Ok(await _context.Companies.ToListAsync());

        }
    }
}
