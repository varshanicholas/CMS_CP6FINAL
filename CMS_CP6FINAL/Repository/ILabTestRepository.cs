using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public interface ILabTestRepository
    {
        Task<ActionResult<IEnumerable<LabTest>>> GetAllLabTests();
        Task<ActionResult<LabTest>> GetLabTestById(int id);
        Task<ActionResult<LabTest>> AddLabTest(LabTest labTest);
        Task<ActionResult<int>> AddLabTestReturnId(LabTest labTest);
        Task<ActionResult<IEnumerable<LabTestCategory>>> GetAllCategories();
        Task<ActionResult<LabTest>> UpdateLabTestById(int id, LabTest labTest);
        Task<ActionResult<LabTest>> GetLabTestByName(string name); // New method
        JsonResult DeleteLabTest(int id);

    }
}
