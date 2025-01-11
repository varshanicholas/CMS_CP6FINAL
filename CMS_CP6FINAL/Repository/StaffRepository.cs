using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Utility;
using CMS_CP6FINAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

﻿using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly CmsCamp6finalContext _context; // Ensure the correct context

        public StaffRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
        {
            return await _context.Staff
                                 .Include(staff => staff.Department) // Include Department
                                 .ToListAsync();
        }

        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            return await _context.Staff
                                 .Include(staff => staff.Department) // Include Department
                                 .FirstOrDefaultAsync(staff => staff.StaffId == id);
        }

        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentNullException(nameof(staff), "Staff data is null");
            }
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            // Include Department in the returned data
            return await _context.Staff
                                 .Include(s => s.Department)
                                 .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
        }

        public async Task<ActionResult<int>> PostStaffReturnId(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentNullException(nameof(staff), "Staff data is null");
            }
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return staff.StaffId;
        }

        public async Task<ActionResult<Staff>> PutStaff(int id, Staff staff)
        {
            var existingStaff = await _context.Staff.FindAsync(id);
            if (existingStaff == null)
            {
                return null;
            }

            existingStaff.StaffName = staff.StaffName;
            existingStaff.Gender = staff.Gender;
            existingStaff.PhoneNumber = staff.PhoneNumber;
            existingStaff.Email = staff.Email;
            existingStaff.Dob = staff.Dob;
            existingStaff.Address = staff.Address;
            existingStaff.Qualification = staff.Qualification;
            existingStaff.DepartmentId = staff.DepartmentId;
            existingStaff.CreatedDate = staff.CreatedDate;
            existingStaff.IsActive = staff.IsActive;

            await _context.SaveChangesAsync();

            // Include Department in the returned data
            return await _context.Staff
                                 .Include(s => s.Department)
                                 .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
        }

        public JsonResult DeleteStaff(int id)
        {
            var existingStaff = _context.Staff.Find(id);
            if (existingStaff == null)
            {
                return new JsonResult(new { success = false, message = "Staff not found" }) { StatusCode = 404 };
            }

            existingStaff.IsActive = false;
            _context.SaveChanges(); // Ensure the change is saved synchronously
            return new JsonResult(new { success = true, message = "Staff marked as inactive" }) { StatusCode = 200 };
        }


        public async Task<ActionResult<Staff>> GetStaffByPhoneNumberOrStaffId(string phoneNumber, int staffId)
{
    var staff = await _context.Staff
                         .Include(staff => staff.Department)
                         .FirstOrDefaultAsync(staff => staff.PhoneNumber == phoneNumber || staff.StaffId == staffId);
    if (staff == null)
    {
        Console.WriteLine("No matching staff found in repository.");
    }
    else
    {
        Console.WriteLine($"Found staff in repository: {staff.StaffName}");
    }
    return staff;
}









        public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        {
            return await _context.Staff
                                 .Include(staff => staff.Department) // Include Department
                                 .FirstOrDefaultAsync(staff => staff.PhoneNumber == phoneNumber);
        }

       
    }
}

﻿//using CMS_CP6FINAL.Model;
//using CMS_CP6FINAL.Utility;
//using CMS_CP6FINAL.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

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



        #region 3 - Search By Id
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var staff = await _context.Staff
                                              .Include(staff => staff.Department)
                                              .FirstOrDefaultAsync(staff => staff.StaffId == id);
                    return staff;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        public async Task<Department> FindDepartmentById(int? departmentId)
        {
            return await _context.Departments.FindAsync(departmentId);
        }
//        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
//{
//    try
//    {
//        if (_context != null)
//        {
//            // Retrieve staff records with their related departments
//            var staffWithDepartments = await _context.Staff
//                                                     .Include(staff => staff.Department)
//                                                     .OrderByDescending(staff => staff.StaffId)
//                                                     .ToListAsync();

//            // Ensure all staff's departments have their specializations loaded
//            foreach (var staff in staffWithDepartments)
//            {
//                if (staff.Department != null)
//                {
//                    staff.Department = await _context.Departments
//                                                     .Include(d => d.Specialization)
//                                                     .FirstOrDefaultAsync(d => d.DepartmentId == staff.DepartmentId);
//                }
//            }

//            return staffWithDepartments;
//        }

//        // Return an empty List if context is null
//        return new List<Staff>();
//    }
//    catch (Exception ex)
//    {
//        // Handle the exception (e.g., log the error)
//        return new List<Staff>(); // Returning an empty list on error
//    }
//}

        public async Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                await _context.Staff.AddAsync(staff);
                await _context.SaveChangesAsync();

                var staffWithDepartment = await _context.Staff
                                                        .Include(s => s.Department)
                                                        .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);

                return staffWithDepartment;
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }



        #region 4 - Insert a Staff member - Return Staff Record
        //public async Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff)
        //{
        //    try
        //    {
        //        if (staff == null)
        //        {
        //            throw new ArgumentNullException(nameof(staff), "Staff data is null");
        //        }

      
       

