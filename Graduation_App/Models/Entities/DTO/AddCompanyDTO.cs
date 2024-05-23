using System;
using System.Collections.Generic;

namespace Graduation_App.Models.Entities;

public partial class AddCompanyDTO
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? CompanyLogo { get; set; }

    public string? AboutCompany { get; set; }

    public string? Industry { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? ContactInformation { get; set; }

    public string? Password { get; set; }

}
