using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class NewAppointment
{
    public int AppointmentId { get; set; }

    public int DocId { get; set; }

    public int? SpecializationId { get; set; }

    public int PatientId { get; set; }

    public int DailyAppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public int TokenNumber { get; set; }

    public int ConsultationFees { get; set; }

    public int RegistrationFees { get; set; }

    public bool ConsultationStatus { get; set; }

    public int DocAvlId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual DailyAppointmentAvailability DailyAppointment { get; set; } = null!;

    public virtual Doctor Doc { get; set; } = null!;

    public virtual DoctorAvailability DocAvl { get; set; } = null!;

    public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();

    public virtual ICollection<LabReport> LabReports { get; set; } = new List<LabReport>();

    public virtual ICollection<LabTestPrescription> LabTestPrescriptions { get; set; } = new List<LabTestPrescription>();

    public virtual ICollection<MainPrescription> MainPrescriptions { get; set; } = new List<MainPrescription>();

    public virtual ICollection<MedicinePrescription> MedicinePrescriptions { get; set; } = new List<MedicinePrescription>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual Specialization? Specialization { get; set; }
}
