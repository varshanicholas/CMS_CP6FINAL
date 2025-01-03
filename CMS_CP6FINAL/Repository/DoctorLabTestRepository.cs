using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.ViewModel;
using Microsoft.EntityFrameworkCore;
namespace CMS_CP6FINAL.Repository
{
    public class DoctorLabTestRepository : IDoctorLabTestRepository
    {
        private readonly CmsCamp6finalContext _context;

        public DoctorLabTestRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorLabTestViewModel>> GetDailyLabTestsAsync(DateTime date)
        {
            return await (from appointment in _context.NewAppointments
                          join labTestPrescription in _context.LabTestPrescriptions
                              on appointment.AppointmentId equals labTestPrescription.AppointmentId
                          join labTest in _context.LabTests
                              on labTestPrescription.LabTestId equals labTest.LabTestId
                          join patient in _context.Patients
                              on appointment.PatientId equals patient.PatientId
                          where appointment.AppointmentDate.Date == date.Date
                          select new DoctorLabTestViewModel
                          {
                              PatientName = patient.PatientName,
                              LabTestName = labTest.LabTestName,
                              ResultType = labTest.ResultType,
                              ReferenceRange = labTest.ReferenceMinRange.HasValue && labTest.ReferenceMaxRange.HasValue
                                  ? $"{labTest.ReferenceMinRange}-{labTest.ReferenceMaxRange}"
                                  : "N/A",
                              Status = labTestPrescription.IsStatus.HasValue && labTestPrescription.IsStatus.Value ? "Completed" : "Pending",
                              Remarks = _context.LabReports
                                  .Where(report => report.LabTestPrescriptionId == labTestPrescription.LabTestPrescriptionId)
                                  .Select(report => report.Remarks)
                                  .FirstOrDefault()
                          }).ToListAsync();
        }
    }

}