//        public async Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff)
//{
//    try
//    {
//        if (staff == null)
//        {
//            return new ActionResult<Staff>(new Staff { StaffName = "Staff data is null." });
//        }

        //        if (_context == null)
        //        {
        //            throw new InvalidOperationException("Database context is not initialized");
        //        }
//        if (_context == null || !await _context.Database.CanConnectAsync())
//        {
//            Console.WriteLine("Database connection failed.");
//            return new ActionResult<Staff>(new Staff { StaffName = "Database context is not initialized or connection failed." });
//        }

        //        await _context.Staff.AddAsync(staff);
        //        await _context.SaveChangesAsync();
//        // Validate the department
//        var existingDepartment = await _context.Departments
//                                              .Include(d => d.Specialization)
//                                              .FirstOrDefaultAsync(d => d.DepartmentId == staff.DepartmentId);

//        if (existingDepartment == null)
//        {
//            Console.WriteLine($"No department found with DepartmentId: {staff.DepartmentId}");
//            return new ActionResult<Staff>(new Staff { StaffName = "Department not found." });
//        }

//        // Add the staff record
//        await _context.Staff.AddAsync(staff);
//        await _context.SaveChangesAsync();

        //        var staffWithDepartment = await _context.Staff
        //                                                .Include(s => s.Department)
        //                                                .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
//        // Retrieve the added staff record
//        var addedStaff = await _context.Staff
//                                       .Include(s => s.Department)
//                                       .ThenInclude(d => d.Specialization)
//                                       .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);

        //        return staffWithDepartment;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception here if needed
        //        return null;
        //    }
        //}
        #endregion
//        if (addedStaff == null)
//        {
//            Console.WriteLine("Staff not found after adding.");
//            return new ActionResult<Staff>(new Staff { StaffName = "Staff not found after adding." });
//        }

//        // Log for debugging
//        Console.WriteLine($"Added Staff: {addedStaff.StaffId}, {addedStaff.StaffName}");
//        Console.WriteLine($"Department: {addedStaff.Department.DepartmentName}");
//        Console.WriteLine($"Specialization: {addedStaff.Department.Specialization.SpecializationName}");

//        return addedStaff;
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("Exception: " + ex.Message);
//        return new ActionResult<Staff>(new Staff { StaffName = "Internal server error: " + ex.Message });
//    }
//}

        #region 5 - Insert a Staff member - Return ID
        public async Task<ActionResult<int>> PostStaffReturnId(Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }




        //        if (_context == null)
        //        {
        //            throw new InvalidOperationException("Database context is not initialized");
        //        }
//    }
//}
//=======
////        #region 1 - Get All Staff - Search All
////        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
////        {
////            try
////            {
////                if (_context != null)
////                {
////                    return await _context.Staff
////                                         .Include(staff => staff.Department)
////                                         .OrderByDescending(staff => staff.StaffId)
////                                         .ToListAsync();
////                }
////                // Return an empty List if context is null
////                return new List<Staff>();
////            }
////            catch (Exception ex)
////            {
////                // Handle the exception (e.g., log the error)
////                return new List<Staff>(); // Returning an empty list on error
////            }
////        }
////        #endregion


////        #region 3 - Search By Id
////        public async Task<ActionResult<Staff>> GetStaffById(int id)
////        {
////            try
////            {
////                if (_context != null)
////                {
////                    var staff = await _context.Staff
////                                              .Include(staff => staff.Department)
////                                              .FirstOrDefaultAsync(staff => staff.StaffId == id);
////                    return staff;
////                }
////                return null;
////            }
////            catch (Exception ex)
////            {
////                return null;
////            }
////        }
////        #endregion
////        public async Task<Department> FindDepartmentById(int? departmentId)
////{
////    return await _context.Departments.FindAsync(departmentId);
////}

        //        var existingStaff = await _context.Staff.FindAsync(id);
////public async Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff)
////{
////    try
////    {
////        if (staff == null)
////        {
////            throw new ArgumentNullException(nameof(staff), "Staff data is null");
////        }

        //        if (existingStaff == null)
        //        {
        //            return null;
        //        }
////        if (_context == null)
////        {
////            throw new InvalidOperationException("Database context is not initialized");
////        }

////        await _context.Staff.AddAsync(staff);
////        await _context.SaveChangesAsync();

        //        existingStaff.StaffName = staff.StaffName;
        //        existingStaff.Gender = staff.Gender;
        //        existingStaff.PhoneNumber = staff.PhoneNumber;
        //        existingStaff.Email = staff.Email;
        //        existingStaff.Dob = staff.Dob;
        //        existingStaff.Address = staff.Address;
        //        existingStaff.Qualification = staff.Qualification;
        //        existingStaff.DepartmentId = staff.DepartmentId;
        //        existingStaff.CreatedDate = staff.CreatedDate;
