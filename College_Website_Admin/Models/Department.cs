using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class Department
{
    public int Departmentid { get; set; }

    public string Departmentname { get; set; } = null!;

    public string? Departmenthead { get; set; }

    public DateTime? Datesubmitted { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
