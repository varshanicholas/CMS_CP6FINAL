using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CMS_CP6FINAL.Repository;

namespace CMS_CP6FINAL.Repositories
{
    public class PatientHistoryDoctorRepository : IPatientHistoryDoctorRepository
    {
        private readonly CmsCamp6finalContext _context;

        public PatientHistoryDoctorRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        public async Task<List<PatientHistoryViewModel>> GetPatientHistoryAsync(int patientId)
        {
            var today = DateTime.Today;

            var query = from appointment in _context.NewAppointments
                        join mainPrescription in _context.MainPrescriptions
                            on appointment.AppointmentId equals mainPrescription.AppointmentId into mainPrescriptions
                        from mainPrescription in mainPrescriptions.DefaultIfEmpty()
                        join doctor in _context.Doctors
                            on appointment.DoctorId equals doctor.DoctorId
                        join staff in _context.Staff
                            on doctor.StaffId equals staff.StaffId
                        join department in _context.Departments
                            on doctor.SpecializationId equals department.DepartmentId
                        join medicinePrescription in _context.MedicinePrescriptions
                            on appointment.AppointmentId equals medicinePrescription.AppointmentId into medicinePrescriptions
                        from medicinePrescription in medicinePrescriptions.DefaultIfEmpty()
                        join medicine in _context.Medicines
                            on medicinePrescription.MedicineId equals medicine.MedicineId into medicines
                        from medicine in medicines.DefaultIfEmpty()
                        join labTestPrescription in _context.LabTestPrescriptions
                            on appointment.AppointmentId equals labTestPrescription.AppointmentId into labTestPrescriptions
                        from labTestPrescription in labTestPrescriptions.DefaultIfEmpty()
                        join labTest in _context.LabTests
                            on labTestPrescription.LabTestId equals labTest.LabTestId into labTests
                        from labTest in labTests.DefaultIfEmpty()
                        where appointment.PatientId == patientId && appointment.AppointmentDate.HasValue && appointment.AppointmentDate.Value.Date == today
                        select new
                        {
                            appointment.AppointmentDate,
                            mainPrescription.Symptoms,
                            mainPrescription.Diagnosis,
                            mainPrescription.DoctorNotes,
                            doctor.DoctorId,
                            staff.StaffName,
                            department.DepartmentName,
                            medicine.MedicineName,
                            medicinePrescription,
                            labTest.LabTestName,
                            labTestPrescription
                        };

            var result = await query.ToListAsync();

            var viewModels = new List<PatientHistoryViewModel>();

            foreach (var item in result)
            {
                var viewModel = new PatientHistoryViewModel
                {
                    AppointmentDate = (DateTime)item.AppointmentDate,
                    Symptoms = item.Symptoms,
                    Diagnosis = item.Diagnosis,
                    DoctorNotes = item.DoctorNotes,
                    Specialization = item.DepartmentName, // Department name will represent specialization here
                    StaffName = item.StaffName, // Adding the doctor's name
                    Medicines = item.medicinePrescription != null
                        ? new List<MedicineDetailsViewModel>
                        {
                            new MedicineDetailsViewModel
                            {
                                MedicineName = item.MedicineName,
                                Dosage = item.medicinePrescription?.Dosage,
                                Duration = item.medicinePrescription?.Duration,
                                Frequency = item.medicinePrescription?.Frequency
                            }
                        }
                        : new List<MedicineDetailsViewModel>(),

                    LabTests = item.labTestPrescription != null
                        ? new List<LabTestDetailsViewModel>
                        {
                            new LabTestDetailsViewModel
                            {
                                LabTestName = item.LabTestName,
                                LabTestValue = item.labTestPrescription?.IsStatus.ToString(),
                                LabReports = item.labTestPrescription?.LabReports
                            }
                        }
                        : new List<LabTestDetailsViewModel>()
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
