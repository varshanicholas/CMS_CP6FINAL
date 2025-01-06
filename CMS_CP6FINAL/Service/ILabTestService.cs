using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Service
{
    public interface ILabTestService
    {
        Task<ActionResult<IEnumerable<LabTest>>> GetAllLabTests();
        Task<ActionResult<LabTest>> GetLabTestByIdAndName(int id, string name);
        Task<ActionResult<LabTest>> AddLabTest(LabTest labTest);
        Task<ActionResult<int>> AddLabTestReturnId(LabTest labTest);
        Task<ActionResult<LabTest>> UpdateLabTestByIdAndName(int id, string name, LabTest labTest);
        JsonResult DeleteLabTest(int id);
    }
}
