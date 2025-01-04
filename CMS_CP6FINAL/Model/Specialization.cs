using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CMS_CP6FINAL.Model;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public string SpecializationName { get; set; } = null!;
      [JsonIgnore]
    public virtual ICollection<Department>? Departments { get; set; } = new List<Department>();
      [JsonIgnore]
    public virtual ICollection<DoctorReferral>? DoctorReferrals { get; set; } = new List<DoctorReferral>();
      [JsonIgnore]
    public virtual ICollection<NewAppointment>? NewAppointments { get; set; } = new List<NewAppointment>();
}
