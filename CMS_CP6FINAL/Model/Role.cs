﻿using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<UserRegistration> UserRegistrations { get; set; } = new List<UserRegistration>();
}
