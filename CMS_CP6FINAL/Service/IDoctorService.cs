using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Service
{
    public interface IDoctorService
    {
        Task<ActionResult<IEnumerable<Doctor>>> GetDoctors();
        Task<ActionResult<Doctor>> GetDoctorById(int id);
        Task<ActionResult<Doctor>> PostDoctor(Doctor doctor);
        Task<ActionResult<int>> PostDoctorReturnId(Doctor doctor);
        Task<ActionResult<Doctor>> PutDoctor(int id, Doctor doctor);
        Task<JsonResult> DeleteDoctor(int id);
        Task<ActionResult<Doctor>> GetDoctorByPhoneNumberOrDoctorId(string phoneNumber, int doctorId);
        Task<ActionResult<Doctor>> GetDoctorByPhoneNumber(string phoneNumber);
    }
}
