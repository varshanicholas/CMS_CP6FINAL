using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.ViewModel
{
    public class DoctorStartConsultationViewModel
    {
        public int AppointmentId { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string DoctorNotes { get; set; }
        public string Quantity { get; set; }
        public List<MedicineViewModel> Medicines { get; set; }
        public List<LabTestViewModel> LabTests { get; set; }
    }

    public class MedicineViewModel
    {
        public string MedicineName { get; set; }
        public string Unit { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Frequency { get; set; }
    }

    public class LabTestViewModel
    {
        public string LabTestName { get; set; }
        public string LabTestValue { get; set; }
        public string Remarks { get; set; }
    }

}
