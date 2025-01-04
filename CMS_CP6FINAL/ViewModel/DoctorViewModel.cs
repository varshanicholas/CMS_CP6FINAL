using CMS_CP6FINAL.Model;

namespace CMS_CP6FINAL.ViewModel
{
    public class DoctorViewModel
    {
        // Staff properties
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DepartmentName { get; set; } 

        // Doctor properties
        public int DoctorId { get; set; }
        public decimal ConsultationFee { get; set; }

        // Specialization properties
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }

        // Navigation properties
        public virtual Department Department { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
