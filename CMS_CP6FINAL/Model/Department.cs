﻿using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    
    [System.Text.Json.Serialization.JsonIgnore]

    public virtual Specialization Specialization { get; set; }

    public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();
    
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
