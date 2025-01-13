using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public interface IStaffRepository
    {
        Task<ActionResult<IEnumerable<Staff>>> GetStaffs();
        Task<ActionResult<Staff>> GetStaffById(int id);
        Task<ActionResult<Staff>> PostStaff(Staff staff);
        Task<ActionResult<int>> PostStaffReturnId(Staff staff);
        Task<ActionResult<Staff>> PutStaff(int id, Staff staff);
        JsonResult DeleteStaff(int id);
        Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber);
        Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId);

        //   Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId);
    }
}