using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class LabTest
{
    public int LabTestId { get; set; }

    public string LabTestName { get; set; } = null!;

    public int CategoryId { get; set; }

    public decimal Cost { get; set; }

    public string ResultType { get; set; } = null!;

    public decimal? ReferenceMinRange { get; set; }

    public decimal? ReferenceMaxRange { get; set; }

    public string SampleRequired { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual LabTestCategory Category { get; set; } = null!;

    public virtual ICollection<LabReport> LabReports { get; set; } = new List<LabReport>();

    public virtual ICollection<LabTestPrescription> LabTestPrescriptions { get; set; } = new List<LabTestPrescription>();
}
