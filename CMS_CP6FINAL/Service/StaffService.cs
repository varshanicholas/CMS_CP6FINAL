using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Service
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        // Constructor Injection
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        // Insert
        public async Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff)
        {
            return await _staffRepository.PostStaffReturnRecord(staff);
        }

        // Update
        //public async Task<ActionResult<Staff>> PutStaff(int id, Staff staff)
        //{
        //    return await _staffRepository.PutStaff(id, staff);
        //}

        // Search By StaffId
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            return await _staffRepository.GetStaffById(id);
        }

        // List All Staff
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            return await _staffRepository.GetStaff();
        }

        public async Task<Department> FindDepartmentById(int departmentId) { return await _staffRepository.FindDepartmentById(departmentId); }

        // Delete Staff
        //public JsonResult DeleteStaff(int id)
        //{
        //    return _staffRepository.DeleteStaff(id);
        //}

        //// Get all departments
        //public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        //{
        //    return await _staffRepository.GetDepartments();
        //}

        //// Insert a Staff member using stored procedure - Return Staff Record
        //public async Task<ActionResult<IEnumerable<Staff>>> PostStaffByProcedureReturnRecord(Staff staff)
        //{
        //    return await _staffRepository.PostStaffByProcedureReturnRecord(staff);
        //}

        //// Implementation of new search methods
        //public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        //{
        //    return await _staffRepository.GetStaffByPhoneNumber(phoneNumber);
        //}

        //public async Task<ActionResult<Staff>> GetStaffByStaffId(int staffId)
        //{
        //    return await _staffRepository.GetStaffByStaffId(staffId);
        //}

        // Methods for StaffViewModel
        #region StaffViewModel Methods

        // 1- Get All StaffView - Search All
        public async Task<ActionResult<IEnumerable<StaffViewModel>>> GetStaffView()
        {
            return await _staffRepository.GetStaffView();
        }

        // 3 - Get a StaffView member based on Id
        public async Task<ActionResult<StaffViewModel>> GetStaffViewById(int id)
        {
            return await _staffRepository.GetStaffViewById(id);
        }

        // 4 - Insert a StaffView member - Return StaffView Record
        public async Task<ActionResult<StaffViewModel>> PostStaffViewReturnRecord(Staff staff)
        {
            return await _staffRepository.PostStaffViewReturnRecord(staff);
        }

        // 5 - Insert a StaffView member - Return ID
        public async Task<ActionResult<int>> PostStaffViewReturnId(Staff staff)
        {
            return await _staffRepository.PostStaffViewReturnId(staff);
        }

        // 6 - Update a StaffView member with ID and staffView details
        public async Task<ActionResult<StaffViewModel>> PutStaffView(int id, Staff staff)
        {
            return await _staffRepository.PutStaffView(id, staff);
        }

        // 7 - Delete a StaffView member
        public JsonResult DeleteStaffView(int id)
        {
            return _staffRepository.DeleteStaffView(id);
        }

        // 9 - Additional methods if needed
        public async Task<ActionResult<IEnumerable<StaffViewModel>>> PostStaffViewByProcedureReturnRecord(Staff staff)
        {
            return await _staffRepository.PostStaffViewByProcedureReturnRecord(staff);
        }

        // New methods for search functionality
        public async Task<ActionResult<StaffViewModel>> GetStaffViewByPhoneNumber(string phoneNumber)
        {
            return await _staffRepository.GetStaffViewByPhoneNumber(phoneNumber);
        }

        public async Task<ActionResult<StaffViewModel>> GetStaffViewByStaffId(int staffId)
        {
            return await _staffRepository.GetStaffViewByStaffId(staffId);
        }

        #endregion
    }
}
