using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CP6FINAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientHistoryController : ControllerBase
    {
        private readonly IPatientHistoryDoctorRepository _repository;

        public PatientHistoryController(IPatientHistoryDoctorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetPatientHistory/{patientId}")]
        public async Task<IActionResult> GetPatientHistory(int patientId)
        {
            var history = await _repository.GetPatientHistoryAsync(patientId);

            if (history == null || !history.Any())
            {
                return NotFound("No history found for the specified patient.");
            }

            return Ok(history);
        }

    }


}
