using System;
using System.Collections.Generic;

namespace College_Website_Admin.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
