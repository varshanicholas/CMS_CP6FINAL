using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS_CP6FINAL.Utility;

namespace CMS_CP6FINAL.Repository
{
    public interface IStaffRepository
    {
        #region 1- Get All Staff - Search All
        // Get all staff from DB
        // Search All

        // Task for async method--await will be there in function
        // ActionResult-return type.returns to client.-for returning data and status code. Status codes are 200 ok..etc
        public Task<ActionResult<IEnumerable<Staff>>> GetStaff();

        #endregion

        // 3 - Get a Staff member based on Id
       
        // 4 - Insert a Staff member - Return Staff Record
        public Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff);

     
    }
}
