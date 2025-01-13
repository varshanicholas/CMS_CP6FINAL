using CMS_CP6FINAL.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMS_CP6FINAL.Utility
{
    public static class Validations
    {
        public static bool IsValid(this LabTest labTest)
        {
            if (string.IsNullOrWhiteSpace(labTest.LabTestName))
            {
                throw new ArgumentException("LabTest name is required");
            }

            if (labTest.Cost <= 0)
            {
                throw new ArgumentException("Cost should be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(labTest.ResultType))
            {
                throw new ArgumentException("ResultType is required");
            }

            if (string.IsNullOrWhiteSpace(labTest.SampleRequired))
            {
                throw new ArgumentException("SampleRequired is required");
            }

            return true;
        }

        public static bool IsValid(this Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.StaffName))
            {
                throw new ArgumentException("Staff name is required");
            }
            if (string.IsNullOrWhiteSpace(staff.PhoneNumber) || staff.PhoneNumber.Length != 10)
            {
                throw new ArgumentException("Valid PhoneNumber is required");
            }
            if (string.IsNullOrWhiteSpace(staff.Email))
            {
                throw new ArgumentException("Email is required");
            }
            if (staff.Dob == default)
            {
                throw new ArgumentException("Valid Date of Birth is required");
            }
            if (string.IsNullOrWhiteSpace(staff.Address))
            {
                throw new ArgumentException("Address is required");
            }
            if (string.IsNullOrWhiteSpace(staff.Qualification))
            {
                throw new ArgumentException("Qualification is required");
            }
            return true;
        }

        public static bool IsValid(this Doctor doctor)
        {
            if (doctor.StaffId == 0)
            {
                throw new ArgumentException("Staff ID is required");
            }
            if (doctor.ConsultationFee <= 0)
            {
                throw new ArgumentException("Consultation Fee should be greater than zero");
            }
            if (doctor.SpecializationId == 0)
            {
                throw new ArgumentException("Specialization ID is required");
            }
            return true;
        }

        public class RegisterUserModel
        {
            [Required]
            public int StaffId { get; set; }

            [Required]
            public int RoleId { get; set; }

            [Required]
            [StringLength(50)]
            public string Username { get; set; } = null!;

            [Required]
            [StringLength(50)]
            public string Password { get; set; } = null!;
        }
    }
}