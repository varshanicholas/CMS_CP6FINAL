//using CMS_CP6FINAL.Model;
//using CMS_CP6FINAL.Repository;
//using CMS_CP6FINAL.ViewModel;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace CMS_CP6FINAL.Repositories
//{
//    public class PatientHistoryDoctorRepository : IPatientHistoryDoctorRepository
//    {
//        private readonly CmsCamp6finalContext _context;

//        public PatientHistoryDoctorRepository(CmsCamp6finalContext context)
//        {
//            _context = context;
//        }
//        public async Task<List<PatientHistoryViewModel>> GetPatientHistoryAsync(int patientId)
//        {
//            var query = from appointment in _context.NewAppointments
//                        join mainPrescription in _context.MainPrescriptions
//                            on appointment.AppointmentId equals mainPrescription.AppointmentId into mainPrescriptions
//                        from mainPrescription in mainPrescriptions.DefaultIfEmpty()
//                        join doctor in _context.Doctors
//                            on appointment.DocId equals doctor.DoctorId
//                        join department in _context.Departments
//                            on doctor.StaffId equals department.DepartmentId
//                        join medicinePrescription in _context.MedicinePrescriptions
//                            on appointment.AppointmentId equals medicinePrescription.AppointmentId into medicinePrescriptions
//                        from medicinePrescription in medicinePrescriptions.DefaultIfEmpty()
//                        join medicine in _context.Medicines
//                            on medicinePrescription.MedicineId equals medicine.MedicineId into medicines
//                        from medicine in medicines.DefaultIfEmpty()
//                        join labTestPrescription in _context.LabTestPrescriptions
//                            on appointment.AppointmentId equals labTestPrescription.AppointmentId into labTestPrescriptions
//                        from labTestPrescription in labTestPrescriptions.DefaultIfEmpty()
//                        join labTest in _context.LabTests
//                            on labTestPrescription.LabTestId equals labTest.LabTestId into labTests
//                        from labTest in labTests.DefaultIfEmpty()
//                        where appointment.PatientId == patientId
//                        select new
//                        {
//                            appointment.AppointmentDate,
//                            mainPrescription.Symptoms,
//                            mainPrescription.Diagnosis,
//                            mainPrescription.DoctorNotes,
//                            department.SpecializationId,
//                            Medicine = medicine.MedicineName,
//                            MedicinePrescription = medicinePrescription,
//                            LabTest = labTest.LabTestName,
//                            LabTestPrescription = labTestPrescription
//                        };

//            var result = await query.ToListAsync();

//            var viewModels = new List<PatientHistoryViewModel>();

//            foreach (var item in result)
//            {
//                var viewModel = new PatientHistoryViewModel
//                {
//                    AppointmentDate = item.AppointmentDate,
//                    Symptoms = item.Symptoms,
//                    Diagnosis = item.Diagnosis,
//                    DoctorNotes = item.DoctorNotes,
//                    Specialization = await _context.Specializations
//                                                    .Where(s => s.SpecializationId == item.SpecializationId)
//                                                    .Select(s => s.SpecializationName)
//                                                    .FirstOrDefaultAsync(),
//                    Medicines = item.MedicinePrescription != null
//                        ? new List<MedicineDetailsViewModel>
//                        {
//                    new MedicineDetailsViewModel
//                    {
//                        MedicineName = item.Medicine,
//                        Unit = item.MedicinePrescription?.Quantity,
//                        Dosage = item.MedicinePrescription?.Dosage,
//                        Duration = item.MedicinePrescription?.Duration,
//                        Frequency = item.MedicinePrescription?.Frequency
//                    }
//                        }
//                        : new List<MedicineDetailsViewModel>(),

//                    LabTests = item.LabTestPrescription != null
//                        ? new List<LabTestDetailsViewModel>
//                        {
//                    new LabTestDetailsViewModel
//                    {
//                        LabTestName = item.LabTest,
//                        LabTestValue = item.LabTestPrescription?.IsStatus.ToString(),
//                        LabReports = item.LabTestPrescription?.LabReports
//                    }
//                        }
//                        : new List<LabTestDetailsViewModel>()
//                };

//                viewModels.Add(viewModel);
//            }

//            return viewModels;
//        }

//    }
//}

using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                            on appointment.DocId equals doctor.DoctorId
                        join department in _context.Departments
                            on doctor.StaffId equals department.DepartmentId
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
                        where appointment.PatientId == patientId && appointment.AppointmentDate.Date == today
                        select new
                        {
                            appointment.AppointmentDate,
                            mainPrescription.Symptoms,
                            mainPrescription.Diagnosis,
                            mainPrescription.DoctorNotes,
                            department.SpecializationId,
                            Medicine = medicine.MedicineName,
                            MedicinePrescription = medicinePrescription,
                            LabTest = labTest.LabTestName,
                            LabTestPrescription = labTestPrescription
                        };

            var result = await query.ToListAsync();

            var viewModels = new List<PatientHistoryViewModel>();

            foreach (var item in result)
            {
                var viewModel = new PatientHistoryViewModel
                {
                    AppointmentDate = item.AppointmentDate,
                    Symptoms = item.Symptoms,
                    Diagnosis = item.Diagnosis,
                    DoctorNotes = item.DoctorNotes,
                    Specialization = await _context.Specializations
                                                    .Where(s => s.SpecializationId == item.SpecializationId)
                                                    .Select(s => s.SpecializationName)
                                                    .FirstOrDefaultAsync(),
                    Medicines = item.MedicinePrescription != null
                        ? new List<MedicineDetailsViewModel>
                        {
                            new MedicineDetailsViewModel
                            {
                                MedicineName = item.Medicine,
                                //Unit = item.MedicinePrescription?.Quantity,
                                Dosage = item.MedicinePrescription?.Dosage,
                                Duration = item.MedicinePrescription?.Duration,
                                Frequency = item.MedicinePrescription?.Frequency
                            }
                        }
                        : new List<MedicineDetailsViewModel>(),

                    LabTests = item.LabTestPrescription != null
                        ? new List<LabTestDetailsViewModel>
                        {
                            new LabTestDetailsViewModel
                            {
                                LabTestName = item.LabTest,
                                LabTestValue = item.LabTestPrescription?.IsStatus.ToString(),
                                LabReports = item.LabTestPrescription?.LabReports
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
