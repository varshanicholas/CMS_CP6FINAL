using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Utility;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public class StaffRepository : IStaffRepository
    {
        // EF - Virtual Database
        private readonly CmsCamp6finalContext _context;

        // DI -- Constructor Injection
        public StaffRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

  
      public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
{
    try
    {
        if (_context != null)
        {
            // Retrieve staff records with their related departments
            var staffWithDepartments = await _context.Staff
                                                     .Include(staff => staff.Department)
                                                     .OrderByDescending(staff => staff.StaffId)
                                                     .ToListAsync();

            // Ensure all staff's departments have their specializations loaded
            foreach (var staff in staffWithDepartments)
            {
                if (staff.Department != null)
                {
                    staff.Department = await _context.Departments
                                                     .Include(d => d.Specialization)
                                                     .FirstOrDefaultAsync(d => d.DepartmentId == staff.DepartmentId);
                }
            }

            return staffWithDepartments;
        }

        // Return an empty List if context is null
        return new List<Staff>();
    }
    catch (Exception ex)
    {
        // Handle the exception (e.g., log the error)
        return new List<Staff>(); // Returning an empty list on error
    }
}


      
       

        public async Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff)
{
    try
    {
        if (staff == null)
        {
            return new ActionResult<Staff>(new Staff { StaffName = "Staff data is null." });
        }

        if (_context == null || !await _context.Database.CanConnectAsync())
        {
            Console.WriteLine("Database connection failed.");
            return new ActionResult<Staff>(new Staff { StaffName = "Database context is not initialized or connection failed." });
        }

        // Validate the department
        var existingDepartment = await _context.Departments
                                              .Include(d => d.Specialization)
                                              .FirstOrDefaultAsync(d => d.DepartmentId == staff.DepartmentId);

        if (existingDepartment == null)
        {
            Console.WriteLine($"No department found with DepartmentId: {staff.DepartmentId}");
            return new ActionResult<Staff>(new Staff { StaffName = "Department not found." });
        }

        // Add the staff record
        await _context.Staff.AddAsync(staff);
        await _context.SaveChangesAsync();

        // Retrieve the added staff record
        var addedStaff = await _context.Staff
                                       .Include(s => s.Department)
                                       .ThenInclude(d => d.Specialization)
                                       .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);

        if (addedStaff == null)
        {
            Console.WriteLine("Staff not found after adding.");
            return new ActionResult<Staff>(new Staff { StaffName = "Staff not found after adding." });
        }

        // Log for debugging
        Console.WriteLine($"Added Staff: {addedStaff.StaffId}, {addedStaff.StaffName}");
        Console.WriteLine($"Department: {addedStaff.Department.DepartmentName}");
        Console.WriteLine($"Specialization: {addedStaff.Department.Specialization.SpecializationName}");

        return addedStaff;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception: " + ex.Message);
        return new ActionResult<Staff>(new Staff { StaffName = "Internal server error: " + ex.Message });
    }
}






    }
}
