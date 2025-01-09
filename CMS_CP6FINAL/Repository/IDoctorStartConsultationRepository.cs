using CMS_CP6FINAL.ViewModel;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public interface IDoctorStartConsultationRepository
    {
        //Task GetConsultationDetailsAsync(int appointmentId);
        Task<DoctorStartConsultationViewModel> StartConsultationAsync(DoctorStartConsultationViewModel model);
    }
}
