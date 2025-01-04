using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorLabTestRepository _labTestRepository;

    public DoctorController(IDoctorLabTestRepository labTestRepository)
    {
        _labTestRepository = labTestRepository;
    }

    //[HttpGet("DailyLabTests")]
    //public async Task<IActionResult> GetDailyLabTests([FromQuery] DateTime date)
    //{
    //    if (date == default)
    //    {
    //        date = DateTime.Today;
    //    }

    //    var labTests = await _labTestRepository.GetDailyLabTestsAsync(date);
    //    return Ok(labTests);
    //}
}
