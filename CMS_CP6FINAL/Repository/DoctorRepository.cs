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

            // Retrieve the corresponding staff details
            var existingStaff = await _context.Staff.FindAsync(doctor.StaffId);
            if (existingStaff == null)
            {
                throw new ArgumentException("Invalid StaffId", nameof(doctor.StaffId));
            }

            // Set the Staff and Specialization references
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

            // Retrieve the corresponding staff details
            var existingStaff = await _context.Staff.FindAsync(doctor.StaffId);
            if (existingStaff == null)
            {
                throw new ArgumentException("Invalid StaffId", nameof(doctor.StaffId));
            }

            // Set the Staff and Specialization references
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
    }
}
