using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string FacultyName { get; set; } = null!;

    public string? FacultyImage { get; set; }

    public DateTime? Datesubmitted { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
