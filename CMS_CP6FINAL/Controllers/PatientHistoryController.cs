using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repositories;
using CMS_CP6FINAL.ViewModel;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHistoryDoctorController : ControllerBase
    {
        private readonly IPatientHistoryDoctorRepository _repository;

        public PatientHistoryDoctorController(IPatientHistoryDoctorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("PatientHistory/{patientId}")]
        public async Task<ActionResult<List<PatientHistoryViewModel>>> GetPatientHistory(int patientId)
        {
            var history = await _repository.GetPatientHistoryAsync(patientId);
            if (history == null || history.Count == 0)
            {
                return NotFound();
            }
            return Ok(history);
        }
    }
}