////        var staffWithDepartment = await _context.Staff
////                                                .Include(s => s.Department)
////                                                .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);

        //        await _context.SaveChangesAsync();
////        return staffWithDepartment;
////    }
////    catch (Exception ex)
////    {
////        // Log exception here if needed
////        return null;
////    }
////}



        //#region 7 - Delete a Staff member
        //public JsonResult DeleteStaff(int id)
        //{
        //    try
        //    {
        //        if (_context == null)
        //        {
        //            return new JsonResult(new
        //            {
        //                success = false,
        //                message = "Database context is not initialized."
        //            })
        //            {
        //                StatusCode = StatusCodes.Status500InternalServerError
        //            };
        //        }
////        #region 4 - Insert a Staff member - Return Staff Record
////        //public async Task<ActionResult<Staff>> PostStaffReturnRecord(Staff staff)
////        //{
////        //    try
////        //    {
////        //        if (staff == null)
////        //        {
////        //            throw new ArgumentNullException(nameof(staff), "Staff data is null");
////        //        }

        //        var existingStaff = _context.Staff.Find(id);
////        //        if (_context == null)
////        //        {
////        //            throw new InvalidOperationException("Database context is not initialized");
////        //        }

        //        if (existingStaff == null)
        //        {
        //            return new JsonResult(new
        //            {
        //                success = false,
        //                message = "Staff not found."
        //            })
        //            {
        //                StatusCode = StatusCodes.Status400BadRequest
        //            };
        //        }
////        //        await _context.Staff.AddAsync(staff);
////        //        await _context.SaveChangesAsync();

        //        _context.Staff.Remove(existingStaff);
        //        _context.SaveChangesAsync();
////        //        var staffWithDepartment = await _context.Staff
////        //                                                .Include(s => s.Department)
////        //                                                .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);

        //        return new JsonResult(new
        //        {
        //            success = true,
        //            message = "Staff deleted successfully."
        //        })
        //        {
        //            StatusCode = StatusCodes.Status200OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new
        //        {
        //            success = false,
        //            message = "An error occurred."
        //        })
        //        {
        //            StatusCode = StatusCodes.Status500InternalServerError
        //        };
        //    }
        //}
        //#endregion
////        //        return staffWithDepartment;
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        // Log exception here if needed
////        //        return null;
////        //    }
////        //}
////        #endregion

        //#region 8 - Get all departments
        //public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        //{
        //    try
        //    {
        //        if (_context != null)
        //        {
        //            return await _context.Departments.ToListAsync();
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion
////        #region 5 - Insert a Staff member - Return ID
////        public async Task<ActionResult<int>> PostStaffReturnId(Staff staff)
////        {
////            try
////            {
////                if (staff == null)
////                {
////                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
////                }

        //#region 9 - Insert a Staff member using stored procedure - Return Staff Record
        //public async Task<ActionResult<IEnumerable<Staff>>> PostStaffByProcedureReturnRecord(Staff staff)
        //{
        //    try
        //    {
        //        if (staff == null)
        //        {
        //            throw new ArgumentNullException(nameof(staff), "Staff data is null");
        //        }
////                if (_context == null)
////                {
////                    throw new InvalidOperationException("Database context is not initialized");
////                }

        //        if (_context == null)
        //        {
        //            throw new InvalidOperationException("Database context is not initialized");
        //        }
////                await _context.Staff.AddAsync(staff);
////                var changesRecord = await _context.SaveChangesAsync();

        //        var result = await _context.Staff.FromSqlRaw(
        //            "EXEC InsertStaff @StaffName, @Gender, @PhoneNumber, @Email, @Dob, @Address, @Qualification, @DepartmentId, @CreatedDate",
        //            new SqlParameter("@StaffName", staff.StaffName),
        //            new SqlParameter("@Gender", staff.Gender),
        //            new SqlParameter("@PhoneNumber", staff.PhoneNumber),
        //            new SqlParameter("@Email", staff.Email),
        //            new SqlParameter("@Dob", staff.Dob),
        //            new SqlParameter("@Address", staff.Address),
        //            new SqlParameter("@Qualification", staff.Qualification),
        //            new SqlParameter("@DepartmentId", staff.DepartmentId),
        //            new SqlParameter("@CreatedDate", staff.CreatedDate)
        //        ).ToListAsync();
