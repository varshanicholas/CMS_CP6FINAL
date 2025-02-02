
﻿using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Service
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _repository;

        public StaffService(IStaffRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
        {
            return await _repository.GetStaffs();
        }

        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            return await _repository.GetStaffById(id);
        }

        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            return await _repository.PostStaff(staff);
        }

        public async Task<ActionResult<int>> PostStaffReturnId(Staff staff)
        {
            return await _repository.PostStaffReturnId(staff);
        }

        public async Task<ActionResult<Staff>> PutStaff(int id, Staff staff)
        {
            return await _repository.PutStaff(id, staff);
        }

        public async Task<JsonResult> DeleteStaff(int id)
        {
            return await Task.Run(() => _repository.DeleteStaff(id));
        }

        public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        {
            return await _repository.GetStaffByPhoneNumber(phoneNumber);
        }

        public async Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId)
        {
            return await _repository.GetStaffByPhoneNumberOrStaffId(phoneNumber, staffId);
        }

        
    public async Task<ActionResult<IEnumerable<StaffDeptViewModel>>> GetAllStaffsByViewModel()
    {
        return await _repository.GetViewModelStaffs();
    }

       




    }
}

﻿//using CMS_CP6FINAL.Model;
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
