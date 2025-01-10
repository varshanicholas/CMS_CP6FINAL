
﻿using CMS_CP6FINAL.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMS_CP6FINAL.Utility

﻿//using CMS_CP6FINAL.Model;
//using System;
//using System.Text.RegularExpressions;

//namespace CMS_CP6FINAL.Utility
//{
//    public class Validations
//    {
//        public static string ValidateInput(string pattern, string errorMessage)
//        {
//            string input;
//            while (true)
//            {
//                input = Console.ReadLine();
//                if (Regex.IsMatch(input, pattern))
//                {
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine(errorMessage);
//                    Console.Write("Please enter again: ");
//                }
//            }
//            return input;
//        }

//        public static string ValidateStaffName()
//        {
//            string pattern = @"^[A-Za-z]+( [A-Za-z]+)*$";
//            string errorMessage = "Invalid Staff Name (Letters and spaces only, must not start or end with space).";
//            return ValidateInput(pattern, errorMessage);
//        }

//        public static string ValidateGender()
//        {
//            string pattern = @"^(Male|Female)$";
//            string errorMessage = "Invalid Gender (Accept either Male or Female).";
//            return ValidateInput(pattern, errorMessage);
//        }

//        public static string ValidatePhoneNumber()
//        {
//            string pattern = @"^[6-9]\d{9}$";
//            string errorMessage = "Invalid Phone Number (digits only, must contain 10 digits and starting digit can only be between 6 to 9).";
//            return ValidateInput(pattern, errorMessage);
//        }

//        public static string ValidateEmail()
//        {
//            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,20}$";
//            string errorMessage = "Invalid Email (letters and numbers only, must contain an @ symbol and be up to 20 characters).";
//            return ValidateInput(pattern, errorMessage);
//        }

//        public static string ValidateAddress()
//        {
//            string pattern = @"^[A-Za-z0-9][A-Za-z0-9\s,]*[A-Za-z0-9]$";
//            string errorMessage = "Invalid Address (letters, spaces, digits, and commas allowed; must start and end with number or letter).";
//            return ValidateInput(pattern, errorMessage);
//        }

//        public static string ValidateQualification()
//        {
//            string pattern = @"^[A-Za-z]+( [A-Za-z]+)*$";
//            string errorMessage = "Invalid Qualification (Letters and spaces only, must not start or end with space).";
//            return ValidateInput(pattern, errorMessage);
//        }

//        public static decimal ValidateConsultationFee()
//        {
//            decimal fee;
//            while (true)
//            {
//                string input = Console.ReadLine();
//                if (decimal.TryParse(input, out fee) && fee > 0)
//                {
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine("Invalid Consultation Fee (must be a positive decimal number).");
//                    Console.Write("Please enter again: ");
//                }
//            }
//            return fee;
//        }



//        public static string ValidateDepartmentName(string departmentName)
//        {
//            // Example validation logic: Ensure the department name is not null or empty and has a minimum length
//            if (string.IsNullOrWhiteSpace(departmentName))
//            {
//                throw new ArgumentException("Department name cannot be null or empty.");
//            }

//            if (departmentName.Length < 3)
//            {
//                throw new ArgumentException("Department name must be at least 3 characters long.");
//            }

//            return departmentName;
//        }
//        public static int ValidateSpecializationId(int? specializationId)
//        {
//            // Example validation logic for specialization ID
//            if (specializationId == null || specializationId <= 0)
//            {
//                throw new ArgumentException("Specialization ID must be a positive integer.");
//            }

//            return specializationId.Value;
//        }
//    }
//}

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
    }
}
