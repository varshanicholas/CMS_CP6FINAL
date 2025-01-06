using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int SpecializationId { get; set; }

<<<<<<< HEAD
    public virtual Specialization Specialization { get; set; }
=======

    public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();

    public virtual Specialization? Specialization { get; set; }

>>>>>>> 81d8d89bb65215eaa82ed75f09d12a5529332f40

    [System.Text.Json.Serialization.JsonIgnore] 
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}

