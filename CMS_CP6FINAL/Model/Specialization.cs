using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model
{
    public partial class Specialization
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; } = null!;
        public int? DepartmentId { get; set; } // Nullable because it might not be assigned initially


        public virtual Department? Department { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();

  
  
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}


























//using System;
//using System.Collections.Generic;

//namespace CMS_CP6FINAL.Model;

//public partial class Specialization
//{
//    public int SpecializationId { get; set; }

//    public string SpecializationName { get; set; } = null!;

//    public int? DepartmentId { get; set; }

//    public virtual Department? Department { get; set; }

//    public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();

//    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
//}

