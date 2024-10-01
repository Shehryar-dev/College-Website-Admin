using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class Facility
{
    public int FacilityId { get; set; }

    public string FacilityName { get; set; } = null!;

    public string FacilityDescription { get; set; } = null!;

    public DateOnly? FacilityAvailablefrom { get; set; }

    public int? FacilityDepartId { get; set; }

    public virtual Department? FacilityDepart { get; set; }
}
