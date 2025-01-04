using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.ViewModel;

namespace CMS_CP6FINAL.Utility
{
  
    //public static class MappingHelpers
    //{
    //    public static StaffViewModel MapToViewModel(Staff staff)
        //{
        //    return new StaffViewModel
        //    {
        //        StaffId = staff.StaffId,
        //        StaffName = staff.StaffName,
        //        Gender = staff.Gender,
        //        PhoneNumber = staff.PhoneNumber,
        //        Email = staff.Email,
        //        Dob = staff.Dob,
        //        Address = staff.Address,
        //        Qualification = staff.Qualification,
        //        DepartmentName = staff.Department.DepartmentName,
        //        CreatedDate = staff.CreatedDate,
        //        SpecializationName = staff.Department.SpecializationId == 6 ? "Other" : staff.Department.Specialization.SpecializationName
        //    };

        public static class MappingHelpers
        {
            public static StaffViewModel MapToViewModel(Staff staff)
            {
                return new StaffViewModel
                {
                    StaffId = staff.StaffId,
                    StaffName = staff.StaffName,
                    Gender = staff.Gender,
                    PhoneNumber = staff.PhoneNumber,
                    Email = staff.Email,
                    Dob = staff.Dob,
                    Address = staff.Address,
                    Qualification = staff.Qualification,
                    DepartmentId = staff.DepartmentId,
                    CreatedDate = staff.CreatedDate
                };
            }
        }
    }
    

