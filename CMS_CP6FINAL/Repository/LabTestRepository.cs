
﻿using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public class LabTestRepository : ILabTestRepository
    {
        private readonly CmsCamp6finalContext _context;

        public LabTestRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<LabTest>>> GetAllLabTests()
        {
            return await _context.LabTests.Include(lt => lt.Category).ToListAsync();
        }

        public async Task<ActionResult<LabTest>> GetLabTestByIdAndName(int id, string name)
        {
            return await _context.LabTests.Include(lt => lt.Category).FirstOrDefaultAsync(lt => lt.LabTestId == id && lt.LabTestName == name);
        }

        public async Task<ActionResult<LabTest>> AddLabTest(LabTest labTest)
        {
            if (labTest.IsValid())
            {
                await _context.LabTests.AddAsync(labTest);
                await _context.SaveChangesAsync();
                return labTest;
            }
            return new BadRequestResult();
        }

        public async Task<ActionResult<int>> AddLabTestReturnId(LabTest labTest)
        {
            if (labTest.IsValid())
            {
                await _context.LabTests.AddAsync(labTest);
                await _context.SaveChangesAsync();
                return new OkObjectResult(labTest.LabTestId); // Return OkObjectResult with LabTestId
            }
            return new BadRequestResult();
        }


        public async Task<ActionResult<LabTest>> UpdateLabTestByIdAndName(int id, string name, LabTest labTest)
        {
            var existingLabTest = await _context.LabTests.FindAsync(id);

            if (existingLabTest != null && existingLabTest.LabTestName == name && labTest.IsValid())
            {
                // Add logging to check the Cost value
                Console.WriteLine($"Updating LabTest with Cost: {labTest.Cost}"); // Added logging
                existingLabTest.LabTestName = labTest.LabTestName;
                existingLabTest.Cost = labTest.Cost;
                existingLabTest.ResultType = labTest.ResultType;
                existingLabTest.ReferenceMinRange = labTest.ReferenceMinRange;
                existingLabTest.ReferenceMaxRange = labTest.ReferenceMaxRange;
                existingLabTest.SampleRequired = labTest.SampleRequired;
                existingLabTest.IsActive = labTest.IsActive;
                existingLabTest.CategoryId = labTest.CategoryId;

                await _context.SaveChangesAsync();
                return existingLabTest;
            }
            return new BadRequestResult();
        }

        //isactive deletion

        public JsonResult DeleteLabTest(int id)
{
    var existingLabTest = _context.LabTests.Find(id);

    if (existingLabTest != null)
    {
        existingLabTest.IsActive = false;
        _context.SaveChanges(); // Ensure the change is saved synchronously
        return new JsonResult(new { success = true, message = "LabTest marked as inactive successfully" }) { StatusCode = 200 };
    }

    return new JsonResult(new { success = false, message = "LabTest not found" }) { StatusCode = 404 };
}

       


        //for etier deletion

        //       public JsonResult DeleteLabTest(int id)
        //{
        //    var existingLabTest = _context.LabTests.Find(id);

        //    if (existingLabTest != null)
        //    {
        //        _context.LabTests.Remove(existingLabTest);
        //        _context.SaveChanges(); // Corrected from SaveChangesAsync to SaveChanges
        //        return new JsonResult(new { success = true, message = "LabTest deleted successfully" }) { StatusCode = 200 };
        //    }

        //    return new JsonResult(new { success = false, message = "LabTest not found" }) { StatusCode = 404 };
        //}

    }
}

﻿//using CMS_CP6FINAL.Model;
//using CMS_CP6FINAL.Utility;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CMS_CP6FINAL.Repository
//{
//    public class LabTestRepository : ILabTestRepository
//    {
//        private readonly CmsCamp6finalContext _context;

//        public LabTestRepository(CmsCamp6finalContext context)
//        {
//            _context = context;
//        }

//        public async Task<ActionResult<IEnumerable<LabTest>>> GetAllLabTests()
//        {
//            return await _context.LabTests.Include(lt => lt.Category).ToListAsync();
//        }

//        public async Task<ActionResult<LabTest>> GetLabTestByIdAndName(int id, string name)
//        {
//            return await _context.LabTests.Include(lt => lt.Category).FirstOrDefaultAsync(lt => lt.LabTestId == id && lt.LabTestName == name);
//        }

//        public async Task<ActionResult<LabTest>> AddLabTest(LabTest labTest)
//        {
//            //if (labTest.IsValid())
//            {
//                await _context.LabTests.AddAsync(labTest);
//                await _context.SaveChangesAsync();
//                return labTest;
//            }
//            return new BadRequestResult();
//        }

//        public async Task<ActionResult<int>> AddLabTestReturnId(LabTest labTest)
//        {
//            //if (labTest.IsValid())
//            {
//                await _context.LabTests.AddAsync(labTest);
//                await _context.SaveChangesAsync();
//                return labTest.LabTestId;
//            }
//            return new BadRequestResult();
//        }

//        public async Task<ActionResult<LabTest>> UpdateLabTestByIdAndName(int id, string name, LabTest labTest)
//        {
//            var existingLabTest = await _context.LabTests.FindAsync(id);

//            if (existingLabTest != null && existingLabTest.LabTestName == name && labTest.IsValid())
//            {
//                // Add logging to check the Cost value
//                Console.WriteLine($"Updating LabTest with Cost: {labTest.Cost}"); // Added logging
//                existingLabTest.LabTestName = labTest.LabTestName;
//                existingLabTest.Cost = labTest.Cost;
//                existingLabTest.ResultType = labTest.ResultType;
//                existingLabTest.ReferenceMinRange = labTest.ReferenceMinRange;
//                existingLabTest.ReferenceMaxRange = labTest.ReferenceMaxRange;
//                existingLabTest.SampleRequired = labTest.SampleRequired;
//                existingLabTest.IsActive = labTest.IsActive;
//                existingLabTest.CategoryId = labTest.CategoryId;

//                await _context.SaveChangesAsync();
//                return existingLabTest;
//            }
//            return new BadRequestResult();
//        }




//        public JsonResult DeleteLabTest(int id)
//        {
//            var existingLabTest = _context.LabTests.Find(id);

//            if (existingLabTest != null)
//            {
//                _context.LabTests.Remove(existingLabTest);
//                _context.SaveChangesAsync();
//                return new JsonResult(new { success = true, message = "LabTest deleted successfully" }) { StatusCode = 200 };
//            }

//            return new JsonResult(new { success = false, message = "LabTest not found" }) { StatusCode = 404 };
//        }
//    }
//}

