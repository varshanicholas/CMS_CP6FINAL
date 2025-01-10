using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorLabTestRepository _doctorLabTestRepository;

    public DoctorController(IDoctorLabTestRepository doctorLabTestRepository)
    {
        _doctorLabTestRepository = doctorLabTestRepository;
    }

    [HttpGet("GetLabTestReports/{appointmentId}")]
    public async Task<IActionResult> GetLabTestReports(int appointmentId)
    {
        var reports = await _doctorLabTestRepository.GetLabTestReportsByAppointmentIdAsync(appointmentId);

        if (reports == null || !reports.Any())
        {
            return NotFound("No lab test reports found for the given appointment.");
        }

        return Ok(reports);
    }
}
