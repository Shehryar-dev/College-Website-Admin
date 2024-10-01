using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public int CourseCredits { get; set; }

    public string? CourseDuration { get; set; }

    public string? CourseDescription { get; set; }

    public virtual ICollection<AdmissionForm> AdmissionForms { get; set; } = new List<AdmissionForm>();

    public virtual Department? Department { get; set; }
}
