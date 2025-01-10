//using CMS_CP6FINAL.Model;
//using CMS_CP6FINAL.ViewModel;
//using CMS_CP6FINAL.Utility;
//using CMS_CP6FINAL.Utility;
//using CMS_CP6FINAL.Repository;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;

//namespace CMS_CP6FINAL.Service
//{
//    public class StaffService : IStaffService
//    {
//        private readonly IStaffRepository _staffRepository;

//        // Constructor Injection
//        public StaffService(IStaffRepository staffRepository)
//        {
//            _staffRepository = staffRepository;
//        }

//        // Insert
//        public async Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff)
//        {
//            // Apply validation logic for staff properties
//            staff.StaffName = Validations.ValidateStaffName();   // Validate staff name
//            staff.Gender = Validations.ValidateGender();         // Validate gender
//            staff.PhoneNumber = Validations.ValidatePhoneNumber(); // Validate phone number
//            staff.Email = Validations.ValidateEmail();           // Validate email
//            staff.Address = Validations.ValidateAddress();       // Validate address
//            staff.Qualification = Validations.ValidateQualification(); // Validate qualification

//            await _staffRepository.PostStaffReturnRecord(staff);
//            return new ActionResult<Staff>(staff);
//        }

//        // Get All Staff
//        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
//        {
//            return await _staffRepository.GetStaff();
//        }
//    }
//}
