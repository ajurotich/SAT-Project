using System;
using System.Collections.Generic;

namespace SAT.Data.EF.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string CourseDescription { get; set; } = null!;

    public byte CreditHours { get; set; }

    public string? Curriculum { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ScheduledClass> ScheduledClasses { get; set; } = new List<ScheduledClass>();
}
