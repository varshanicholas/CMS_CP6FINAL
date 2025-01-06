using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : ControllerBase
        {
            //call repository

            private readonly IReceptionistRepository  _repository;


            //Dependency injuction DI ---Constructor Instrucor

            public ReceptionistController (IReceptionistRepository repository)
            {
                _repository = repository;
            }


        [HttpGet("Patientslist")]
        //[Authorize(AuthenticationSchemes  ="Bearer")]
        public async Task<ActionResult<IEnumerable<Patient >>> GetAllPatients()
        {
            var pat = await _repository.GetPatient();
            if (pat == null)
            {
                return NotFound("No patients found");
            }

            return Ok(pat);
        }

        // Search by ID
        [HttpGet("PatientById/{id}")]
        public async Task<ActionResult<Patient>> GetPatientsById(int id)
        {
            var pat = await _repository.GetPatientById(id);
            if (pat == null)
            {
                return NotFound("No Patients found");
            }

            return Ok(pat);
        }

        // Search by Phone Number
        [HttpGet("PatientsByPhone/{ph}")]
        public async Task<ActionResult<Patient>> GetPatientsByphno(string ph)
        {
            var pat = await _repository.GetPatientByPhoneNumber(ph);
            if (pat == null)
            {
                return NotFound("No Patients found");
            }

            return Ok(pat);
        }



        #region -4 insert an patient-return patient record

        [HttpPost("InsertPatient")]
        public async Task<ActionResult<Patient >> InsertPatientsReturnRecord (Patient pat)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named patient

                var newPatient = await _repository.PostPatientReturnRecord(pat);

                if (newPatient != null)
                {
                    return Ok(newPatient);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }


        #endregion

        #region 6 Update an patient

        [HttpPut("updatepatient/{id}")]
        public async Task<ActionResult<Patient>> UpdatePatientReturnRecord(int id, Patient  pat)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named patient

                var updatePatient = await _repository.PutPatient(id, pat);

                if (updatePatient != null)
                {
                    return Ok(updatePatient);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }


        #endregion

        #region 7 delete an patient
        [HttpDelete("patientdelete/{id}")]
        public IActionResult DeletePatient(int id)
        {
            try
            {
                var result = _repository.DeletePatient(id);

                if (result == null)
                {


                    //if result indicates failure or null
                    return NotFound(new
                    {
                        success = false,
                        message = "patients could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion
    }
}

