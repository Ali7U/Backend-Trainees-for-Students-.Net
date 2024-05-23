namespace Graduation_App.Models.Entities.DTO
{
   public class AppDto
{
    public int ApplicationId { get; set; }
    public int TraineeCourseId { get; set; }
    public int? UserId { get; set; }
    public string? Status { get; set; }
}

public class TraineeDto
{
    public int TraineeCourseId { get; set; }
    public string? TraineeTitle { get; set; }
    public int? CompanyId { get; set; }
    public string? TraineeDescription { get; set; }
    public string? ExpectationsFromStudents { get; set; }
    public decimal? Gparequirement { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int? MaxUsers { get; set; }
    public List<AppDto>? Applications { get; set; }
}

public class CompanyDto
{
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public List<TraineeDto>? Traineecourses { get; set; }
}

}