////                if (changesRecord > 0)
////                {
////                    return staff.StaffId;
////                }
////                else
////                {
////                    throw new Exception("Failed to save staff record to the database");
////                }
////            }
////            catch (Exception ex)
////            {
////                // Log exception here if needed
////                return null;
////            }
////        }
////        #endregion

        //        if (result != null && result.Count > 0)
        //        {
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception here if needed
        //        return null;
        //    }
        //}

        //#endregion

        public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.PhoneNumber == phoneNumber);
            if (staff == null)
            {
                return new NotFoundResult();
            }
            return new ActionResult<Staff>(staff);
        }

        public async Task<ActionResult<Staff>> GetStaffByStaffId(int staffId)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null)
            {
                return new NotFoundResult();
            }
            return new ActionResult<Staff>(staff);
        }
        public async Task<ActionResult<DoctorViewModel>> GetDoctorViewModelById(int staffId)
        {
            // Fetch the staff with department and specialization
            var staff = await _context.Staff
                                      .Include(s => s.Department)
                                      .ThenInclude(d => d.Specialization)
                                      .FirstOrDefaultAsync(s => s.StaffId == staffId
                                                            && (s.Department.DepartmentName == "Internal Medicine - Cardiology"
                                                                || s.Department.DepartmentName == "Internal Medicine - Endocrinology"
                                                                || s.Department.DepartmentName == "Internal Medicine - Gastroenterology"
                                                                || s.Department.DepartmentName == "Dermatology"
                                                                || s.Department.DepartmentName == "Oncology"));
            if (staff == null)
            {
                return new NotFoundResult();
            }
////        //#region 6 - Update a Staff member with ID and staff details
////        //public async Task<ActionResult<Staff>> PutStaff(int id, Staff staff)
////        //{
////        //    try
////        //    {
////        //        if (staff == null)
////        //        {
////        //            throw new ArgumentNullException(nameof(staff), "Staff data is null");
////        //        }

////        //        if (_context == null)
////        //        {
////        //            throw new InvalidOperationException("Database context is not initialized");
////        //        }

////        //        var existingStaff = await _context.Staff.FindAsync(id);

////        //        if (existingStaff == null)
////        //        {
////        //            return null;
////        //        }

////        //        existingStaff.StaffName = staff.StaffName;
////        //        existingStaff.Gender = staff.Gender;
////        //        existingStaff.PhoneNumber = staff.PhoneNumber;
////        //        existingStaff.Email = staff.Email;
////        //        existingStaff.Dob = staff.Dob;
////        //        existingStaff.Address = staff.Address;
////        //        existingStaff.Qualification = staff.Qualification;
////        //        existingStaff.DepartmentId = staff.DepartmentId;
////        //        existingStaff.CreatedDate = staff.CreatedDate;

////        //        await _context.SaveChangesAsync();

////        //        var staffWithDepartment = await _context.Staff
////        //                                                .Include(s => s.Department)
////        //                                                .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);

////        //        return staffWithDepartment;
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        // Log exception here if needed
////        //        return null;
////        //    }
////        //}
////        //#endregion

////        //#region 7 - Delete a Staff member
////        //public JsonResult DeleteStaff(int id)
////        //{
////        //    try
////        //    {
////        //        if (_context == null)
////        //        {
////        //            return new JsonResult(new
////        //            {
////        //                success = false,
////        //                message = "Database context is not initialized."
////        //            })
////        //            {
////        //                StatusCode = StatusCodes.Status500InternalServerError
////        //            };
////        //        }

////        //        var existingStaff = _context.Staff.Find(id);

////        //        if (existingStaff == null)
////        //        {
////        //            return new JsonResult(new
////        //            {
////        //                success = false,
////        //                message = "Staff not found."
////        //            })
////        //            {
////        //                StatusCode = StatusCodes.Status400BadRequest
////        //            };
////        //        }

////        //        _context.Staff.Remove(existingStaff);
////        //        _context.SaveChangesAsync();

////        //        return new JsonResult(new
////        //        {
////        //            success = true,
////        //            message = "Staff deleted successfully."
////        //        })
////        //        {
////        //            StatusCode = StatusCodes.Status200OK
////        //        };
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        return new JsonResult(new
////        //        {
////        //            success = false,
////        //            message = "An error occurred."
////        //        })
////        //        {
////        //            StatusCode = StatusCodes.Status500InternalServerError
////        //        };
////        //    }
////        //}
////        //#endregion

////        //#region 8 - Get all departments
////        //public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
////        //{
////        //    try
////        //    {
////        //        if (_context != null)
////        //        {
////        //            return await _context.Departments.ToListAsync();
////        //        }
////        //        return null;
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        return null;
////        //    }
////        //}
////        //#endregion

////        //#region 9 - Insert a Staff member using stored procedure - Return Staff Record
////        //public async Task<ActionResult<IEnumerable<Staff>>> PostStaffByProcedureReturnRecord(Staff staff)
////        //{
////        //    try
////        //    {
////        //        if (staff == null)
////        //        {
////        //            throw new ArgumentNullException(nameof(staff), "Staff data is null");
////        //        }

////        //        if (_context == null)
////        //        {
////        //            throw new InvalidOperationException("Database context is not initialized");
////        //        }

////        //        var result = await _context.Staff.FromSqlRaw(
////        //            "EXEC InsertStaff @StaffName, @Gender, @PhoneNumber, @Email, @Dob, @Address, @Qualification, @DepartmentId, @CreatedDate",
////        //            new SqlParameter("@StaffName", staff.StaffName),
////        //            new SqlParameter("@Gender", staff.Gender),
////        //            new SqlParameter("@PhoneNumber", staff.PhoneNumber),
////        //            new SqlParameter("@Email", staff.Email),
////        //            new SqlParameter("@Dob", staff.Dob),
////        //            new SqlParameter("@Address", staff.Address),
////        //            new SqlParameter("@Qualification", staff.Qualification),
////        //            new SqlParameter("@DepartmentId", staff.DepartmentId),
////        //            new SqlParameter("@CreatedDate", staff.CreatedDate)
////        //        ).ToListAsync();

////        //        if (result != null && result.Count > 0)
////        //        {
////        //            return result;
////        //        }
////        //        else
////        //        {
////        //            return null;
////        //        }
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        // Log exception here if needed
////        //        return null;
////        //    }
////        //}

////        //#endregion

////        public async Task<ActionResult<Staff>> GetStaffByPhoneNumber(string phoneNumber)
////        {
////            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.PhoneNumber == phoneNumber);
////            if (staff == null)
////            {
////                return new NotFoundResult();
////            }
////            return new ActionResult<Staff>(staff);
////        }

////        public async Task<ActionResult<Staff>> GetStaffByStaffId(int staffId)
////        {
////            var staff = await _context.Staff.FindAsync(staffId);
////            if (staff == null)
////            {
////                return new NotFoundResult();
////            }
////            return new ActionResult<Staff>(staff);
////        }
////       public async Task<ActionResult<DoctorViewModel>> GetDoctorViewModelById(int staffId)
////{
////    // Fetch the staff with department and specialization
////    var staff = await _context.Staff
////                              .Include(s => s.Department)
////                              .ThenInclude(d => d.Specialization)
////                              .FirstOrDefaultAsync(s => s.StaffId == staffId 
////                                                    && (s.Department.DepartmentName == "Internal Medicine - Cardiology" 
////                                                        || s.Department.DepartmentName == "Internal Medicine - Endocrinology" 
////                                                        || s.Department.DepartmentName == "Internal Medicine - Gastroenterology" 
////                                                        || s.Department.DepartmentName == "Dermatology"
////                                                        || s.Department.DepartmentName == "Oncology"));
////    if (staff == null)
////    {
////        return new NotFoundResult();
////    }

////    // Fetch the doctor
////    var doctor = await _context.Doctors
////                               .Include(d => d.Staff)
////                               .ThenInclude(s => s.Department)
////                               .ThenInclude(d => d.Specialization)
////                               .FirstOrDefaultAsync(d => d.StaffId == staffId);
////    if (doctor == null)
////    {
////        return new NotFoundResult();
////    }

            // Fetch the doctor
            var doctor = await _context.Doctors
                                       .Include(d => d.Staff)
                                       .ThenInclude(s => s.Department)
                                       .ThenInclude(d => d.Specialization)
                                       .FirstOrDefaultAsync(d => d.StaffId == staffId);
            if (doctor == null)
            {
                return new NotFoundResult();
            }
////    // Construct the DoctorViewModel
////    var doctorViewModel = new DoctorViewModel
////    {
////        // Staff properties
////        StaffId = staff.StaffId,
////        StaffName = staff.StaffName,
////        Gender = staff.Gender,
////        PhoneNumber = staff.PhoneNumber,
////        Email = staff.Email,
////        Dob = staff.Dob,
////        Address = staff.Address,
////        Qualification = staff.Qualification,
////        DepartmentId = staff.DepartmentId,
////        CreatedDate = staff.CreatedDate,
////        DepartmentName = staff.Department?.DepartmentName,

            // Construct the DoctorViewModel
            var doctorViewModel = new DoctorViewModel
            {
                // Staff properties
                StaffId = staff.StaffId,
                StaffName = staff.StaffName,
                Gender = staff.Gender,
                PhoneNumber = staff.PhoneNumber,
                Email = staff.Email,
                Dob = staff.Dob,
                Address = staff.Address,
                Qualification = staff.Qualification,
                DepartmentId = staff.DepartmentId,
                CreatedDate = staff.CreatedDate,
                DepartmentName = staff.Department?.DepartmentName,
////        // Doctor properties
////        DoctorId = doctor.DoctorId,
////        ConsultationFee = doctor.ConsultationFee,

                // Doctor properties
                DoctorId = doctor.DoctorId,
                ConsultationFee = doctor.ConsultationFee,
////        // Specialization properties
////        SpecializationId = staff.Department?.Specialization?.SpecializationId ?? 0,
////        SpecializationName = staff.Department?.Specialization?.SpecializationName,

                // Specialization properties
                SpecializationId = staff.Department?.Specialization?.SpecializationId ?? 0,
                SpecializationName = staff.Department?.Specialization?.SpecializationName,
////        // Navigation properties
////        Department = staff.Department,
////        Specialization = staff.Department?.Specialization
////    };

                // Navigation properties
                Department = staff.Department,
                Specialization = staff.Department?.Specialization
            };
////    return new ActionResult<DoctorViewModel>(doctorViewModel);
////}





    
      

////        public async Task<ActionResult<IEnumerable<StaffViewModel>>> GetStaffView()
////        {
////            try
////            {
////                if (_context != null)
////                {
////                    var staffList = await _context.Staff
////                                                  .Include(staff => staff.Department)
////                                                  .Where(staff => staff.Department.SpecializationId == 6)
////                                                  .OrderByDescending(staff => staff.StaffId)
////                                                  .ToListAsync();

        public async Task<ActionResult<IEnumerable<StaffViewModel>>> GetStaffView()
        {
            try
            {
                if (_context != null)
                {
                    var staffList = await _context.Staff
                                                  .Include(staff => staff.Department)
                                                  .Where(staff => staff.Department.SpecializationId == 6)
                                                  .OrderByDescending(staff => staff.StaffId)
                                                  .ToListAsync();
////                    var staffViewModels = staffList.Select(staff => MappingHelpers.MapToViewModel(staff)).ToList();
////                    return new ActionResult<IEnumerable<StaffViewModel>>(staffViewModels);
////                }
////                return new List<StaffViewModel>();
////            }
////            catch (Exception ex)
////            {
////                return new List<StaffViewModel>();
////            }
////        }

                    var staffViewModels = staffList.Select(staff => MappingHelpers.MapToViewModel(staff)).ToList();
                    return new ActionResult<IEnumerable<StaffViewModel>>(staffViewModels);
                }
                return new List<StaffViewModel>();
            }
            catch (Exception ex)
            {
                return new List<StaffViewModel>();
            }
        }
////        public async Task<ActionResult<StaffViewModel>> GetStaffViewById(int id)
////        {
////            try
////            {
////                if (_context != null)
////                {
////                    var staff = await _context.Staff
////                                              .Include(staff => staff.Department)
////                                              .FirstOrDefaultAsync(staff => staff.StaffId == id
////                                                                         && staff.Department.SpecializationId == 6);
////                    if (staff == null)
////                    {
////                        return new NotFoundResult();
////                    }

        public async Task<ActionResult<StaffViewModel>> GetStaffViewById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var staff = await _context.Staff
                                              .Include(staff => staff.Department)
                                              .FirstOrDefaultAsync(staff => staff.StaffId == id
                                                                         && staff.Department.SpecializationId == 6);
                    if (staff == null)
                    {
                        return new NotFoundResult();
                    }
////                    return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staff));
////                }
////                return null;
////            }
////            catch (Exception ex)
////            {
////                return null;
////            }
////        }

                    return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staff));
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
////        public async Task<ActionResult<int>> PostStaffViewReturnId(Staff staff)
////        {
////            try
////            {
////                if (staff == null)
////                {
////                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
////                }

        public async Task<ActionResult<int>> PostStaffViewReturnId(Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
                }
