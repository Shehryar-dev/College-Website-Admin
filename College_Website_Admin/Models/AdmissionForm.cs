using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class AdmissionForm
{
    public int FormId { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public string AdmissionStream { get; set; } = null!;

    public string AdmissionField { get; set; } = null!;

    public string? PreviousUniversity { get; set; }

    public string? PreviousEnrollmentNo { get; set; }

    public string? PreviousCenter { get; set; }

    public string? PreviousStream { get; set; }

    public string? PreviousField { get; set; }

    public int MarksObtained { get; set; }

    public int TotalMarks { get; set; }

    public string? ClassObtained { get; set; }

    public string? SportsDetails { get; set; }

    public string? ApplicationStatus { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public int? AdmissionUserId { get; set; }

    public virtual ICollection<AdmissionStatus> AdmissionStatuses { get; set; } = new List<AdmissionStatus>();

    public virtual User? AdmissionUser { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<CourseSelection> CourseSelections { get; set; } = new List<CourseSelection>();

    public virtual Student? Student { get; set; }
}
