using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly CmsCamp6finalContext _context;

        public DoctorRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors
                                 .Include(doctor => doctor.Staff)
                                 .ThenInclude(staff => staff.Department)
                                 .Include(doctor => doctor.Specialization)
                                 .ToListAsync();
        }



        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            return await _context.Doctors
                                 .Include(doctor => doctor.Staff)
                                 .ThenInclude(staff => staff.Department)
                                 .Include(doctor => doctor.Specialization)
                                 .FirstOrDefaultAsync(doctor => doctor.DoctorId == id);
        }
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor), "Doctor data is null");
            }

            var allowedDepartments = new Dictionary<int, int>
    {
        { 2, 1 }, { 5, 2 }, { 8, 3 },
        { 9, 4 }, { 10, 5 }, { 11, 6 }, { 12, 7 }
    };

            var existingStaff = await _context.Staff.FindAsync(doctor.StaffId);
            if (existingStaff == null)
            {
                throw new ArgumentException("Invalid StaffId", nameof(doctor.StaffId));
            }

            if (!allowedDepartments.ContainsKey(existingStaff.DepartmentId))
            {
                return new BadRequestObjectResult("Staff belongs to an unauthorized department.");
            }

            if (allowedDepartments[existingStaff.DepartmentId] != doctor.SpecializationId)
            {
                return new BadRequestObjectResult("Specialization does not match the department.");
            }

            doctor.Staff = existingStaff;
            var specialization = await _context.Specializations.FindAsync(doctor.SpecializationId);
            if (specialization == null)
            {
                throw new ArgumentException("Invalid SpecializationId", nameof(doctor.SpecializationId));
            }
            doctor.Specialization = specialization;

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return await _context.Doctors
                                 .Include(d => d.Staff)
                                 .ThenInclude(s => s.Department)
                                 .Include(d => d.Specialization)
                                 .FirstOrDefaultAsync(d => d.DoctorId == doctor.DoctorId);
        }
        public async Task<ActionResult<int>> PostDoctorReturnId(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor), "Doctor data is null");
            }

            var allowedDepartments = new Dictionary<int, int>
    {
        { 2, 1 }, { 5, 2 }, { 8, 3 },
        { 9, 4 }, { 10, 5 }, { 11, 6 }, { 12, 7 }
    };

            var existingStaff = await _context.Staff.FindAsync(doctor.StaffId);
            if (existingStaff == null)
            {
                throw new ArgumentException("Invalid StaffId", nameof(doctor.StaffId));
            }

            if (!allowedDepartments.ContainsKey(existingStaff.DepartmentId))
            {
                return new BadRequestObjectResult("Staff belongs to an unauthorized department.");
            }

            if (allowedDepartments[existingStaff.DepartmentId] != doctor.SpecializationId)
            {
                return new BadRequestObjectResult("Specialization does not match the department.");
            }

            doctor.Staff = existingStaff;
            var specialization = await _context.Specializations.FindAsync(doctor.SpecializationId);
            if (specialization == null)
            {
                throw new ArgumentException("Invalid SpecializationId", nameof(doctor.SpecializationId));
            }
            doctor.Specialization = specialization;

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor.DoctorId;
        }

        public async Task<ActionResult<Doctor>> PutDoctor(int id, Doctor doctor)
        {
            var existingDoctor = await _context.Doctors.FindAsync(id);
            if (existingDoctor == null)
            {
                return null;
            }

            existingDoctor.ConsultationFee = doctor.ConsultationFee;
            existingDoctor.SpecializationId = doctor.SpecializationId;
            existingDoctor.StaffId = doctor.StaffId;
            existingDoctor.IsActive = doctor.IsActive;

            // Update the staff entity separately if needed
            if (doctor.Staff != null && _context.Staff.Any(s => s.StaffId == doctor.StaffId))
            {
                var existingStaff = _context.Staff.Find(doctor.StaffId);
                existingStaff.StaffName = doctor.Staff.StaffName;
                existingStaff.Gender = doctor.Staff.Gender;
                existingStaff.PhoneNumber = doctor.Staff.PhoneNumber;
                existingStaff.Email = doctor.Staff.Email;
                existingStaff.Dob = doctor.Staff.Dob;
                existingStaff.Address = doctor.Staff.Address;
                existingStaff.Qualification = doctor.Staff.Qualification;
                existingStaff.DepartmentId = doctor.Staff.DepartmentId;
                existingStaff.CreatedDate = doctor.Staff.CreatedDate;
                existingStaff.IsActive = doctor.Staff.IsActive;
            }

            await _context.SaveChangesAsync();

            return await _context.Doctors
                                 .Include(d => d.Staff)
                                 .ThenInclude(s => s.Department)
                                 .Include(d => d.Specialization)
                                 .FirstOrDefaultAsync(d => d.DoctorId == doctor.DoctorId);
        }

        public JsonResult DeleteDoctor(int id)
        {
            var existingDoctor = _context.Doctors.Find(id);
            if (existingDoctor == null)
            {
                return new JsonResult(new { success = false, message = "Doctor not found" }) { StatusCode = 404 };
            }

            existingDoctor.IsActive = false;
            _context.SaveChanges();
            return new JsonResult(new { success = true, message = "Doctor marked as inactive" }) { StatusCode = 200 };
        }

        public async Task<ActionResult<Doctor>> GetDoctorByPhoneNumberOrDoctorId(string phoneNumber, int doctorId)
        {
            var doctor = await _context.Doctors
                                       .Include(d => d.Staff)
                                       .ThenInclude(s => s.Department)
                                       .Include(d => d.Specialization)
                                       .FirstOrDefaultAsync(d => d.Staff.PhoneNumber == phoneNumber || d.DoctorId == doctorId);
            return doctor;
        }

        public async Task<ActionResult<Doctor>> GetDoctorByPhoneNumber(string phoneNumber)
        {
            return await _context.Doctors
                                 .Include(d => d.Staff)
                                 .ThenInclude(s => s.Department)
                                 .Include(d => d.Specialization)
                                 .FirstOrDefaultAsync(d => d.Staff.PhoneNumber == phoneNumber);
        }


        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffsByDepartment()
        {
            var departments = new List<string>
            {
                "Cardiology", "Oncology", "Neurology",
                "Pediatrics", "Orthopedics", "Gastroenterology", "Dermatology"
            };

            return await _context.Staff
                                 .Include(staff => staff.Department)
                                 .Where(staff => departments.Contains(staff.Department.DepartmentName))
                                 .ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffsNotInDoctorTable()
        {
            var departments = new List<string>
        {
        "Cardiology", "Oncology", "Neurology",
        "Pediatrics", "Orthopedics", "Gastroenterology", "Dermatology"
        };

            return await _context.Staff
                                 .Include(staff => staff.Department)
                                 .Where(staff => !_context.Doctors.Any(doctor => doctor.StaffId == staff.StaffId) &&
                                                 departments.Contains(staff.Department.DepartmentName))
                                 .ToListAsync();
        }


    //    public async Task<ActionResult<Doctor>> PostDoctorById(int staffId, Doctor doctor)
    //    {
    //        if (doctor == null)
    //        {
    //            throw new ArgumentNullException(nameof(doctor), "Doctor data is null");
    //        }

    //        // Define allowed departments and their valid specializations
    //        var allowedDepartments = new Dictionary<string, List<int>>
    //{
    //    { "Cardiology", new List<int> {  1 } }, // Cardiology can have specialization 1 and 2
    //    { "Oncology", new List<int> { 2} }, // Oncology can have specialization 3
    //    { "Neurology", new List<int> { 3} }, // Neurology can have specialization 4
    //    { "Pediatrics", new List<int> { 4 } }, // Pediatrics can have specialization 5
    //    { "Orthopedics", new List<int> { 5} }, // Orthopedics can have specialization 6
    //    { "Gastroenterology", new List<int> {  6} }, // Gastroenterology can have specialization 7
    //    { "Dermatology", new List<int> { 7} } // Dermatology can have specialization 8
    //};

    //        // Check if the staff exists
    //        var existingStaff = await _context.Staff
    //                                           .Include(staff => staff.Department)
    //                                           .FirstOrDefaultAsync(staff => staff.StaffId == staffId);

    //        if (existingStaff == null)
    //        {
    //            return new BadRequestObjectResult("Staff with the given ID does not exist.");
    //        }

    //        // Check if the staff belongs to an allowed department
    //        if (!allowedDepartments.ContainsKey(existingStaff.Department.DepartmentName))
    //        {
    //            return new BadRequestObjectResult("Staff belongs to an unauthorized department.");
    //        }

    //        // Check if the specialization matches the department
    //        if (!allowedDepartments[existingStaff.Department.DepartmentName].Contains(doctor.SpecializationId))
    //        {
    //            return new BadRequestObjectResult("Specialization does not match the department.");
    //        }

    //        // Check if the staff is already a doctor
    //        var existingDoctor = await _context.Doctors
    //                                           .FirstOrDefaultAsync(d => d.StaffId == staffId);
    //        if (existingDoctor != null)
    //        {
    //            return new BadRequestObjectResult("This staff member is already a doctor.");
    //        }

    //        // Assign the staff to the doctor entity
    //        doctor.StaffId = staffId;

    //        // Find and assign specialization
    //        var specialization = await _context.Specializations.FindAsync(doctor.SpecializationId);
    //        if (specialization == null)
    //        {
    //            return new BadRequestObjectResult("Invalid SpecializationId.");
    //        }
    //        doctor.Specialization = specialization;

    //        // Add the new doctor
    //        _context.Doctors.Add(doctor);
    //        await _context.SaveChangesAsync();

    //        // Return the added doctor
    //        return await _context.Doctors
    //                             .Include(d => d.Staff)
    //                             .ThenInclude(s => s.Department)
    //                             .Include(d => d.Specialization)
    //                             .FirstOrDefaultAsync(d => d.DoctorId == doctor.DoctorId);
    //    }



//        public async Task<ActionResult<Doctor>> PostDoctorById(int staffId, Doctor doctor)
//{
//    if (doctor == null)
//    {
//        throw new ArgumentNullException(nameof(doctor), "Doctor data is null");
//    }

//    // Define allowed departments and their valid specializations
//    var allowedDepartments = new Dictionary<string, List<int>>
//    {
//        { "Cardiology", new List<int> { 1 } }, 
//        { "Oncology", new List<int> { 2 } }, 
//        { "Neurology", new List<int> { 3 } }, 
//        { "Pediatrics", new List<int> { 4 } }, 
//        { "Orthopedics", new List<int> { 5 } }, 
//        { "Gastroenterology", new List<int> { 6 } }, 
//        { "Dermatology", new List<int> { 7 } }
//    };

//    // Check if the staff exists
//    var existingStaff = await _context.Staff
//                                       .Include(staff => staff.Department)
//                                       .FirstOrDefaultAsync(staff => staff.StaffId == staffId);

//    if (existingStaff == null)
//    {
//        return new BadRequestObjectResult("Staff with the given ID does not exist.");
//    }

//    // Check if the staff belongs to an allowed department
//    if (!allowedDepartments.ContainsKey(existingStaff.Department.DepartmentName))
//    {
//        return new BadRequestObjectResult("Staff belongs to an unauthorized department.");
//    }

//    // Check if the specialization matches the department
//    if (!allowedDepartments[existingStaff.Department.DepartmentName].Contains(doctor.SpecializationId))
//    {
//        return new BadRequestObjectResult("Specialization does not match the department.");
//    }

//    // Check if the staff is already a doctor
//    var existingDoctor = await _context.Doctors
//                                       .FirstOrDefaultAsync(d => d.StaffId == staffId);
//    if (existingDoctor != null)
//    {
//        return new BadRequestObjectResult("This staff member is already a doctor.");
//    }



//    // Ensure doctor is linked to the existing staff
//    doctor.StaffId = staffId;
//    doctor.Staff = existingStaff;

//    // Find and assign specialization
//    var specialization = await _context.Specializations.FindAsync(doctor.SpecializationId);
//    if (specialization == null)
//    {
//        return new BadRequestObjectResult("Invalid SpecializationId.");
//    }
//    doctor.Specialization = specialization;

//    // Add the new doctor without creating a new staff
//    _context.Doctors.Add(doctor);
//    await _context.SaveChangesAsync();

//    // Return the added doctor
//    var addedDoctor = await _context.Doctors
//                                     .Include(d => d.Staff)
//                                     .ThenInclude(s => s.Department)
//                                     .Include(d => d.Specialization)
//                                     .FirstOrDefaultAsync(d => d.DoctorId == doctor.DoctorId);

//    return addedDoctor ?? new BadRequestObjectResult("Failed to retrieve the added doctor.");
//}


        public async Task<ActionResult<Doctor>> PostDoctorById(int staffId, Doctor doctor)
{
    if (doctor == null)
    {
        throw new ArgumentNullException(nameof(doctor), "Doctor data is null");
    }

    // Define allowed departments and their valid specializations
    var allowedDepartments = new Dictionary<string, List<int>>
    {
        { "Cardiology", new List<int> { 1 } }, 
        { "Oncology", new List<int> { 2 } }, 
        { "Neurology", new List<int> { 3 } }, 
        { "Pediatrics", new List<int> { 4 } }, 
        { "Orthopedics", new List<int> { 5 } }, 
        { "Gastroenterology", new List<int> { 6 } }, 
        { "Dermatology", new List<int> { 7 } }
    };

    // Check if the staff exists
    var existingStaff = await _context.Staff
                                       .Include(staff => staff.Department)
                                       .FirstOrDefaultAsync(staff => staff.StaffId == staffId);

    if (existingStaff == null)
    {
        return new BadRequestObjectResult("Staff with the given ID does not exist.");
    }

    // Check if the staff belongs to an allowed department
    if (!allowedDepartments.ContainsKey(existingStaff.Department.DepartmentName))
    {
        return new BadRequestObjectResult("Staff belongs to an unauthorized department.");
    }

    // Check if the specialization matches the department
    if (!allowedDepartments[existingStaff.Department.DepartmentName].Contains(doctor.SpecializationId))
    {
        return new BadRequestObjectResult("Specialization does not match the department.");
    }

    // Check if the staff is already a doctor
    var existingDoctor = await _context.Doctors
                                       .FirstOrDefaultAsync(d => d.StaffId == staffId);
    if (existingDoctor != null)
    {
        return new BadRequestObjectResult("This staff member is already a doctor.");
    }

    // Ensure doctor is linked to the existing staff
    doctor.StaffId = staffId;
    doctor.Staff = existingStaff;

    // Find and assign specialization
    var specialization = await _context.Specializations.FindAsync(doctor.SpecializationId);
    if (specialization == null)
    {
        return new BadRequestObjectResult("Invalid SpecializationId.");
    }
    doctor.Specialization = specialization;

    // Add the new doctor without creating a new staff
    _context.Doctors.Add(doctor);
    await _context.SaveChangesAsync();

    // Attempt to retrieve the added doctor
    var addedDoctor = await _context.Doctors
                                     .Include(d => d.Staff)
                                     .ThenInclude(s => s.Department)
                                     .Include(d => d.Specialization)
                                     .FirstOrDefaultAsync(d => d.DoctorId == doctor.DoctorId);

    // Check if the doctor retrieval failed
    if (addedDoctor == null)
    {
        return new BadRequestObjectResult("Failed to retrieve the added doctor.");
    }

    return addedDoctor;
}

        public async Task<IEnumerable<Specialization>> GetAllSpecializationsAsync()
        {
            return await _context.Specializations.ToListAsync();
        }



    }
}
