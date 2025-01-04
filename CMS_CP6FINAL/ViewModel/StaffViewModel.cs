using System;

namespace CMS_CP6FINAL.Model
{
    public class StaffViewModel
    {
        public int StaffId { get; set; }

        public string StaffName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime Dob { get; set; }

        public string Address { get; set; } = null!;

        public string Qualification { get; set; } = null!;

        public int? DepartmentId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
