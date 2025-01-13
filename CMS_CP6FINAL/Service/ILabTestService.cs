using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Service
{
    public interface ILabTestService
    {
        Task<ActionResult<IEnumerable<LabTest>>> GetAllLabTests();
        Task<ActionResult<LabTest>> GetLabTestById(int id);
        Task<ActionResult<LabTest>> AddLabTest(LabTest labTest);
        Task<ActionResult<int>> AddLabTestReturnId(LabTest labTest);
        Task<ActionResult<IEnumerable<LabTestCategory>>> GetAllCategories();
        Task<ActionResult<LabTest>> UpdateLabTestById(int id, LabTest labTest);
        JsonResult DeleteLabTest(int id);
        Task<ActionResult<LabTest>> GetLabTestByName(string name); // New method
    }
}
