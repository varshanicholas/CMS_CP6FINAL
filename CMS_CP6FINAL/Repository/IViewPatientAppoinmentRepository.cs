//using CMS_CP6FINAL.ViewModel;

public interface IViewPatientAppoinmentRepository
{
    Task<IEnumerable<StartDiagnosysViewmodel>> GetTodaysAppointmentsAsync(int doctorId);
    IEnumerable<StartDiagnosysViewmodel> SearchPatient(string? patientName, int? appointmentId, int? tokenNumber, string? phoneNumber);

}
