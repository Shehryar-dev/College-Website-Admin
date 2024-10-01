using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class CourseSelection
{
    public int SelectionId { get; set; }

    public int? FormId { get; set; }

    public string SelectedSubject { get; set; } = null!;

    public string? OptionalSubject { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual AdmissionForm? Form { get; set; }
}
