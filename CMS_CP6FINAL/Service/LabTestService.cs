using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<ActionResult<LabTest>> GetLabTestById(int id)
        {
            return await _repository.GetLabTestById(id);
        }

        public async Task<ActionResult<LabTest>> AddLabTest(LabTest labTest)
        {
            return await _repository.AddLabTest(labTest);
        }

        public async Task<ActionResult<int>> AddLabTestReturnId(LabTest labTest)
        {
            return await _repository.AddLabTestReturnId(labTest);
        }

        public async Task<ActionResult<IEnumerable<LabTestCategory>>> GetAllCategories()
        {
            return await _repository.GetAllCategories();
        }

        public async Task<ActionResult<LabTest>> UpdateLabTestById(int id, LabTest labTest)
        {
            return await _repository.UpdateLabTestById(id, labTest);
        }

        public JsonResult DeleteLabTest(int id)
        {
            return _repository.DeleteLabTest(id);
        }

        public async Task<ActionResult<LabTest>> GetLabTestByName(string name)
        {
            return await _repository.GetLabTestByName(name);
        }
    }
}
