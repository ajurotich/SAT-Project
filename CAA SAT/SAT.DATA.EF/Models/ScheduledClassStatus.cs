using System;
using System.Collections.Generic;

namespace SAT.UI.MVC.Models;

public partial class ScheduledClassStatus
{
    public int Scsid { get; set; }

    public string Scname { get; set; } = null!;

    public virtual ICollection<ScheduledClass> ScheduledClasses { get; set; } = new List<ScheduledClass>();
}
