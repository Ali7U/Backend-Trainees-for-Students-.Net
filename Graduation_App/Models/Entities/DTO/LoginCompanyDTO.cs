namespace Graduation_App.Models.Entities.DTO
{
    public class LoginCompanyDTO
    {
        public required string CompanyName { get; set; }
        public string? Password { get; set; }
        public string? token { get; set; }
        
    }
}