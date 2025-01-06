using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        //call repository

        private readonly IReceptionistRepository _repository;


        //Dependency injuction DI ---Constructor Instrucor

        public AppointmentController(IReceptionistRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("d2")]
        //[Authorize(AuthenticationSchemes  ="Bearer")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var dep = await _repository.GetAllDepartment();
            if (dep == null)
            {
                return NotFound("No department found");
            }

            return Ok(dep);
        }

        // GET: api/DoctorAvailability/byDoctor/{doctorId}
        [HttpGet("byDoctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<DoctorAvailability>>> GetDoctorsAvailabilityByDoctorId(int doctorId)
        {
            var availability = await _repository.GetDoctorAvailabilityByDoctorId(doctorId);

            if (availability == null || !availability.Any())
            {
                return NotFound(new { Message = "Doctor availability not found." });
            }

            var response = availability.Select(da => new
            {
                da.DocAvlId,
                da.DoctorId,
               
                da.Weekdays,
                da.MorningSession,
                da.EveningSession
            });

            return Ok(response);
        }
    
       

        //[HttpGet("byDepartmentandDate/{departmentId}/{date}")]
        //public async Task<List<DoctorAvailability>> GetAvailableDoctorsByDepartmentAndDate(int departmentId, [FromRoute] string date)
        //{
        //    var appointmentDate = DateTime.Parse(date);
        //    var availableDoctors = await _repository.GetDoctorAvailabilityWeekAndDepartmentId(departmentId, appointmentDate);
        //    return availableDoctors.ToList();
        //}

        [HttpPost("SaveAppointment")]
        public async Task<NewAppointment> SaveNewAppointment(NewAppointment newAppointment)
        {
            return await _repository.SaveAppointment(newAppointment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultationFeesByDoctorId(int id)
        {
            try
            {
                // Call the repository method to get the consultation fee
                var consultationFee = await _repository.GetConsultationFeeByDoctorId(id);

                // Check if the consultation fee was found
                if (consultationFee.HasValue)
                {
                    return Ok(consultationFee.Value);
                }

                // Return NotFound if no fee is associated with the doctor ID
                return NotFound("Consultation fee not found for the given doctor ID.");
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error: {ex.Message}");

                // Return a generic error response
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("doctors/{departmentId}")]
      
        public async Task<ActionResult<IEnumerable<string>>> GetDoctorsNameByDepartmentId(int departmentId)
        {
            try
            {
                var staffNames = await _repository.GetDoctorsByDepartmentId(departmentId);

                // Return 200 OK with the list of doctor names
                return Ok(staffNames);
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error: {ex.Message}");

                // Return a 500 Internal Server Error with a message
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

    }
}
