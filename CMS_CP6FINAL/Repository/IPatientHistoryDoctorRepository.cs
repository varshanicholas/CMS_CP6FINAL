using CMS_CP6FINAL.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public interface IPatientHistoryDoctorRepository
    {
        Task<List<PatientHistoryViewModel>> GetPatientHistoryAsync(int patientId);
    }
}
