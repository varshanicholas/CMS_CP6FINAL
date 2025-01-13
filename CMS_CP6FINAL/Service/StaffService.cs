using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // Ensure proper asynchronous handling
            return await Task.Run(() => _repository.DeleteStaff(id));
        }

        public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        {
            return await _repository.GetStaffByPhoneNumber(phoneNumber);
        }

        public async Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId)
        {
            var staff = await _repository.GetStaffByPhoneNumberOrStaffId(phoneNumber, staffId);
            if (staff == null || staff.Value == null)
            {
                Console.WriteLine("No staff found in service.");
                return new ActionResult<Staff>((Staff)null); // Explicitly specify the type
            }
            else
            {
                Console.WriteLine($"Found staff in service: {staff.Value.StaffName}");
            }
            return staff;
        }


    }
}