namespace Graduation_App.Models.Entities.DTO
{
    public class LoginUserDTO
    {
        public required string Email { get; set; }
        public required string LoginPassword { get; set; }
        public string? token { get; set; }

    }
}
