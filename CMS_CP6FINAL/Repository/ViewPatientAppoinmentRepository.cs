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
                    .Where(a => a.DocId == doctorId && a.AppointmentDate.Date == today)
                    .OrderBy(a => a.TokenNumber)
                    .Select(a => new StartDiagnosysViewmodel
                    {
                        AppointmentId = a.AppointmentId,
                        TokenNumber = a.TokenNumber,
                        AppointmentDate = a.AppointmentDate,
                        PatientName = a.Patient.PatientName,
                        Gender = a.Patient.Gender,
                        BloodGroup = a.Patient.BloodGroup,
                        PhoneNumber = a.Patient.PhoneNumber,
                        SpecializationName = a.Specialization.SpecializationName
                    })
                    .ToListAsync();

                return appointments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching appointments: {ex.Message}");
                throw;
            }
        }

        // Search for a patient
        public IEnumerable<StartDiagnosysViewmodel> SearchPatient(string? patientName, int? appointmentId, int? tokenNumber, string? phoneNumber)
        {
            var query = _context.NewAppointments
                .Include(a => a.Patient)
                .Include(a => a.Department)
                .Where(a =>
                    (string.IsNullOrEmpty(patientName) || a.Patient.PatientName.Contains(patientName)) &&
                    (!appointmentId.HasValue || a.AppointmentId == appointmentId.Value) &&
                    (!tokenNumber.HasValue || a.TokenNumber == tokenNumber.Value) &&
                    (string.IsNullOrEmpty(phoneNumber) || a.Patient.PhoneNumber.Contains(phoneNumber)))
                .Select(a => new StartDiagnosysViewmodel
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDate = a.AppointmentDate,
                    TokenNumber = a.TokenNumber,
                    PatientName = a.Patient.PatientName,
                    Gender = a.Patient.Gender,
                    BloodGroup = a.Patient.BloodGroup,
                    PhoneNumber = a.Patient.PhoneNumber,
                    SpecializationName = a.Department.DepartmentName
                });

            return query.ToList();
        }
    }
}