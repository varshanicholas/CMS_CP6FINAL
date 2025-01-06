namespace CMS_CP6FINAL.ViewModel
{
    public class StartDiagnosysViewmodel
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TokenNumber { get; set; }
        public string PatientName { get; set; } = null!;
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? PhoneNumber { get; set; } // Updated to match the data you want to return
        public string? SpecializationName { get; set; } // Updated to match the data you want to return

    }
}
