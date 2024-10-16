﻿using System;
using System.Collections.Generic;

namespace SAT.Data.EF.Models;

public partial class StudentStatus
{
    public int Ssid { get; set; }

    public string Ssname { get; set; } = null!;

    public string? Ssdescription { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
