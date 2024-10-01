using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentsName { get; set; } = null!;

    public string FatherName { get; set; } = null!;

    public string? MotherName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string StudentsGender { get; set; } = null!;

    public string? ResidentialAddress { get; set; }

    public string? PermanentAddress { get; set; }

    public string StudentsContactNumber { get; set; } = null!;

    public string StudentsEmail { get; set; } = null!;

    public DateTime? RegistrationDate { get; set; }

    public int? StudentUserId { get; set; }

    public virtual ICollection<AdmissionForm> AdmissionForms { get; set; } = new List<AdmissionForm>();

    public virtual ICollection<PreviousEducation> PreviousEducations { get; set; } = new List<PreviousEducation>();

    public virtual User? StudentUser { get; set; }
}
