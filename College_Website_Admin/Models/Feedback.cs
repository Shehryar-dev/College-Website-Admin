using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class Feedback
{
    public int Feedbackid { get; set; }

    public int? FeedbackUserId { get; set; }

    public string? Feedbacktext { get; set; }

    public DateTime? Datesubmitted { get; set; }

    public virtual User? FeedbackUser { get; set; }
}
