using System;
using System.Collections.Generic;

namespace Graduation_App.Models.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public decimal? Gpa { get; set; }

    public string? Major { get; set; }

    public string? Skills { get; set; }

    public string? ResumeCv { get; set; }

    public string? Portfolio { get; set; }

    public string? LinkedInProfile { get; set; }

    public string? GitHubProfile { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
