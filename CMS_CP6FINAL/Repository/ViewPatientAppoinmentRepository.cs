using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CMS_CP6FINAL.Repository
{
    public class ViewPatientAppoinmentRepository : IViewPatientAppoinmentRepository
    {
        private readonly CmsCamp6finalContext _context;

        public ViewPatientAppoinmentRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        // View today's appointments
        public async Task<IEnumerable<StartDiagnosysViewmodel>> GetTodaysAppointmentsAsync(int doctorId)
        {
            try
            {
                var today = DateTime.Today;

                var appointments = await _context.NewAppointments
                    .Include(a => a.Patient)
                    .Include(a => a.Department)
                    .Where(a => a.DoctorId == doctorId && a.AppointmentDate.HasValue && a.AppointmentDate.Value.Date == today)
                    .OrderBy(a => a.TokenNumber)
                    .Select(a => new StartDiagnosysViewmodel
                    {
                        AppointmentId = a.AppointmentId,  // Directly assign non-nullable AppointmentId
                        TokenNumber = a.TokenNumber ?? 0,  // Handle nullable TokenNumber
                        AppointmentDate = a.AppointmentDate.Value,  // Safely accessing DateTime value
                        PatientName = a.Patient.PatientName,
                        Gender = a.Patient.Gender,
                        BloodGroup = a.Patient.BloodGroup,
                        PhoneNumber = a.Patient.PhoneNumber,
                        DepartmentName = a.Department.DepartmentName
                    })
                    .ToListAsync();

                return appointments;
            }
            catch (Exception ex)
            {
                // Handle exceptions here (e.g., logging)
                throw new Exception("Error fetching today's appointments.", ex);
            }
        }

        // Search for a patient
        public IEnumerable<StartDiagnosysViewmodel> SearchPatient(string? patientName, int? appointmentId, int? tokenNumber, string? phoneNumber)
        {
            // Updated implementation
            var query = _context.NewAppointments
                .Include(a => a.Patient)
                .Include(a => a.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(patientName))
            {
                query = query.Where(a => a.Patient.PatientName.Contains(patientName));
            }

            if (appointmentId.HasValue)
            {
                query = query.Where(a => a.AppointmentId == appointmentId.Value);
            }

            if (tokenNumber.HasValue)
            {
                query = query.Where(a => a.TokenNumber == tokenNumber.Value);
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(a => a.Patient.PhoneNumber.Contains(phoneNumber));
            }

            var result = query.Select(a => new StartDiagnosysViewmodel
            {
                AppointmentId = a.AppointmentId,
                AppointmentDate = a.AppointmentDate ?? DateTime.MinValue,
                TokenNumber = a.TokenNumber ?? 0,
                PatientName = a.Patient.PatientName,
                Gender = a.Patient.Gender,
                BloodGroup = a.Patient.BloodGroup,
                PhoneNumber = a.Patient.PhoneNumber,
                DepartmentName = a.Department.DepartmentName
            });

            return result.ToList();
        }

    }
}
