
using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repositories
{
    public class DoctorStartConsultationRepository : IDoctorStartConsultationRepository
    {
        private readonly CmsCamp6finalContext _context;

        public DoctorStartConsultationRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        //        public async Task<bool> StartConsultationAsync(DoctorStartConsultationViewModel model)
        //        {
        //            using var transaction = await _context.Database.BeginTransactionAsync();

        //            try
        //            {
        //                // Insert MainPrescription
        //                var mainPrescription = new MainPrescription
        //                {
        //                    Symptoms = model.Symptoms,
        //                    Diagnosis = model.Diagnosis,
        //                    DoctorNotes = model.DoctorNotes,
        //                    AppointmentId = model.AppointmentId,
        //                    CreatedDate = DateTime.Now,
        //                    CreatedBy = 1 // Replace with logged-in user's ID
        //                };
        //                await _context.MainPrescriptions.AddAsync(mainPrescription);
        //                await _context.SaveChangesAsync();

        //                // Insert Medicines
        //                foreach (var medicine in model.Medicines)
        //                {
        //                    var medicineEntity = await _context.Medicines
        //                        .FirstOrDefaultAsync(m => m.MedicineName == medicine.MedicineName);

        //                    if (medicineEntity != null)
        //                    {
        //                        var medicinePrescription = new MedicinePrescription
        //                        {
        //                            AppointmentId = model.AppointmentId,
        //                            MedicineId = medicineEntity.MedicineId,
        //                            Quantity = medicine.Unit,
        //                            Dosage = medicine.Dosage,
        //                            Duration = medicine.Duration,
        //                            Frequency = medicine.Frequency,
        //                            IsMedicineStatus = true,
        //                            CreatedDate = DateTime.Now,
        //                            CreatedBy = 1 // Replace with logged-in user's ID
        //                        };
        //                        await _context.MedicinePrescriptions.AddAsync(medicinePrescription);
        //                    }
        //                }

        //                // Insert LabTests
        //                foreach (var labTest in model.LabTests)
        //                {
        //                    var labTestEntity = await _context.LabTests
        //                        .FirstOrDefaultAsync(lt => lt.LabTestName == labTest.LabTestName);

        //                    if (labTestEntity != null)
        //                    {
        //                        var labTestPrescription = new LabTestPrescription
        //                        {
        //                            AppointmentId = model.AppointmentId,
        //                            LabTestId = labTestEntity.LabTestId,
        //                            IsStatus = true,
        //                            IsActive = true,
        //                            CreatedDate = DateTime.Now,
        //                            CreatedBy = 1 // Replace with logged-in user's ID
        //                        };
        //                        await _context.LabTestPrescriptions.AddAsync(labTestPrescription);

        //                        // Insert LabReport
        //                        var labReport = new LabReport
        //                        {
        //                            LabTestId = labTestEntity.LabTestId,
        //                            LabTestValue = labTest.LabTestValue,
        //                            Remarks = labTest.Remarks,
        //                            AppointmentId = model.AppointmentId,
        //                            LabTestPrescriptionId = labTestPrescription.LabTestPrescriptionId
        //                        };
        //                        await _context.LabReports.AddAsync(labReport);
        //                    }
        //                }

        //                await _context.SaveChangesAsync();
        //                await transaction.CommitAsync();

        //                return true;
        //            }
        //            catch
        //            {
        //                await transaction.RollbackAsync();
        //                throw;
        //            }
        //        }

        //        Task<DoctorStartConsultationViewModel> IDoctorStartConsultationRepository.StartConsultationAsync(DoctorStartConsultationViewModel model)
        //        {
        //            throw new NotImplementedException();
        //        }
        //    }
        //}


        public async Task<DoctorStartConsultationViewModel> StartConsultationAsync(DoctorStartConsultationViewModel model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Insert MainPrescription
                var mainPrescription = new MainPrescription
                {
                    Symptoms = model.Symptoms,
                    Diagnosis = model.Diagnosis,
                    DoctorNotes = model.DoctorNotes,
                    AppointmentId = model.AppointmentId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1 // Replace with logged-in user's ID
                };
                await _context.MainPrescriptions.AddAsync(mainPrescription);
                await _context.SaveChangesAsync();

                // Insert Medicines
                foreach (var medicine in model.Medicines)
                {
                    var medicineEntity = await _context.Medicines
                        .FirstOrDefaultAsync(m => m.MedicineName == medicine.MedicineName);

                    if (medicineEntity != null)
                    {
                        var medicinePrescription = new MedicinePrescription
                        {
                            AppointmentId = model.AppointmentId,
                            MedicineId = medicineEntity.MedicineId,
                            Quantity = medicine.Unit,
                            Dosage = medicine.Dosage,
                            Duration = medicine.Duration,
                            Frequency = medicine.Frequency,
                            IsMedicineStatus = true,
                            CreatedDate = DateTime.Now,
                            CreatedBy = 1 // Replace with logged-in user's ID
                        };
                        await _context.MedicinePrescriptions.AddAsync(medicinePrescription);
                    }
                }

                // Insert LabTests
                foreach (var labTest in model.LabTests)
                {
                    var labTestEntity = await _context.LabTests
                        .FirstOrDefaultAsync(lt => lt.LabTestName == labTest.LabTestName);

                    if (labTestEntity != null)
                    {
                        var labTestPrescription = new LabTestPrescription
                        {
                            AppointmentId = model.AppointmentId,
                            LabTestId = labTestEntity.LabTestId,
                            IsStatus = true,
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            CreatedBy = 1 // Replace with logged-in user's ID
                        };
                        await _context.LabTestPrescriptions.AddAsync(labTestPrescription);

                        // Insert LabReport
                        var labReport = new LabReport
                        {
                            LabTestId = labTestEntity.LabTestId,
                            LabTestValue = labTest.LabTestValue,
                            Remarks = labTest.Remarks,
                            AppointmentId = model.AppointmentId,
                            LabTestPrescriptionId = labTestPrescription.LabTestPrescriptionId
                        };
                        await _context.LabReports.AddAsync(labReport);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Return the updated model
                return model;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}


