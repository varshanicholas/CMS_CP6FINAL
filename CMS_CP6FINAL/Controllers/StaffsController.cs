using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Service;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;

        public StaffController(IStaffService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaffs()
        {
            var staffs = await _service.GetStaffs();
            if (staffs == null || !staffs.Value.Any())
            {
                return NotFound(new { message = "No Staffs found" });
            }

            return Ok(staffs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            var staff = await _service.GetStaffById(id);
            if (staff == null || staff.Value == null)
            {
                return NotFound(new { message = "Staff not found" });
            }

            return Ok(staff.Value);
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> InsertPostStaff([FromBody] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newStaff = await _service.PostStaff(staff);
                return CreatedAtAction(nameof(GetStaffById), new { id = newStaff.Value.StaffId }, newStaff.Value);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertPostStaffReturnId([FromBody] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newStaffId = await _service.PostStaffReturnId(staff);
                return Ok(new { id = newStaffId.Value });
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Staff>> UpdatePutStaff(int id, [FromBody] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedStaff = await _service.PutStaff(id, staff);
                if (updatedStaff.Value == null)
                {
                    return NotFound(new { message = "Staff not found for update" });
                }

                return Ok(updatedStaff.Value);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var result = await _service.DeleteStaff(id);
            if (result.StatusCode == 200)
            {
                return Ok(new { message = "Staff deleted successfully" });
            }

            return NotFound(new { message = "Staff not found for deletion" });
        }

        [HttpGet("by-phone/{phoneNumber}")]
        public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 10)
            {
                return BadRequest(new { message = "Invalid phone number" });
            }

            var staff = await _service.GetStaffByPhoneNumber(phoneNumber);
            if (staff == null || staff.Value == null)
            {
                return NotFound(new { message = "Staff not found" });
            }

            return Ok(staff.Value);
        }

        [HttpGet("by-phone-or-id/{phoneNumber}/{staffId}")]
        public async Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 10)
            {
                return BadRequest(new { message = "Invalid phone number" });
            }

            var staff = await _service.GetStaffByPhoneNumberOrStaffId(phoneNumber, staffId);
            if (staff == null || staff.Value == null)
            {
                return NotFound(new { message = "Staff not found" });
            }

            return Ok(staff.Value);
        }

        [HttpGet("vm")]
        public async Task<ActionResult<IEnumerable<StaffDeptViewModel>>> GetAllStaffsByViewModel()
        {
            var staffs = await _service.GetAllStaffsByViewModel();
            if (staffs == null || !staffs.Value.Any())
            {
                return NotFound(new { message = "No staff found" });
            }

            return Ok(staffs.Value);
        }
    }
}
