using CMS_CP6FINAL.ViewModel;

namespace CMS_CP6FINAL.Repository
{
    public interface IPatientHistoryDoctorRepository
    {
        Task<List<PatientHistoryViewModel>> GetPatientHistoryAsync(int patientId);
    }
}
