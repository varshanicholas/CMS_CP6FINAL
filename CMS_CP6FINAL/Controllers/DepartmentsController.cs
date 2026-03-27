using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService service)
        {
            _departmentService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetDepartments();
            if (departments == null)
            {
                return NotFound("No Departments found");
            }

            return Ok(departments);
        }
    }
}
