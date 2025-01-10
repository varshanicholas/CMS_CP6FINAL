using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : ControllerBase
    {
        private readonly ILabTestService _service;

        public LabTestController(ILabTestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTest>>> GetAllLabTests()
        {
            var labTests = await _service.GetAllLabTests();
            if (labTests == null)
            {
                return NotFound("No lab tests found");
            }
            return Ok(labTests);
        }

        [HttpGet("{id}/{name}")]
        public async Task<ActionResult<LabTest>> GetLabTestByIdAndName(int id, string name)
        {
            var labTest = await _service.GetLabTestByIdAndName(id, name);
            if (labTest == null)
            {
                return NotFound("Lab test not found");
            }
            return Ok(labTest);
        }

        [HttpPost]
        public async Task<ActionResult<LabTest>> AddLabTest(LabTest labTest)
        {
            if (ModelState.IsValid)
            {
                var newLabTest = await _service.AddLabTest(labTest);
                if (newLabTest != null)
                {
                    return Ok(newLabTest);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPost("v1")]
        public async Task<ActionResult<int>> AddLabTestReturnId(LabTest labTest)
        {
            if (ModelState.IsValid)
            {
                var labTestIdResult = await _service.AddLabTestReturnId(labTest);
                if (labTestIdResult.Result is OkObjectResult okResult && (int)okResult.Value > 0)
                {
                    return Ok((int)okResult.Value);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPut("{id}/{name}")]
        public async Task<ActionResult<LabTest>> UpdateLabTestByIdAndName(int id, string name, LabTest labTest)
        {
            if (ModelState.IsValid)
            {

                var updatedLabTest = await _service.UpdateLabTestByIdAndName(id, name, labTest);
                if (updatedLabTest != null)
                {
                    return Ok(updatedLabTest);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLabTest(int id)
        {
            var result = _service.DeleteLabTest(id);
            if (result != null)
            {
                if (result.StatusCode == 200)
                {
                    return Ok(result.Value);
                }
                return NotFound(result.Value);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { success = false, message = "An unexpected error occurred" });
        }
    }
}
