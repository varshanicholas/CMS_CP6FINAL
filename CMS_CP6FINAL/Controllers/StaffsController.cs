using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.ViewModel;
using CMS_CP6FINAL.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class StaffsController : ControllerBase
    {
        // Call repository
        private readonly IStaffRepository _repository;

        // DI Constructor Injection
        public StaffsController(IStaffRepository repository)
        {
            _repository = repository;
        }

        #region 1 - Get All Staff - Search All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaff()
        {
            var staff = await _repository.GetStaff();
            if (staff == null)
            {
                return NotFound("No staff members found");
            }

            return Ok(staff);
        }
        #endregion

        #region 2 - Get Staff by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            var staff = await _repository.GetStaffById(id);
            if (staff == null)
            {
                return NotFound($"Staff with Id = {id} not found");
            }

            return Ok(staff);
        }
        #endregion
        //[HttpPut("{id}")]
        //public async Task<IActionResult> InsertPostStaffReturnRecord(int id, Staff updatedStaff)
        //{
        //    if (id != updatedStaff.StaffId)
        //    {
        //        return BadRequest("Staff ID mismatch");
        //    }

        //    var staff = await _context.Staffs.Include(s => s.Department).FirstOrDefaultAsync(s => s.StaffId == id);
        //    if (staff == null)
        //    {
        //        return NotFound("Staff not found");
        //    }

        //    // Update staff details using validation methods
        //    staff.StaffName = Validations.ValidateStaffName(updatedStaff.StaffName);
        //    staff.Gender = Validations.ValidateGender(updatedStaff.Gender);
        //    staff.PhoneNumber = Validations.ValidatePhoneNumber(updatedStaff.PhoneNumber);
        //    staff.Email = Validations.ValidateEmail(updatedStaff.Email);
        //    staff.Dob = updatedStaff.Dob;
        //    staff.Address = Validations.ValidateAddress(updatedStaff.Address);
        //    staff.Qualification = Validations.ValidateQualification(updatedStaff.Qualification);
        //    staff.DepartmentId = updatedStaff.DepartmentId;
        //    staff.CreatedDate = updatedStaff.CreatedDate;

        //    // Check and set SpecialisationId based on DepartmentId
        //    if (staff.DepartmentId == 17 || staff.DepartmentId == 18 || staff.DepartmentId == 19 || staff.DepartmentId == 20)
        //    {
        //        staff.Department.SpecializationId = 20;
        //    }
        //    else
        //    {
        //        return BadRequest("Invalid DepartmentId for the given SpecialisationId");
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StaffExists(id))
        //        {
        //            return NotFound("Staff not found");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //private bool StaffExists(int id)
        //{
        //    return _context.Staffs.Any(e => e.StaffId == id);
        //}



