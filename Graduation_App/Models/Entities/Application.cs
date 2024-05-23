using System;
using System.Collections.Generic;

namespace Graduation_App.Models.Entities;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int? UserId { get; set; }

    public int? TraineeCourseId { get; set; }

    public string? Status { get; set; }

    public virtual Traineecourse? TraineeCourse { get; set; }

    public virtual User? User { get; set; }
}
