using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS_CP6FINAL.Utility;

namespace CMS_CP6FINAL.Service
{
    public class LabTestService : ILabTestService
    {
        private readonly ILabTestRepository _repository;

        public LabTestService(ILabTestRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<IEnumerable<LabTest>>> GetAllLabTests()
        {
            return await _repository.GetAllLabTests();
        }

        public async Task<ActionResult<LabTest>> GetLabTestByIdAndName(int id, string name)
        {
            return await _repository.GetLabTestByIdAndName(id, name);
        }

        public async Task<ActionResult<LabTest>> AddLabTest(LabTest labTest)
        {
            if (labTest.IsValid())
            {
                return await _repository.AddLabTest(labTest);
            }
            return new BadRequestResult();
        }

        public async Task<ActionResult<int>> AddLabTestReturnId(LabTest labTest)
        {
            if (labTest.IsValid())
            {
                return await _repository.AddLabTestReturnId(labTest);
            }
            return new BadRequestResult();
        }

        public async Task<ActionResult<LabTest>> UpdateLabTestByIdAndName(int id, string name, LabTest labTest)
        {
            if (labTest.IsValid())
            {
                return await _repository.UpdateLabTestByIdAndName(id, name,   labTest);
            }
            return new BadRequestResult();
        }


        public JsonResult DeleteLabTest(int id)
        {
            return _repository.DeleteLabTest(id);
        }
    }
}
