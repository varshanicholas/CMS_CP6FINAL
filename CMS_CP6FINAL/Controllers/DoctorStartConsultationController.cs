using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class DoctorStartConsultationController : ControllerBase
    {
        private readonly IDoctorStartConsultationRepository _repository;

        public DoctorStartConsultationController(IDoctorStartConsultationRepository repository)
        {
            _repository = repository;
        }

        //    [HttpPost]
        //    [Route("StartConsultation")]
        //    public async Task<IActionResult> StartConsultation([FromBody] DoctorStartConsultationViewModel model)
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        try
        //        {
        //            var result = await _repository.StartConsultationAsync(model);
        //            if (result != null)
        //                return Ok(result);

        //            return StatusCode(500, new { Message = "An error occurred while starting the consultation." });
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, new { Message = ex.Message });
        //        }
        //    }
        //}

        [HttpPost]
        [Route("StartConsultation")]
        public async Task<IActionResult> StartConsultation([FromBody] DoctorStartConsultationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _repository.StartConsultationAsync(model);
                return Ok(result); // Return the updated model as a response
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
