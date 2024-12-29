using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class MainPrescription
{
    public int MainPrescriptionId { get; set; }

    public string Symptoms { get; set; } = null!;

    public string Diagnosis { get; set; } = null!;

    public string DoctorNotes { get; set; } = null!;

    public int AppointmentId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public virtual NewAppointment Appointment { get; set; } = null!;

    public virtual Staff CreatedByNavigation { get; set; } = null!;
}
