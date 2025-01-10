using CMS_CP6FINAL.Model;
using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.ViewModel
{
    public class PatientHistoryViewModel
    {
        public DateTime AppointmentDate { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string DoctorNotes { get; set; }
        public string Specialization { get; set; }
        public string StaffName { get; set; } // Added to hold the doctor's name
        public List<MedicineDetailsViewModel> Medicines { get; set; }
        public List<LabTestDetailsViewModel> LabTests { get; set; }
    }

    public class MedicineDetailsViewModel
    {
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Frequency { get; set; }
    }

    public class LabTestDetailsViewModel
    {
        public string LabTestName { get; set; }
        public string LabTestValue { get; set; }
        public string Remarks { get; set; }
        public ICollection<LabReport>? LabReports { get; internal set; }
    }
}
