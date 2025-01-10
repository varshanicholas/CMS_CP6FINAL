using System.Collections.Generic;
using System.Threading.Tasks;

<<<<<<< HEAD
//namespace CMS_CP6FINAL.Repository
//{
//    public interface IDoctorLabTestRepository
//    {
//        Task<IEnumerable<DoctorLabTestViewModel>> GetDailyLabTestsAsync(DateTime date);
//    }
=======
namespace CMS_CP6FINAL.Repository
{
    public interface IDoctorLabTestRepository
    {
        Task<IEnumerable<DoctorLabTestViewModel>> GetLabTestReportsByAppointmentIdAsync(int appointmentId);
    }
}