////                if (_context == null)
////                {
////                    throw new InvalidOperationException("Database context is not initialized");
////                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
////                staff.DepartmentId = 6; // Set DepartmentId to 6 for other staffs

                staff.DepartmentId = 6; // Set DepartmentId to 6 for other staffs
////                await _context.Staff.AddAsync(staff);
////                var changesRecord = await _context.SaveChangesAsync();

                await _context.Staff.AddAsync(staff);
                var changesRecord = await _context.SaveChangesAsync();
////                if (changesRecord > 0)
////                {
////                    return new ActionResult<int>(staff.StaffId);
////                }
////                else
////                {
////                    throw new Exception("Failed to save staff record to the database");
////                }
////            }
////            catch (Exception ex)
////            {
////                Console.WriteLine($"An error occurred: {ex.Message}");
////                Console.WriteLine(ex.StackTrace);
////                return new ActionResult<int>(-1);
////            }
////        }

                if (changesRecord > 0)
                {
                    return new ActionResult<int>(staff.StaffId);
                }
                else
                {
                    throw new Exception("Failed to save staff record to the database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return new ActionResult<int>(-1);
            }
        }
////        public async Task<ActionResult<StaffViewModel>> PostStaffViewReturnRecord(Staff staff)
////        {
////            try
////            {
////                var existingDepartment = await _context.Departments
////                                                       .FirstOrDefaultAsync(d => d.DepartmentName == staff.Department.DepartmentName);

        public async Task<ActionResult<StaffViewModel>> PostStaffViewReturnRecord(Staff staff)
        {
            try
            {
                var existingDepartment = await _context.Departments
                                                       .FirstOrDefaultAsync(d => d.DepartmentName == staff.Department.DepartmentName);
////                if (existingDepartment == null)
////                {
////                    _context.Departments.Attach(staff.Department);
////                }
////                else
////                {
////                    staff.Department = existingDepartment;
////                }

                if (existingDepartment == null)
                {
                    _context.Departments.Attach(staff.Department);
                }
                else
                {
                    staff.Department = existingDepartment;
                }
////                await _context.Staff.AddAsync(staff);
////                await _context.SaveChangesAsync();

                await _context.Staff.AddAsync(staff);
                await _context.SaveChangesAsync();
////                var staffWithDepartment = await _context.Staff
////                                                        .Include(s => s.Department)
////                                                        .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);

                var staffWithDepartment = await _context.Staff
                                                        .Include(s => s.Department)
                                                        .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
////                return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staffWithDepartment));
////            }
////            catch (Exception ex)
////            {
////                Console.WriteLine($"Exception occurred: {ex.Message}");
////                Console.WriteLine(ex.StackTrace);

                return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staffWithDepartment));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
