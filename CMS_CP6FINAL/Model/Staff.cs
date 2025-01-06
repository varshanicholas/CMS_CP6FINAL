using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

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
}
