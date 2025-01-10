using Microsoft.AspNetCore.Mvc;
using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.ViewModel;

namespace CMS_CP6FINAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewPatientAppointmentController : ControllerBase
    {
        private readonly IViewPatientAppoinmentRepository _repository;

        public ViewPatientAppointmentController(IViewPatientAppoinmentRepository repository)
        {
            _repository = repository;
        }

        // Get today's appointments for a doctor
        [HttpGet("today/{doctorId}")]
        public async Task<IActionResult> GetTodaysAppointments(int doctorId)
        {
            var appointments = await _repository.GetTodaysAppointmentsAsync(doctorId);
            if (appointments == null || !appointments.Any())
            {
                return NotFound("No appointments found for today.");
            }
            return Ok(appointments);
        }

        [HttpGet("search")]
        public IActionResult SearchPatient([FromQuery] string? patientName, [FromQuery] int? appointmentId, [FromQuery] int? tokenNumber, [FromQuery] string? phoneNumber)
        {
            Console.WriteLine($"Search Parameters: patientName={patientName}, appointmentId={appointmentId}, tokenNumber={tokenNumber}, phoneNumber={phoneNumber}");

            var result = _repository.SearchPatient(patientName, appointmentId, tokenNumber, phoneNumber);

            Console.WriteLine($"Search Results: {result.Count()} records found.");

            if (result == null || !result.Any())
            {
                return NotFound("No patients match the search criteria.");
            }

            return Ok(result);
        }

    }
}
