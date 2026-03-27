using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.Utility;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                // Validate staff object
                if (staff.IsValid())
                {
                    return await _repository.PostStaff(staff);
                }
            }
            catch (ArgumentException ex)
            {
                // Log or handle validation exceptions
                return new BadRequestObjectResult(new { message = ex.Message });
            }

            // If validation fails, return an error response
            return new BadRequestResult();
        }

        public async Task<ActionResult<int>> PostStaffReturnId(Staff staff)
        {
            try
            {
                // Validate staff object
                if (staff.IsValid())
                {
                    return await _repository.PostStaffReturnId(staff);
                }
            }
            catch (ArgumentException ex)
            {
                // Log or handle validation exceptions
                return new BadRequestObjectResult(new { message = ex.Message });
            }

            // If validation fails, return an error response
            return new BadRequestResult();
        }

        public async Task<ActionResult<Staff>> PutStaff(int id, Staff staff)
        {
            try
            {
                // Validate staff object
                if (staff.IsValid())
                {
                    return await _repository.PutStaff(id, staff);
                }
            }
            catch (ArgumentException ex)
            {
                // Log or handle validation exceptions
                return new BadRequestObjectResult(new { message = ex.Message });
            }

            // If validation fails, return an error response
            return new BadRequestResult();
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
