using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class AdmissionStatus
{
    public int StatusId { get; set; }

    public int? FormId { get; set; }

    public string? StatusUpdate { get; set; }

    public string? StatusMessage { get; set; }

    public DateTime? StatusDate { get; set; }

    public int? AdmissionUserId { get; set; }

    public virtual User? AdmissionUser { get; set; }

    public virtual AdmissionForm? Form { get; set; }
}
