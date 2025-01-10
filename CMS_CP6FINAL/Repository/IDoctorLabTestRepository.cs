using System.Collections.Generic;
using System.Threading.Tasks;


namespace CMS_CP6FINAL.Repository
{
    public interface IDoctorLabTestRepository
    {
        Task<IEnumerable<DoctorLabTestViewModel>> GetLabTestReportsByAppointmentIdAsync(int appointmentId);
    }
}
