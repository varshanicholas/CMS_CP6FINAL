using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace CMS_CP6FINAL.Repository
{
    public interface IReceptionistRepository
    {
        #region 1-Get all Patients
        public Task<ActionResult<IEnumerable<Patient>>> GetPatient();

        #endregion



        #region -2 Get an Patient based on id


        public Task<ActionResult<Patient>> GetPatientById(int id);
        #endregion

        #region -9 Get an Patient based on Phone number


        public Task<ActionResult<Patient>> GetPatientByPhoneNumber(string ph);
        #endregion


        #region -3--insert an Patient-return Patient record

        public Task<ActionResult<Patient>> PostPatientReturnRecord(Patient Patient);
        #endregion





        #region -4 update an Patient with id and Patient


        public Task<ActionResult<Patient>> PutPatient(int id, Patient patient);
        #endregion



        #region -5 delete an Patient


        public JsonResult DeletePatient(int id);

        #endregion

        #region-6 --Get all department

        public Task<ActionResult<IEnumerable<Department>>> GetAllDepartment();

        #endregion


        #region-7 --Get Doctors by department

        public  Task<IEnumerable<Doctor>> GetDoctorsByDepartmentId(int departmentId);



        #endregion


        #region-8--Get Available Doctor By DoctorId 

        public Task<IEnumerable<DoctorAvailability>> GetDoctorAvailabilityByDoctorId(int doctorId);

        //public Task<IEnumerable<DoctorAvailability>> GetDoctorAvailabilityWeekAndDepartmentId(int departmentId, DateTime appointmentDate);



        #endregion

        public  Task<decimal?> GetConsultationFeeByDoctorId(int doctor);

        
        public Task<int> GetTotalAppointmentsByDoctorId(int doctorId);
       public Task<NewAppointment> BookAppointment(NewAppointment appointment);
        public Task<ActionResult<IEnumerable<NewAppointment>>> PostNewAppointmentByProcedure(NewAppointment appointment);



    }


}
