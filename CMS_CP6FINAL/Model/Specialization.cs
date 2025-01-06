using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CMS_CP6FINAL.Model;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public string SpecializationName { get; set; } = null!;
<<<<<<< HEAD
=======

>>>>>>> 81d8d89bb65215eaa82ed75f09d12a5529332f40
      [JsonIgnore]
    public virtual ICollection<Department>? Departments { get; set; } = new List<Department>();
      [JsonIgnore]
    public virtual ICollection<DoctorReferral>? DoctorReferrals { get; set; } = new List<DoctorReferral>();
      [JsonIgnore]
    public virtual ICollection<NewAppointment>? NewAppointments { get; set; } = new List<NewAppointment>();
<<<<<<< HEAD
=======

>>>>>>> 81d8d89bb65215eaa82ed75f09d12a5529332f40
}
