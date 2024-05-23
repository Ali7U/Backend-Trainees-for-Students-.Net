using System;
using System.Collections.Generic;

namespace Graduation_App.Models.Entities;

public partial class UpdateApplicationDTO
{

    public int? TraineeCourseId { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }
}
