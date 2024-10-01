using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class PreviousEducation
{
    public int EducationId { get; set; }

    public int? StudentId { get; set; }

    public string InstituteName { get; set; } = null!;

    public string? UniversityName { get; set; }

    public string? ExamName { get; set; }

    public int? MarksObtained { get; set; }

    public int? TotalMarks { get; set; }

    public string? ClassObtained { get; set; }

    public DateOnly? PassingYear { get; set; }

    public int? AdmissionUserId { get; set; }

    public virtual User? AdmissionUser { get; set; }

    public virtual Student? Student { get; set; }
}
