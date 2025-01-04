using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public Task<ActionResult<Staff>> GetStaffById(int id);

        // 4 - Insert a Staff member - Return Staff Record
        public Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff);

        // 5 - Insert a Staff member - Return ID
       public Task<ActionResult<int>> PostStaffReturnId(Staff staff);
       

     public Task<Department> FindDepartmentById(int? departmentId);
 
    // Other methods



        // 6 - Update a Staff member with ID and staff details
      //  public Task<ActionResult<Staff>> PutStaff(int id, Staff staff);

        // 7 - Delete a Staff member
       // public JsonResult DeleteStaff(int id);

        // 8 - Get all departments (if applicable)
       // public Task<ActionResult<IEnumerable<Department>>> GetDepartments();

        // 9 - Additional methods if needed
      //  public Task<ActionResult<IEnumerable<Staff>>> PostStaffByProcedureReturnRecord(Staff staff);

        // New methods for search functionality
      //  public Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber);
      //  public Task<ActionResult<Staff>> GetStaffByStaffId(int staffId);

        // New method for getting DoctorViewModel by StaffId
      //  public Task<ActionResult<DoctorViewModel>> GetDoctorViewModelById(int staffId);

        // Methods for StaffViewModel
        #region StaffViewModel Methods

        // 1- Get All StaffView - Search All
        public Task<ActionResult<IEnumerable<StaffViewModel>>> GetStaffView();

        // 3 - Get a StaffView member based on Id
        public Task<ActionResult<StaffViewModel>> GetStaffViewById(int id);

        // 4 - Insert a StaffView member - Return StaffView Record
        public Task<ActionResult<StaffViewModel>> PostStaffViewReturnRecord(Staff staff);

        // 5 - Insert a StaffView member - Return ID
        public Task<ActionResult<int>> PostStaffViewReturnId(Staff staff);

        // 6 - Update a StaffView member with ID and staffView details
        public Task<ActionResult<StaffViewModel>> PutStaffView(int id, Staff staff);

        // 7 - Delete a StaffView member
        public JsonResult DeleteStaffView(int id);

        // 9 - Additional methods if needed
        public Task<ActionResult<IEnumerable<StaffViewModel>>> PostStaffViewByProcedureReturnRecord(Staff staff);

        // New methods for search functionality
        public Task<ActionResult<StaffViewModel>> GetStaffViewByPhoneNumber(string phoneNumber);
        public Task<ActionResult<StaffViewModel>> GetStaffViewByStaffId(int staffId);

        #endregion
    }
}
