using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CMS_CP6FINAL.Model;

namespace CMS_CP6FINAL.Repository
{
    public class DoctorLabTestRepository : IDoctorLabTestRepository
    {
        private readonly CmsCamp6finalContext _context;

//        public DoctorLabTestRepository(CmsCamp6finalContext context)
//        {
//            _context = context;
//        }

        public async Task<IEnumerable<DoctorLabTestViewModel>> GetLabTestReportsByAppointmentIdAsync(int appointmentId)
        {
            var reports = await (from ltp in _context.LabTestPrescriptions
                                 join lt in _context.LabTests on ltp.LabTestId equals lt.LabTestId
                                 join ltc in _context.LabTestCategories on lt.CategoryId equals ltc.CategoryId
                                 join p in _context.Patients on ltp.AppointmentId equals appointmentId
                                 join na in _context.NewAppointments on p.PatientId equals na.PatientId
                                 where na.AppointmentId == appointmentId
                                 join lr in _context.LabReports on ltp.LabTestPrescriptionId equals lr.LabTestPrescriptionId into labReportJoin
                                 from lr in labReportJoin.DefaultIfEmpty()
                                 select new DoctorLabTestViewModel
                                 {
                                     PatientName = p.PatientName,
                                     Gender = p.Gender,
                                     PhoneNumber = p.PhoneNumber,
                                     BloodGroup = p.BloodGroup,
                                     LabTestName = lt.LabTestName,
                                     CategoryName = ltc.CategoryName,
                                     Status = ltp.IsStatus.HasValue && ltp.IsStatus.Value ? "Completed" : "Pending",
                                     ResultType = lt.ResultType,
                                     ReferenceMinRange = lt.ReferenceMinRange,
                                     ReferenceMaxRange = lt.ReferenceMaxRange,
                                     SampleRequired = lt.SampleRequired,
                                     CreatedDate = ltp.CreatedDate.HasValue ? ltp.CreatedDate.Value.ToString("yyyy-MM-dd") : null, // Corrected nullable DateTime
                                     Remarks = lr != null ? lr.Remarks : null
                                 }).ToListAsync();

            return reports;
        }
    }
}
