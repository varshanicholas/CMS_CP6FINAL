using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class LabTestPrescription
{
    public int LabTestPrescriptionId { get; set; }

    public int AppointmentId { get; set; }

    public int LabTestId { get; set; }

    public bool? IsStatus { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public virtual NewAppointment Appointment { get; set; } = null!;

    public virtual Staff CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<LabReport> LabReports { get; set; } = new List<LabReport>();

    public virtual LabTest LabTest { get; set; } = null!;
}
