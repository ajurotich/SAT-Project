using System;
using System.Collections.Generic;

namespace SAT.Data.EF.Models;

public partial class ScheduledClass
{
    public int ScheduledClassId { get; set; }

    public int CourseId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string InstructorName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int Scsid { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ScheduledClassStatus Scs { get; set; } = null!;
}
