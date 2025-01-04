using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace CMS_CP6FINAL.Model
{
    public partial class Staff
    {
        public int StaffId { get; set; }

        public string StaffName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime Dob { get; set; }

        public string Address { get; set; } = null!;

        public string Qualification { get; set; } = null!;

        public int? DepartmentId { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Department Department { get; set; }
        [JsonIgnore]
        public virtual ICollection<DoctorReferral>? DoctorReferrals { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<Doctor>? Doctors { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<LabTestPrescription>? LabTestPrescriptions { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<MainPrescription>? MainPrescriptions { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<MedicinePrescription>? MedicinePrescriptions { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<UserRegistration>? UserRegistrations { get; set; } = null;
    }
}
