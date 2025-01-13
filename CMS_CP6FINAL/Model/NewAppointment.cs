using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class NewAppointment
{
    public int AppointmentId { get; set; }

    public int? DoctorId { get; set; }

    public int? DepartmentId { get; set; }

    public int? PatientId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public int? TokenNumber { get; set; }

    public int? ConsultationFees { get; set; }

    public int? RegistrationFees { get; set; }

    public bool? ConsultationStatus { get; set; }

    public int? DocAvlId { get; set; }

    public DateTime? CreatedDate { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<DailyAppointmentAvailability> DailyAppointmentAvailabilities { get; set; } = new List<DailyAppointmentAvailability>();

    public virtual Department? Department { get; set; }

    public virtual DoctorAvailability? DocAvl { get; set; } = null!;

    public virtual Doctor? Doctor { get; set; } = null!;
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<LabReport> LabReports { get; set; } = new List<LabReport>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<LabTestPrescription> LabTestPrescriptions { get; set; } = new List<LabTestPrescription>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<MainPrescription> MainPrescriptions { get; set; } = new List<MainPrescription>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<MedicinePrescription> MedicinePrescriptions { get; set; } = new List<MedicinePrescription>();

    public virtual Patient? Patient { get; set; } = null!;
}