////                return new ActionResult<StaffViewModel>(new StaffViewModel 
////                { 
////                    StaffName = "An error occurred while saving the staff record." 
////                });
////            }
////        }

                return new ActionResult<StaffViewModel>(new StaffViewModel
                {
                    StaffName = "An error occurred while saving the staff record."
                });
            }
        }


       

////        public async Task<ActionResult<StaffViewModel>> PutStaffView(int id, Staff staff)
////        {
////            try
////            {
////                if (staff == null)
////                {
////                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
////                }

        public async Task<ActionResult<StaffViewModel>> PutStaffView(int id, Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
                }
////                if (_context == null)
////                {
////                    throw new InvalidOperationException("Database context is not initialized");
////                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
////                var existingStaff = await _context.Staff.FindAsync(id);

                var existingStaff = await _context.Staff.FindAsync(id);
////                if (existingStaff == null)
////                {
////                    return new ActionResult<StaffViewModel>(new StaffViewModel { StaffName = "Staff not found" });
////                }

                if (existingStaff == null)
                {
                    return new ActionResult<StaffViewModel>(new StaffViewModel { StaffName = "Staff not found" });
                }
////                if (existingStaff.Department.SpecializationId != 6)
////                {
////                    return new ActionResult<StaffViewModel>(new StaffViewModel { StaffName = "Staff does not belong to specialization 6" });
////                }

                if (existingStaff.Department.SpecializationId != 6)
                {
                    return new ActionResult<StaffViewModel>(new StaffViewModel { StaffName = "Staff does not belong to specialization 6" });
                }
