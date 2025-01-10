using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DoctorStartConsultationController : ControllerBase
{
    private readonly IDoctorStartConsultationRepository _repository;

    public DoctorStartConsultationController(IDoctorStartConsultationRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("start-consultation")]
    [AllowAnonymous]
    public async Task<IActionResult> StartConsultation([FromBody] DoctorStartConsultationViewModel consultation)
    {
        
        if (consultation == null)
            return BadRequest("Consultation data is required.");

        //Check for null or empty fields in the consultation object
        if (string.IsNullOrWhiteSpace(consultation.Symptoms))
                return BadRequest("Symptoms are required.");

        if (string.IsNullOrWhiteSpace(consultation.Diagnosis))
            return BadRequest("Diagnosis is required.");

        if (string.IsNullOrWhiteSpace(consultation.DoctorNotes))
            return BadRequest("Doctor notes are required.");

        try
        {
            //    // Get the doctor ID (createdBy) from the authenticated user
            //var createdBy = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            int createdBy = consultation.CreatedBy != 0 ? consultation.CreatedBy : 1000;

            var result = await _repository.AddConsultationAsync(consultation, createdBy);

            return Ok(result); // Return the same consultation object in the response
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}


