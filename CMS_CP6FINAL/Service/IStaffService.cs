using System.Collections.Generic;
using System.Threading.Tasks;
<<<<<<< HEAD
using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
=======
using CMS_CP6FINAL.Utility;
using Microsoft.EntityFrameworkCore;
>>>>>>> ca42b0a2b5ce1c9947bd61b01c79ff8d5431b3c3

namespace CMS_CP6FINAL.Service
{
    public interface IStaffService
    {

        Task<ActionResult<IEnumerable<Staff>>> GetStaffs();
        Task<ActionResult<Staff>> GetStaffById(int id);
        Task<ActionResult<Staff>> PostStaff(Staff staff);
        Task<ActionResult<int>> PostStaffReturnId(Staff staff);
        Task<ActionResult<Staff>> PutStaff(int id, Staff staff);
        Task<JsonResult> DeleteStaff(int id);
        Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber);
        Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId);

<<<<<<< HEAD
          Task<ActionResult<IEnumerable<StaffDeptViewModel>>> GetAllStaffsByViewModel();
        
        }
=======
        //Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId);

        // Insert
        Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff);

        // Update
       // Task<ActionResult<Staff>> PutStaff(int id, Staff staff);

        // Search By StaffId
   

        // List All Staff
        Task<ActionResult<IEnumerable<Staff>>> GetStaff();

        // Delete Staff
      //  JsonResult DeleteStaff(int id);

        //Task<Department> FindDepartmentById(int departmentId);

        // Get all departments (if applicable)
       // Task<ActionResult<IEnumerable<Department>>> GetDepartments();

        // Insert a Staff member using stored procedure - Return Staff Record
       // Task<ActionResult<IEnumerable<Staff>>> PostStaffByProcedureReturnRecord(Staff staff);

        // New methods for search functionality
      //  Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber);
      //  Task<ActionResult<Staff>> GetStaffByStaffId(int staffId);

        // Methods for StaffViewModel
        #region StaffViewModel Methods

        // 1- Get All StaffView - Search All
        //Task<ActionResult<IEnumerable<StaffViewModel>>> GetStaffView();

        //// 3 - Get a StaffView member based on Id
        //Task<ActionResult<StaffViewModel>> GetStaffViewById(int id);

        //// 4 - Insert a StaffView member - Return StaffView Record
        //Task<ActionResult<StaffViewModel>> PostStaffViewReturnRecord(Staff staff);

        //// 5 - Insert a StaffView member - Return ID
        //Task<ActionResult<int>> PostStaffViewReturnId(Staff staff);

        //// 6 - Update a StaffView member with ID and staffView details
        //Task<ActionResult<StaffViewModel>> PutStaffView(int id, Staff staff);

        //// 7 - Delete a StaffView member
        //JsonResult DeleteStaffView(int id);

        //// 9 - Additional methods if needed
        //Task<ActionResult<IEnumerable<StaffViewModel>>> PostStaffViewByProcedureReturnRecord(Staff staff);

        //// New methods for search functionality
        //Task<ActionResult<StaffViewModel>> GetStaffViewByPhoneNumber(string phoneNumber);
        //Task<ActionResult<StaffViewModel>> GetStaffViewByStaffId(int staffId);

        #endregion

    }
>>>>>>> ca42b0a2b5ce1c9947bd61b01c79ff8d5431b3c3
}