////                existingStaff.StaffName = staff.StaffName;
////                existingStaff.Gender = staff.Gender;
////                existingStaff.PhoneNumber = staff.PhoneNumber;
////                existingStaff.Email = staff.Email;
////                existingStaff.Dob = staff.Dob;
////                existingStaff.Address = staff.Address;
////                existingStaff.Qualification = staff.Qualification;
////                existingStaff.CreatedDate = staff.CreatedDate;

                existingStaff.StaffName = staff.StaffName;
                existingStaff.Gender = staff.Gender;
                existingStaff.PhoneNumber = staff.PhoneNumber;
                existingStaff.Email = staff.Email;
                existingStaff.Dob = staff.Dob;
                existingStaff.Address = staff.Address;
                existingStaff.Qualification = staff.Qualification;
                existingStaff.CreatedDate = staff.CreatedDate;
////                await _context.SaveChangesAsync();

                await _context.SaveChangesAsync();
////                var staffWithDepartment = await _context.Staff
////                                                        .Include(s => s.Department)
////                                                        .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);

                var staffWithDepartment = await _context.Staff
                                                        .Include(s => s.Department)
                                                        .FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
////                return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staffWithDepartment));
////            }
////            catch (Exception ex)
////            {
////                return new ActionResult<StaffViewModel>(new StaffViewModel { StaffName = "An error occurred while updating the staff record." });
////            }
////        }

                return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staffWithDepartment));
            }
            catch (Exception ex)
            {
                return new ActionResult<StaffViewModel>(new StaffViewModel { StaffName = "An error occurred while updating the staff record." });
            }
        }






    

   

       

