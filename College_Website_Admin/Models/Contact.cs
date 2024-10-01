using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class Contact
{
    public int ContId { get; set; }

    public string ContName { get; set; } = null!;

    public string ContEmail { get; set; } = null!;

    public string ContPhone { get; set; } = null!;

    public string ContAddress { get; set; } = null!;

    public string? ContMessage { get; set; }

    public int? ContUserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? ContUser { get; set; }
}
