namespace Graduation_App.Models.Entities.DTO
{
    public class ApplicationDto
    {
        public int ApplicationId { get; set; }
        public int? TraineeCourseId { get; set; }
        public int? UserId { get; set; }
        public string? Status { get; set; }
    }
    public class UserDto
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LinkedInProfile { get; set; }
        public string? GitHubProfile { get; set; }
        public string? Portfolio { get; set; }
        public string? Skills { get; set; }
        public List<ApplicationDto>? Applications { get; set; }

    }
}