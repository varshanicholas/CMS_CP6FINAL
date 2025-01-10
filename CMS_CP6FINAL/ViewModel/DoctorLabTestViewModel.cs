namespace CMS_CP6FINAL.Repository
{
    public class DoctorLabTestViewModel
    {
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string BloodGroup { get; set; }
        public string LabTestName { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public string ResultType { get; set; }
        public decimal? ReferenceMinRange { get; set; }
        public decimal? ReferenceMaxRange { get; set; }
        public string SampleRequired { get; set; }
        public string CreatedDate { get; set; }
        public string Remarks { get; set; }
    }
}
