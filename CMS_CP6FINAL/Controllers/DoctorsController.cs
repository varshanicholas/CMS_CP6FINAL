using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorsController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _service.GetDoctors();
            if (doctors == null)
            {
                return NotFound("No Doctors found");
            }

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            var doctor = await _service.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound("No Doctor found");
            }

            return Ok(doctor);
        }
        [HttpPost]
        public async Task<ActionResult<Doctor>> InsertPostDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var newDoctor = await _service.PostDoctor(doctor);
                if (newDoctor != null)
                {
                    return Ok(newDoctor);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertPostDoctorReturnId(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var newDoctorId = await _service.PostDoctorReturnId(doctor);
                if (newDoctorId != null)
                {
                    return Ok(newDoctorId);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> UpdatePutDoctor(int id, Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var updatedDoctor = await _service.PutDoctor(id, doctor);
                if (updatedDoctor != null)
                {
                    return Ok(updatedDoctor);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _service.DeleteDoctor(id);
            if (result.StatusCode == 200)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Value);
        }

        [HttpGet("by-phone/{phoneNumber}")]
        public async Task<ActionResult<Doctor>> GetDoctorByPhoneNumber(string phoneNumber)
        {
            var doctor = await _service.GetDoctorByPhoneNumber(phoneNumber);
            if (doctor == null)
            {
                return NotFound("No Doctor found");
            }

            return Ok(doctor);
        }

        [HttpGet("by-phone-or-id/{phoneNumber}/{doctorId}")]
        public async Task<ActionResult<Doctor>> GetDoctorByPhoneNumberOrDoctorId(string phoneNumber, int doctorId)
        {
            var doctor = await _service.GetDoctorByPhoneNumberOrDoctorId(phoneNumber, doctorId);
            if (doctor == null || doctor.Value == null)
            {
                Console.WriteLine("No doctor found in controller.");
                return NotFound("No Doctor found");
            }

            Console.WriteLine($"Returning doctor in controller: {doctor.Value.Staff.StaffName}");
            return Ok(doctor);
        }
    }
}
