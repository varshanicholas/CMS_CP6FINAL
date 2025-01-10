using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class UserRegistration
{
    public int UserId { get; set; }

    public int StaffId { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Role? Role { get; set; } 
     [System.Text.Json.Serialization.JsonIgnore]
    public virtual Staff? Staff { get; set; } = null!;
}
