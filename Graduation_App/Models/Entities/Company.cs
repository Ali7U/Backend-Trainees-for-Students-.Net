﻿using System;
using System.Collections.Generic;

namespace Graduation_App.Models.Entities;

public partial class Company
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

    public virtual ICollection<Traineecourse> Traineecourses { get; set; } = new List<Traineecourse>();
}
