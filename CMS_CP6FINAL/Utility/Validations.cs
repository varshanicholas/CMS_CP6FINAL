using CMS_CP6FINAL.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
            // Validate name (No digits or special characters except space)
            if (string.IsNullOrWhiteSpace(staff.StaffName) || 
                !Regex.IsMatch(staff.StaffName, @"^[a-zA-Z\s]+$"))
            {
                throw new ArgumentException("Staff name must contain only alphabets and spaces.");
            }

            // Validate phone number (10 digits only)
            if (string.IsNullOrWhiteSpace(staff.PhoneNumber) || 
                !Regex.IsMatch(staff.PhoneNumber, @"^\d{10}$"))
            {
                throw new ArgumentException("PhoneNumber must be exactly 10 digits.");
            }

            // Validate email (No special symbols except allowed ones)
            if (string.IsNullOrWhiteSpace(staff.Email) || 
                !Regex.IsMatch(staff.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                throw new ArgumentException("Invalid Email format.");
            }

            // Validate Date of Birth (At least 18 years old)
            if (staff.Dob == default || CalculateAge(staff.Dob) < 18)
            {
                throw new ArgumentException("Staff must be at least 18 years old.");
            }

            // Validate address
            if (string.IsNullOrWhiteSpace(staff.Address))
            {
                throw new ArgumentException("Address is required.");
            }

            // Validate qualification
            if (string.IsNullOrWhiteSpace(staff.Qualification))
            {
                throw new ArgumentException("Qualification is required.");
            }

            return true;
        }

        public static bool IsValid(this Doctor doctor)
        {
            if (doctor.StaffId == 0)
            {
                throw new ArgumentException("Staff ID is required.");
            }
            if (doctor.ConsultationFee <= 0)
            {
                throw new ArgumentException("Consultation Fee should be greater than zero.");
            }
            if (doctor.SpecializationId == 0)
            {
                throw new ArgumentException("Specialization ID is required.");
            }
            return true;
        }

        private static int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age)) age--;

            return age;
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
