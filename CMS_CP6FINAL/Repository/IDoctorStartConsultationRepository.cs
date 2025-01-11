using System.Threading.Tasks;

public interface IDoctorStartConsultationRepository
{
    Task<DoctorStartConsultationViewModel> AddConsultationAsync(DoctorStartConsultationViewModel consultation, int createdBy);
}
