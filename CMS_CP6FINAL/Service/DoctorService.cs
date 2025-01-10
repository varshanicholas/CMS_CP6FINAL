using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _repository.GetDoctors();
        }

        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            return await _repository.GetDoctorById(id);
        }

        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            return await _repository.PostDoctor(doctor);
        }

        public async Task<ActionResult<int>> PostDoctorReturnId(Doctor doctor)
        {
            return await _repository.PostDoctorReturnId(doctor);
        }

        public async Task<ActionResult<Doctor>> PutDoctor(int id, Doctor doctor)
        {
            return await _repository.PutDoctor(id, doctor);
        }

        public async Task<JsonResult> DeleteDoctor(int id)
        {
            return await Task.Run(() => _repository.DeleteDoctor(id));
        }

        public async Task<ActionResult<Doctor>> GetDoctorByPhoneNumberOrDoctorId(string phoneNumber, int doctorId)
        {
            var doctor = await _repository.GetDoctorByPhoneNumberOrDoctorId(phoneNumber, doctorId);
            if (doctor == null || doctor.Value == null)
            {
                Console.WriteLine("No doctor found in service.");
                return new ActionResult<Doctor>((Doctor)null);
            }
            else
            {
                Console.WriteLine($"Found doctor in service: {doctor.Value.Staff.StaffName}");
            }
            return doctor;
        }

        public async Task<ActionResult<Doctor>> GetDoctorByPhoneNumber(string phoneNumber)
        {
            return await _repository.GetDoctorByPhoneNumber(phoneNumber);
        }
    }
}