#region 3 - Insert a Staff member - Return Staff Record
[HttpPost]
public async Task<ActionResult<Staff>> InsertPostStaffReturnRecord(Staff staff)
{
    if (ModelState.IsValid)
    {
        // Validate input fields
        staff.StaffName = Validations.ValidateStaffName();
        staff.Gender = Validations.ValidateGender();
        staff.PhoneNumber = Validations.ValidatePhoneNumber();
        staff.Email = Validations.ValidateEmail();
        staff.Address = Validations.ValidateAddress();
        staff.Qualification = Validations.ValidateQualification();
        staff.CreatedDate = DateTime.Now;

        // Ensure the DepartmentId is set correctly
        var department = await _repository.FindDepartmentById(staff.DepartmentId);
        if (department == null)
        {
            return NotFound("Department not found");
        }

        var newStaff = await _repository.PostStaffReturnRecord(staff);
        if (newStaff != null)
        {
            // Include department details in response
            newStaff.Value.Department = department;
            return Ok(newStaff);
        }
        else
        {
            return NotFound();
        }
    }
    return BadRequest();
}
#endregion


        //#region 4 - Insert a Staff member - Return ID
        //[HttpPost("v1")]
        //public async Task<ActionResult<int>> InsertPostStaffReturnId(Staff staff)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Validate input fields
        //        staff.StaffName = Validations.ValidateStaffName();
        //        staff.Gender = Validations.ValidateGender();
        //        staff.PhoneNumber = Validations.ValidatePhoneNumber();
        //        staff.Email = Validations.ValidateEmail();
        //        staff.Address = Validations.ValidateAddress();
        //        staff.Qualification = Validations.ValidateQualification();

        //        var newStaffId = await _repository.PostStaffReturnId(staff);
        //        if (newStaffId != null)
        //        {
        //            return Ok(newStaffId);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return BadRequest();
        //}
        // #endregion

        //#region 5 - Update Staff - Return Staff Record
        //[HttpPut("{id}")]
        //public async Task<ActionResult<Staff>> UpdatePutStaff(int id, Staff staff)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Validate input fields
        //        staff.StaffName = Validations.ValidateStaffName();
        //        staff.Gender = Validations.ValidateGender();
        //        staff.PhoneNumber = Validations.ValidatePhoneNumber();
        //        staff.Email = Validations.ValidateEmail();
        //        staff.Address = Validations.ValidateAddress();
        //        staff.Qualification = Validations.ValidateQualification();

        //        var updatedStaff = await _repository.PutStaff(id, staff);
        //        if (updatedStaff != null)
        //        {
        //            return Ok(updatedStaff);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return BadRequest();
        //}
        //#endregion

        //#region 6 - Delete Staff
        //[HttpDelete("{id}")]
        //public IActionResult DeleteStaff(int id)
        //{
        //    try
        //    {
        //        var result = _repository.DeleteStaff(id);
        //        if (result != null)
        //        {
        //            // If result indicates failure or null
        //            return NotFound(new
        //            {
        //                success = false,
        //                message = "Staff could not be deleted or not found"
        //            });
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception in real-world scenarios
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new { success = false, message = "An unexpected error occurred" });
        //    }
        //}
        //#endregion

        //#region 7 - Get All Departments
        //[HttpGet("v2")]
        //public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        //{
        //    var departments = await _repository.GetDepartments();
        //    if (departments == null)
        //    {
        //        return NotFound("No departments found");
        //    }

        //    return Ok(departments);
        //}
        //#endregion

        //#region 8 - Insert Staff using Stored Procedure - Return Staff Record
        //[HttpPost("p1")]
        //public async Task<ActionResult<IEnumerable<Staff>>> InsertPostStaffByProcedureReturnRecord(Staff staff)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newStaff = await _repository.PostStaffByProcedureReturnRecord(staff);
        //        if (newStaff != null)
        //        {
        //            return Ok(newStaff);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return BadRequest();
        //}
        //#endregion

        //#region 9 - Get Staff by Phone Number
        //[HttpGet("phone/{phoneNumber}")]
        //public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        //{
        //    var staff = await _repository.GetStaffByPhoneNumber(phoneNumber);
        //    if (staff == null)
        //    {
        //        return NotFound($"Staff with phone number = {phoneNumber} not found");
        //    }

        //    return Ok(staff);
        //}
        //#endregion

        //#region 10 - Get Staff by StaffId
        //[HttpGet("staffid/{staffId}")]
        //public async Task<ActionResult<Staff>> GetStaffByStaffId(int staffId)
        //{
        //    var staff = await _repository.GetStaffByStaffId(staffId);
        //    if (staff == null)
        //    {
        //        return NotFound($"Staff with Id = {staffId} not found");
        //    }

        //    return Ok(staff);
        //}
        //#endregion

        //#region 11 - Get Doctor ViewModel by Staff Id
        //[HttpGet("doctorviewmodel/{staffId}")]
        //public async Task<ActionResult<DoctorViewModel>> GetDoctorViewModelById(int staffId)
        //{
        //    var doctorViewModel = await _repository.GetDoctorViewModelById(staffId);
        //    if (doctorViewModel == null)
        //    {
        //        return NotFound($"Staff with Id = {staffId} not found");
        //    }

        //    return Ok(doctorViewModel);
        //}
        //#endregion

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<StaffViewModel>>> GetStaffView()
        //{
        //    return await _repository.GetStaffView();
        //}



        //[HttpGet("{id}")]
        //public async Task<ActionResult<StaffViewModel>> GetStaffViewById(int id)
        //{
        //    return await _repository.GetStaffViewById(id);
        //}

        //[HttpPost]
        //public async Task<ActionResult<int>> PostStaffViewReturnId([FromBody] Staff staff)
        //{
        //    return await _repository.PostStaffViewReturnId(staff);
        //}

        //[HttpPost("returnRecord")]
        //public async Task<ActionResult<StaffViewModel>> PostStaffViewReturnRecord([FromBody] Staff staff)
        //{
        //    return await _repository.PostStaffViewReturnRecord(staff);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<StaffViewModel>> PutStaffView(int id, [FromBody] Staff staff)
        //{
        //    return await _repository.PutStaffView(id, staff);
        //}

        //[HttpDelete("{id}")]
        //public JsonResult DeleteStaffView(int id)
        //{
        //    return _repository.DeleteStaffView(id);
        //}

        //[HttpPost("byProcedure")]
        //public async Task<ActionResult<IEnumerable<StaffViewModel>>> PostStaffViewByProcedureReturnRecord([FromBody] Staff staff)
        //{
        //    return await _repository.PostStaffViewByProcedureReturnRecord(staff);
        //}

        //[HttpGet("byPhoneNumber/{phoneNumber}")]
        //public async Task<ActionResult<StaffViewModel>> GetStaffViewByPhoneNumber(string phoneNumber)
        //{
        //    return await _repository.GetStaffViewByPhoneNumber(phoneNumber);
        //}

        //[HttpGet("byStaffId/{staffId}")]
        //public async Task<ActionResult<StaffViewModel>> GetStaffViewByStaffId(int staffId)
        //{
        //    return await _repository.GetStaffViewByStaffId(staffId);
        //}
    }
}
