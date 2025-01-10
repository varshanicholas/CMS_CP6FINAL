using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Staff
{
<<<<<<< HEAD
    public partial class Staff
    {


        public Staff() { Doctors = new HashSet<Doctor>(); }
        public int StaffId { get; set; }
=======
    public int StaffId { get; set; }
>>>>>>> 4e7aadf4c07fde4730c818842b21554d1f7551ed

    public string StaffName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Address { get; set; } = null!;

    public string Qualification { get; set; } = null!;

<<<<<<< HEAD
        public int DepartmentId { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public virtual Department? Department { get; set; }
        [JsonIgnore]
        public virtual ICollection<DoctorReferral>? DoctorReferrals { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<Doctor> Doctors { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<LabTestPrescription>? LabTestPrescriptions { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<MainPrescription>? MainPrescriptions { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<MedicinePrescription>? MedicinePrescriptions { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<UserRegistration>? UserRegistrations { get; set; } = null;
    }
=======
    public int DepartmentId { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<LabTestPrescription> LabTestPrescriptions { get; set; } = new List<LabTestPrescription>();

    public virtual ICollection<MainPrescription> MainPrescriptions { get; set; } = new List<MainPrescription>();

    public virtual ICollection<MedicinePrescription> MedicinePrescriptions { get; set; } = new List<MedicinePrescription>();

    public virtual ICollection<UserRegistration> UserRegistrations { get; set; } = new List<UserRegistration>();
>>>>>>> 4e7aadf4c07fde4730c818842b21554d1f7551ed
}
