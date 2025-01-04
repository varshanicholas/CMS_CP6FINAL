using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int SpecializationId { get; set; }

    public virtual Specialization Specialization { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] 
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}

