using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public string SpecializationName { get; set; } = null!;
<<<<<<< HEAD
=======

<<<<<<< HEAD
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
=======
    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
>>>>>>> 4e7aadf4c07fde4730c818842b21554d1f7551ed
}
