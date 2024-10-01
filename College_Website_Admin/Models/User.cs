using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string? UserImage { get; set; }

    public int? UserRoleId { get; set; }

    public virtual ICollection<AdmissionForm> AdmissionForms { get; set; } = new List<AdmissionForm>();

    public virtual ICollection<AdmissionStatus> AdmissionStatuses { get; set; } = new List<AdmissionStatus>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<PreviousEducation> PreviousEducations { get; set; } = new List<PreviousEducation>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual UserRole? UserRole { get; set; }
}
