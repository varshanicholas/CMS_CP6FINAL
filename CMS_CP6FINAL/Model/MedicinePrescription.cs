using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class MedicinePrescription
{
    public int MedicinePrescriptionId { get; set; }

    public int AppointmentId { get; set; }

    public int MainPrescriptionId { get; set; }

    public int MedicineId { get; set; }

    public string Quantity { get; set; } = null!;

    public string? Dosage { get; set; }

    public string? Duration { get; set; }

    public string Frequency { get; set; } = null!;

    public bool? IsMedicineStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public virtual NewAppointment Appointment { get; set; } = null!;

    public virtual Staff CreatedByNavigation { get; set; } = null!;

    public virtual MainPrescription MainPrescription { get; set; } = null!;

    public virtual Medicine Medicine { get; set; } = null!;
}
