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
        private readonly IStaffService _staffService;

        // Constructor Injection
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            if (staff == null)
            {
                return BadRequest("Staff data is null.");
            }

            try
            {
                // Call the service to add a new staff record
                var result = await _staffService.PostStaffReturnRecord(staff);

                if (result.Value != null)
                {
                    return CreatedAtAction(nameof(PostStaff), new { id = result.Value.StaffId }, result.Value);
                }

                return BadRequest("Failed to create staff record.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            try
            {
                // Call the service to retrieve all staff records
                var result = await _staffService.GetStaff();

                // Check if the result or its value is null, or if the count is zero
                if (result == null || result.Value == null || !result.Value.Any())
                {
                    return NotFound("No staff records found.");
                }

                return Ok(result.Value);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
