using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public string SpecializationName { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();

    public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();
}
