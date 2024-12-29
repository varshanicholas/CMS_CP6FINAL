using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class DoctorReferral
{
    public int ReferralId { get; set; }

    public int ReferringDoctorId { get; set; }

    public int? ReferredDoctorId { get; set; }

    public int SpecializationId { get; set; }

    public int PatientId { get; set; }

    public int AppointmentId { get; set; }

    public DateTime? ReferralDate { get; set; }

    public string? Notes { get; set; }

    public virtual NewAppointment Appointment { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual Staff? ReferredDoctor { get; set; }

    public virtual Doctor ReferringDoctor { get; set; } = null!;

    public virtual Specialization Specialization { get; set; } = null!;
}
