using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class LabReport
{
    public int LabReportId { get; set; }

    public int LabTestId { get; set; }

    public string? LabTestValue { get; set; }

    public string? Remarks { get; set; }

    public int AppointmentId { get; set; }

    public int LabTestPrescriptionId { get; set; }

    public virtual NewAppointment Appointment { get; set; } = null!;

    public virtual LabTest LabTest { get; set; } = null!;

    public virtual LabTestPrescription LabTestPrescription { get; set; } = null!;
}
