using System.Collections.Generic;
using System.Threading.Tasks;
using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CMS_CP6FINAL.Service
{
    public interface IStaffService
    {
        Task<ActionResult<IEnumerable<Staff>>> GetStaffs();
        Task<ActionResult<Staff>> GetStaffById(int id);
        Task<ActionResult<Staff>> PostStaff(Staff staff);
        Task<ActionResult<int>> PostStaffReturnId(Staff staff);
        Task<ActionResult<Staff>> PutStaff(int id, Staff staff);
        Task<JsonResult> DeleteStaff(int id);
        Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber);
        Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId);

          Task<ActionResult<IEnumerable<StaffDeptViewModel>>> GetAllStaffsByViewModel();
        
        }
}
