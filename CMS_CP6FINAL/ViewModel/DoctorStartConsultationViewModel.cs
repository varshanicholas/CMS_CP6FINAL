public class DoctorStartConsultationViewModel
{
    public int AppointmentId { get; set; }
    public string Symptoms { get; set; }
    public string Diagnosis { get; set; }
    public string? DoctorNotes { get; set; }

    public int CreatedBy { get; set; }
    public List<MedicineViewModel> Medicines { get; set; } = new List<MedicineViewModel>();
    public List<LabTestViewModel> LabTests { get; set; } = new List<LabTestViewModel>();
}

public class MedicineViewModel
{
    public string CategoryName { get; set; }
    public string MedicineName { get; set; }
    public string Dosage { get; set; }
    public string Frequency { get; set; }
    public string Quantity { get; set; }
}

public class LabTestViewModel
{
    public string LabTestName { get; set; }
    public string CategoryName { get; set; }
}