////                public JsonResult DeleteStaffView(int id)
////        {
////            try
////            {
////                if (_context == null)
////                {
////                    return new JsonResult(new { message = "Database context is not initialized." });
////                }

        public JsonResult DeleteStaffView(int id)
        {
            try
            {
                if (_context == null)
                {
                    return new JsonResult(new { message = "Database context is not initialized." });
                }
////                var existingStaff = _context.Staff.Find(id);

                var existingStaff = _context.Staff.Find(id);
////                if (existingStaff == null || existingStaff.Department.SpecializationId != 6)
////                {
////                    return new JsonResult(new { message = "Staff not found or does not belong to specialization 6." });
////                }

                if (existingStaff == null || existingStaff.Department.SpecializationId != 6)
                {
                    return new JsonResult(new { message = "Staff not found or does not belong to specialization 6." });
                }
////                _context.Staff.Remove(existingStaff);
////                _context.SaveChangesAsync();

                _context.Staff.Remove(existingStaff);
                _context.SaveChangesAsync();
////                return new JsonResult(new { message = "Staff deleted successfully." });
////            }
////            catch (Exception ex)
////            {
////                return new JsonResult(new { message = "An error occurred." });
////            }
////        }

                return new JsonResult(new { message = "Staff deleted successfully." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred." });
            }
        }
////        public async Task<ActionResult<IEnumerable<StaffViewModel>>> PostStaffViewByProcedureReturnRecord(Staff staff)
////        {
////            try
////            {
////                if (staff == null)
////                {
////                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
////                }

        public async Task<ActionResult<IEnumerable<StaffViewModel>>> PostStaffViewByProcedureReturnRecord(Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    throw new ArgumentNullException(nameof(staff), "Staff data is null");
                }
////                if (_context == null)
////                {
////                    throw new InvalidOperationException("Database context is not initialized");
////                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
////                var result = await _context.Staff.FromSqlRaw(
////                    "EXEC InsertStaff @StaffName, @Gender, @PhoneNumber, @Email, @Dob, @Address, @Qualification, @DepartmentId, @CreatedDate",
////                    new SqlParameter("@StaffName", staff.StaffName),
////                    new SqlParameter("@Gender", staff.Gender),
////                    new SqlParameter("@PhoneNumber", staff.PhoneNumber),
////                    new SqlParameter("@Email", staff.Email),
////                    new SqlParameter("@Dob", staff.Dob),
////                    new SqlParameter("@Address", staff.Address),
////                    new SqlParameter("@Qualification", staff.Qualification),
////                    new SqlParameter("@DepartmentId", 6), // Ensure this is 6
////                    new SqlParameter("@CreatedDate", staff.CreatedDate)
////                ).ToListAsync();

                var result = await _context.Staff.FromSqlRaw(
                    "EXEC InsertStaff @StaffName, @Gender, @PhoneNumber, @Email, @Dob, @Address, @Qualification, @DepartmentId, @CreatedDate",
                    new SqlParameter("@StaffName", staff.StaffName),
                    new SqlParameter("@Gender", staff.Gender),
                    new SqlParameter("@PhoneNumber", staff.PhoneNumber),
                    new SqlParameter("@Email", staff.Email),
                    new SqlParameter("@Dob", staff.Dob),
                    new SqlParameter("@Address", staff.Address),
                    new SqlParameter("@Qualification", staff.Qualification),
                    new SqlParameter("@DepartmentId", 6), // Ensure this is 6
                    new SqlParameter("@CreatedDate", staff.CreatedDate)
                ).ToListAsync();
////                var staffViewModels = result.Select(staff => MappingHelpers.MapToViewModel(staff)).ToList();

                var staffViewModels = result.Select(staff => MappingHelpers.MapToViewModel(staff)).ToList();
////                if (staffViewModels != null && staffViewModels.Count > 0)
////                {
////                    return new ActionResult<IEnumerable<StaffViewModel>>(staffViewModels);
////                }
////                else
////                {
////                    return null;
////                }
////            }
////            catch (Exception ex)
////            {
////                return null;
////            }
////        }

                if (staffViewModels != null && staffViewModels.Count > 0)
                {
                    return new ActionResult<IEnumerable<StaffViewModel>>(staffViewModels);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
////        public async Task<ActionResult<StaffViewModel>> GetStaffViewByPhoneNumber(string phoneNumber)
////        {
////            var staff = await _context.Staff
////                                      .Include(s => s.Department)
////                                      .FirstOrDefaultAsync(s => s.PhoneNumber == phoneNumber 
////                                                             && s.Department.SpecializationId == 6);

        public async Task<ActionResult<StaffViewModel>> GetStaffViewByPhoneNumber(string phoneNumber)
        {
            var staff = await _context.Staff
                                      .Include(s => s.Department)
                                      .FirstOrDefaultAsync(s => s.PhoneNumber == phoneNumber
                                                             && s.Department.SpecializationId == 6);
////            if (staff == null)
////            {
////                return new NotFoundResult();
////            }

            if (staff == null)
            {
                return new NotFoundResult();
            }
////            return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staff));
////        }

            return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staff));
        }
////        public async Task<ActionResult<StaffViewModel>> GetStaffViewByStaffId(int staffId)
////        {
////            var staff = await _context.Staff
////                                      .Include(s => s.Department)
////                                      .FirstOrDefaultAsync(s => s.StaffId == staffId 
////                                                             && s.Department.SpecializationId == 6);

        public async Task<ActionResult<StaffViewModel>> GetStaffViewByStaffId(int staffId)
        {
            var staff = await _context.Staff
                                      .Include(s => s.Department)
                                      .FirstOrDefaultAsync(s => s.StaffId == staffId
                                                             && s.Department.SpecializationId == 6);
////            if (staff == null)
////            {
////                return new NotFoundResult();
////            }

            if (staff == null)
            {
                return new NotFoundResult();
            }
////            return new ActionResult<StaffViewModel>(MappingHelpers.MapToViewModel(staff));
////        }



////    }
////}
//>>>>>>> 4e7aadf4c07fde4730c818842b21554d1f7551ed

