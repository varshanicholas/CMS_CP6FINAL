using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepository _repository;

        public MedicineController(IMedicineRepository repository)
        {
            _repository = repository;
        }

        #region Get all medicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetAllMedicines()
        {
            var medicines = await _repository.GetMedicines();
            if (medicines == null || !medicines.Any())
            {
                return NotFound("No medicines found.");
            }
            return Ok(medicines);
        }
        #endregion

        #region Get medicine by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicineById(int id)
        {
            var medicine = await _repository.GetMedicineById(id);
            if (medicine == null)
            {
                return NotFound("Medicine not found.");
            }
            return Ok(medicine);
        }
        #endregion

        #region Search medicine by Name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<Medicine>>> SearchMedicineByName(string name)
        {
            var medicines = await _repository.GetMedicineByName(name);
            if (medicines == null || !medicines.Any())
            {
                return NotFound("No medicines found matching the name.");
            }
            return Ok(medicines);
        }
        #endregion

        #region Add a new medicine
        [HttpPost]
        public async Task<IActionResult> AddMedicine([FromBody] Medicine medicine)
        {
            if (medicine == null || string.IsNullOrEmpty(medicine.MedicineName) || medicine.MedicineCategoryId <= 0)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _repository.AddMedicine(medicine);
            if (!result)
            {
                return StatusCode(500, "Internal server error while adding medicine.");
            }

            return Ok(medicine);
        }
        #endregion

        #region Update an existing medicine
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, [FromBody] Medicine medicine)
        {
            var updatedMedicine = await _repository.UpdateMedicine(id, medicine);
            if (updatedMedicine == null)
            {
                return NotFound("Medicine not found.");
            }
            return Ok(updatedMedicine);
        }
        #endregion

        [HttpDelete("{id}")]
        public IActionResult DeleteMedicine(int id)
        {
            var result = _repository.DeleteMedicine(id);
            return result;
        }

    }
}
