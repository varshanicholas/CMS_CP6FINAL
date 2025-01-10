using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Controllers
{
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
            if (staffs == null)
            {
                return NotFound("No Staffs found");
            }

            return Ok(staffs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            var staff = await _service.GetStaffById(id);
            if (staff == null)
            {
                return NotFound("No Staff found");
            }

            return Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> InsertPostStaff(Staff staff)
        {
            if (ModelState.IsValid)
            {
                var newStaff = await _service.PostStaff(staff);
                if (newStaff != null)
                {
                    return Ok(newStaff);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertPostStaffReturnId(Staff staff)
        {
            if (ModelState.IsValid)
            {
                var newStaffId = await _service.PostStaffReturnId(staff);
                if (newStaffId != null)
                {
                    return Ok(newStaffId);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Staff>> UpdatePutStaff(int id, Staff staff)
        {
            if (ModelState.IsValid)
            {
                var updatedStaff = await _service.PutStaff(id, staff);
                if (updatedStaff != null)
                {
                    return Ok(updatedStaff);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var result = await _service.DeleteStaff(id);
            if (result.StatusCode == 200)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Value);
        }

        [HttpGet("by-phone/{phoneNumber}")]
        public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        {
            var staff = await _service.GetStaffByPhoneNumber(phoneNumber);
            if (staff == null)
            {
                return NotFound("No Staff found");
            }

            return Ok(staff);
        }




        [HttpGet("by-phone-or-id/{phoneNumber}/{staffId}")]
public async Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId)
{
    var staff = await _service.GetStaffByPhoneNumberOrStaffId(phoneNumber, staffId);
    if (staff == null || staff.Value == null)
    {
        Console.WriteLine("No staff found in controller.");
        return NotFound("No Staff found");
    }

    Console.WriteLine($"Returning staff in controller: {staff.Value.StaffName}");
    return Ok(staff);
}


    }
}
