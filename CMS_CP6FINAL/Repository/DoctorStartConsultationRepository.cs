//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using CMS_CP6FINAL.Model;
//using Microsoft.EntityFrameworkCore;

//public class DoctorStartConsultationRepository : IDoctorStartConsultationRepository
//{
//    private readonly CmsCamp6finalContext _context;

//    public DoctorStartConsultationRepository(CmsCamp6finalContext context)
//    {
//        _context = context;
//    }

//    public async Task<DoctorStartConsultationViewModel> AddConsultationAsync(DoctorStartConsultationViewModel consultation, int createdBy)
//    {
//        using var transaction = await _context.Database.BeginTransactionAsync();

//        try
//        {
//            // Step 1: Add MainPrescription
//            var mainPrescription = new MainPrescription
//            {
//                Symptoms = consultation.Symptoms,
//                Diagnosis = consultation.Diagnosis,
//                DoctorNotes = consultation.DoctorNotes,
//                AppointmentId = consultation.AppointmentId,
//                CreatedDate = DateTime.Now,
//                CreatedBy = createdBy
//            };
//            _context.MainPrescriptions.Add(mainPrescription);
//            await _context.SaveChangesAsync();

//            // Step 2: Add Medicines
//            foreach (var medicine in consultation.Medicines)
//            {
//                var medicineEntity = await _context.Medicines
//                    .FirstOrDefaultAsync(m => m.MedicineName == medicine.MedicineName);

//                if (medicineEntity == null) continue;

//                var medicinePrescription = new MedicinePrescription
//                {
//                    MainPrescriptionId = mainPrescription.MainPrescriptionId,
//                    AppointmentId = consultation.AppointmentId,
//                    MedicineId = medicineEntity.MedicineId,
//                    Quantity = medicine.Quantity,
//                    Dosage = medicine.Dosage,
//                    //Duration = medicine.Duration,
//                    Frequency = medicine.Frequency,
//                    IsMedicineStatus = true,
//                    CreatedDate = DateTime.Now,
//                    CreatedBy = createdBy
//                };
//                _context.MedicinePrescriptions.Add(medicinePrescription);
//            }

//            await _context.SaveChangesAsync();

//            // Step 3: Add Lab Tests
//            foreach (var labTest in consultation.LabTests)
//            {
//                var labTestEntity = await (from lt in _context.LabTests
//                                           join ltc in _context.LabTestCategories on lt.CategoryId equals ltc.CategoryId
//                                           where lt.LabTestName == labTest.LabTestName && ltc.CategoryName == labTest.CategoryName
//                                           select lt).FirstOrDefaultAsync();

//                if (labTestEntity == null) continue;

//                var labTestPrescription = new LabTestPrescription
//                {
//                    AppointmentId = consultation.AppointmentId,
//                    LabTestId = labTestEntity.LabTestId,
//                    IsStatus = false,
//                    IsActive = true,
//                    CreatedDate = DateTime.Now,
//                    CreatedBy = createdBy
//                };
//                _context.LabTestPrescriptions.Add(labTestPrescription);
//            }

//            await _context.SaveChangesAsync();

//            await transaction.CommitAsync();

//            return consultation; // Return the same consultation object as a response
//        }
//        catch
//        {
//            await transaction.RollbackAsync();
//            throw;
//        }
//    }
//}


using System;
using System.Linq;
using System.Threading.Tasks;
using CMS_CP6FINAL.Model;
using Microsoft.EntityFrameworkCore;

public class DoctorStartConsultationRepository : IDoctorStartConsultationRepository
{
    private readonly CmsCamp6finalContext _context;

    public DoctorStartConsultationRepository(CmsCamp6finalContext context)
    {
        _context = context;
    }

    public async Task<DoctorStartConsultationViewModel> AddConsultationAsync(DoctorStartConsultationViewModel consultation, int createdBy)
    {
        if (consultation == null)
            throw new ArgumentNullException(nameof(consultation), "Consultation data cannot be null.");

        // Validate required fields
        if (string.IsNullOrWhiteSpace(consultation.Symptoms))
            throw new ArgumentException("Symptoms cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(consultation.Diagnosis))
            throw new ArgumentException("Diagnosis cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(consultation.DoctorNotes))
            throw new ArgumentException("DoctorNotes cannot be null or empty.");

        // Ensure that Medicines and LabTests are initialized if null
        consultation.Medicines ??= new List<MedicineViewModel>();
        consultation.LabTests ??= new List<LabTestViewModel>();

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Add MainPrescription
            var mainPrescription = new MainPrescription
            {
                Symptoms = consultation.Symptoms,
                Diagnosis = consultation.Diagnosis,
                DoctorNotes = consultation.DoctorNotes,
                AppointmentId = consultation.AppointmentId,
                CreatedDate = DateTime.Now,
                CreatedBy = createdBy
            };
            _context.MainPrescriptions.Add(mainPrescription);
            await _context.SaveChangesAsync();

            // Add Medicines (Ensure the list is not empty)
            if (consultation.Medicines.Any())
            {
                foreach (var medicine in consultation.Medicines)
                {
                    if (string.IsNullOrWhiteSpace(medicine.MedicineName))
                        throw new ArgumentException("MedicineName cannot be null or empty.");

                    var medicineEntity = await _context.Medicines
                        .FirstOrDefaultAsync(m => m.MedicineName == medicine.MedicineName);

                    if (medicineEntity == null) continue; // Skip if medicine not found

                    var medicinePrescription = new MedicinePrescription
                    {
                        MainPrescriptionId = mainPrescription.MainPrescriptionId,
                        AppointmentId = consultation.AppointmentId,
                        MedicineId = medicineEntity.MedicineId,
                        Quantity = medicine.Quantity,
                        Dosage = medicine.Dosage,
                        Frequency = medicine.Frequency,
                        IsMedicineStatus = true,
                        CreatedDate = DateTime.Now,
                        CreatedBy = createdBy
                    };
                    _context.MedicinePrescriptions.Add(medicinePrescription);
                }

                await _context.SaveChangesAsync();
            }

            // Add Lab Tests (Ensure the list is not empty)
            if (consultation.LabTests.Any())
            {
                foreach (var labTest in consultation.LabTests)
                {
                    if (string.IsNullOrWhiteSpace(labTest.LabTestName))
                        throw new ArgumentException("LabTestName cannot be null or empty.");

                    var labTestEntity = await (from lt in _context.LabTests
                                               join ltc in _context.LabTestCategories on lt.CategoryId equals ltc.CategoryId
                                               where lt.LabTestName == labTest.LabTestName && ltc.CategoryName == labTest.CategoryName
                                               select lt).FirstOrDefaultAsync();

                    if (labTestEntity == null) continue; // Skip if lab test not found

                    var labTestPrescription = new LabTestPrescription
                    {
                        AppointmentId = consultation.AppointmentId,
                        LabTestId = labTestEntity.LabTestId,
                        IsStatus = false,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedBy = createdBy
                    };
                    _context.LabTestPrescriptions.Add(labTestPrescription);
                }

                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return consultation; // Return the same consultation object as a response
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException("Error while processing consultation.", ex);
        }
    }

}
