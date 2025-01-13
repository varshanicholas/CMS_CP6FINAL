using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.IO;
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

        //[HttpPost("SaveAppointment")]
        //public async Task<NewAppointment> SaveNewAppointment(NewAppointment newAppointment)
        //{
        //    //return await _repository.SaveAppointment(newAppointment);


        //    if (ModelState.IsValid)
        //    {
        //        //insert a new record and return as an object named patient

        //        var newAppointmentsDetails = await _repository.SaveAppointment(newAppointment);

        //        if (newAppointmentsDetails != null)
        //        {
        //            return Ok(newAppointmentsDetails);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }

        //    }
        //    return BadRequest();
        //}


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
      
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsNameByDepartmentId(int departmentId)
        {
            try
            {
                var doctors = await _repository.GetDoctorsByDepartmentId(departmentId);

                // Return 200 OK with the list of doctor names
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error: {ex.Message}");

                // Return a 500 Internal Server Error with a message
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        // Add to your ReceptionistController

        // Get System Date and Total Appointment Count by DoctorId
        [HttpGet("SystemDateAndTotalAppointments/{doctorId}")]
        public async Task<ActionResult> GetSystemDateAndTotalAppointments(int doctorId)
        {
            try
            {
                // Fetch total appointment count for the doctor
                var totalAppointments = await _repository.GetTotalAppointmentsByDoctorId(doctorId);

                // Get the system date
                var systemDate = DateTime.Now;

                // Add 1 to the total appointments count for the new token
                var totalWithNewToken = totalAppointments + 1;

                var result = new
                {
                    SystemDate = systemDate,
                    TotalAppointments = totalWithNewToken
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurred", error = ex.Message });
            }
        }

        //[HttpPost("book")]
        //public async Task<IActionResult> BookAppointments([FromBody] NewAppointment appointment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Ensure required fields are present
        //    if (appointment.DoctorId == 0 || appointment.AppointmentDate == null)
        //    {
        //        return BadRequest(new { message = "DoctorId and AppointmentDate are required." });
        //    }

        //    // Set the CreatedDate
        //    appointment.CreatedDate = DateTime.Now;

        //    try
        //    {
        //        var savedAppointment = await _repository.AddAppointment(appointment);
        //        if (savedAppointment == null)
        //        {
        //            return StatusCode(500, new { message = "Failed to save appointment." });
        //        }

        //        // Return success response with the saved appointment details
        //        return Ok(new { message = "Appointment booked successfully", appointmentId = savedAppointment.AppointmentId });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return error response if something goes wrong
        //        return StatusCode(500, new { message = "An error occurred while booking the appointment", error = ex.Message });
        //    }
        //}




        [HttpPost("Book")]
        public async Task<IActionResult> BookAppointment([FromBody] NewAppointment appointment)
        {
            if (appointment.DoctorId == null || appointment.DepartmentId == null ||
                appointment.AppointmentDate == null || appointment.PatientId == null)
            {
                return BadRequest(new { success = false, message = "Missing required fields: DoctorId, DepartmentId, AppointmentDate, PatientId" });
            }

            try
            {
                var bookedAppointment = await _repository.BookAppointment(appointment);

                return Ok(new
                {
                    success = true,
                    message = "Appointment booked successfully",
                    data = new
                    {
                        bookedAppointment.AppointmentId,
                        bookedAppointment.DoctorId,
                        bookedAppointment.DepartmentId,
                        bookedAppointment.AppointmentDate,
                        bookedAppointment.PatientId,
                        bookedAppointment.TokenNumber
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An error occurred while booking the appointment", error = ex.Message });
            }
        }


        [HttpPost("bkAppointment")]
        public async Task<ActionResult<IEnumerable<NewAppointment>>> PostAppointmentByProcedure(NewAppointment appointment)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Attempt to insert a new appointment record and return the result
                var newAppointment = await _repository.PostNewAppointmentByProcedure(appointment);

                if (newAppointment != null)
                {
                    return Ok(newAppointment); // Return the created appointment records
                }
                else
                {
                    return NotFound("No appointment could be created or found."); // Handle the case where no data is returned
                }
            }

            // Return bad request if model state is invalid
            return BadRequest("Invalid appointment data provided.");
        }





    }
}
