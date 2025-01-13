using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS_CP6FINAL.ViewModel;
using CMS_CP6FINAL.Utility;

namespace CMS_CP6FINAL.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly CmsCamp6finalContext _context;

        public StaffRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
        {
            return await _context.Staff
                                 .Include(staff => staff.Department)
                                 .ToListAsync();
        }

        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            return await _context.Staff
                                 .Include(staff => staff.Department)
                                 .FirstOrDefaultAsync(staff => staff.StaffId == id);
        }

        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentNullException(nameof(staff), "Staff data is null");
            }
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return await _context.Staff
                                 .Include(s => s.Department)
                                 .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
        }

        public async Task<ActionResult<int>> PostStaffReturnId(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentNullException(nameof(staff), "Staff data is null");
            }
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return staff.StaffId;
        }

        public async Task<ActionResult<Staff>> PutStaff(int id, Staff staff)
        {
            var existingStaff = await _context.Staff.FindAsync(id);
            if (existingStaff == null)
            {
                return null;
            }

            existingStaff.StaffName = staff.StaffName;
            existingStaff.Gender = staff.Gender;
            existingStaff.PhoneNumber = staff.PhoneNumber;
            existingStaff.Email = staff.Email;
            existingStaff.Dob = staff.Dob;
            existingStaff.Address = staff.Address;
            existingStaff.Qualification = staff.Qualification;
            existingStaff.DepartmentId = staff.DepartmentId;
            existingStaff.CreatedDate = staff.CreatedDate;
            existingStaff.IsActive = staff.IsActive;

            await _context.SaveChangesAsync();
            return await _context.Staff
                                 .Include(s => s.Department)
                                 .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
        }

        public async Task<JsonResult> DeleteStaff(int id)
        {
            var existingStaff = _context.Staff.Find(id);
            if (existingStaff == null)
            {
                return new JsonResult(new { success = false, message = "Staff not found" }) { StatusCode = 404 };
            }

            existingStaff.IsActive = false;
            _context.SaveChanges();
            return new JsonResult(new { success = true, message = "Staff marked as inactive" }) { StatusCode = 200 };
        }

        public async Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId)
        {
            return await _context.Staff
                         .Include(staff => staff.Department)
                         .FirstOrDefaultAsync(staff => staff.PhoneNumber == phoneNumber || staff.StaffId == staffId);
        }

        public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        {
            return await _context.Staff
                                 .Include(staff => staff.Department)
                                 .FirstOrDefaultAsync(staff => staff.PhoneNumber == phoneNumber);
        }



        
public async Task<ActionResult<IEnumerable<StaffDeptViewModel>>> GetViewModelStaffs()
{
    try
    {
        if (_context != null)
        {
            return await (from s in _context.Staff
                          join d in _context.Departments
                          on s.DepartmentId equals d.DepartmentId
                          select new StaffDeptViewModel
                          {
                              StaffId = s.StaffId,
                              StaffName = s.StaffName,
                              Gender = s.Gender,
                              PhoneNumber = s.PhoneNumber,
                              Email = s.Email,
                              Address = s.Address,
                              Qualification = s.Qualification,
                              Dob = s.Dob,
                              DepartmentName = d.DepartmentName,
                              CreatedDate = s.CreatedDate,
                              IsActive = s.IsActive
                          }).ToListAsync();
        }
        return new List<StaffDeptViewModel>();
    }
    catch (Exception ex)
    {
        return null;
    }
}


    }
}
