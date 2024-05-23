using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Graduation_App.Models.Entities;

public partial class UpdateTraineecourseDTO
{

    public int? CompanyId { get; set; }

    public string? TraineeTitle { get; set; }

    public string? TraineeDescription { get; set; }

    public string? ExpectationsFromStudents { get; set; }

    public decimal? Gparequirement { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? MaxUsers { get; set; }


}
