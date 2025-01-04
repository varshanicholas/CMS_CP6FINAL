using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewPatientAppoinmentController : ControllerBase
    {
        private readonly IViewPatientAppoinmentRepository _repository;

        public ViewPatientAppoinmentController(IViewPatientAppoinmentRepository repository)
        {
            _repository = repository;
        }

        // Endpoint to fetch today's appointments for a specific doctor
        [HttpGet("todaysAppointments/{doctorId}")]
        public async Task<IActionResult> GetTodaysAppointments(int doctorId)
        {
            try
            {
                var appointments = await _repository.GetTodaysAppointmentsAsync(doctorId);

                if (appointments == null || !appointments.Any())
                {
                    return NotFound(new { Message = "No appointments found for today." });
                }

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching today's appointments: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        // Endpoint to search for a patient
        [HttpGet("search")]
        public IActionResult SearchPatient([FromQuery] string? patientName, [FromQuery] int? appointmentId, [FromQuery] int? tokenNumber, [FromQuery] string? phoneNumber)
        {
            try
            {
                var results = _repository.SearchPatient(patientName, appointmentId, tokenNumber, phoneNumber);

                if (results == null || !results.Any())
                {
                    return NotFound(new { Message = "No records found matching the search criteria." });
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}
