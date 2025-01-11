using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CMS_CP6FINAL.Repository
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        //EF--VirtualDatabase

        private readonly CmsCamp6finalContext _context;

        private Dictionary<string, int> weekDays = new Dictionary<string, int> { {"SUN", 1}, { "MON", 2}, { "TUE", 3 }, { "WED", 4 }, { "THU", 5 }, { "FRI", 6 }, { "SAT", 7 } };

        //DI -Constructor injection

        public ReceptionistRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }


        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Patients.ToListAsync();
                }
                //return an empty list if context is null
                return new List<Patient>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            try
            {
                if (_context != null)
                {
                    //find the Patients by id 

                    var pat = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
                    return pat;
                }

                return null;

            }

            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<ActionResult<Patient>> PostPatientReturnRecord(Patient pat)
        {
            try
            {
                //check if Patient object is not null

                if (pat == null)
                {
                    throw new ArgumentNullException(nameof(pat), "Patient Data is Null");
                }

                //ensure the context is not null

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //add the Patient record to the dbcontext

                await _context.Patients.AddAsync(pat);

                //save changes to the database

                await _context.SaveChangesAsync();

                //retrieve the employee with the related departments

                var patientDetails = await _context.Patients
                    .FirstOrDefaultAsync(p => p.PatientId == pat.PatientId);//eager load


                //return the added Patient record

                return patientDetails;


            }

            catch (Exception ex)
            {
                return null;
            }
        }




        public async Task<ActionResult<Patient>> PutPatient(int id, Patient pat)
        {
            try
            {
                if (pat == null)
                {
                    throw new ArgumentNullException(nameof(pat), "Employee Data is Null");
                }

                //ensure the context is not null

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //find the employee by ID
                var existingPatient = await _context.Patients.FindAsync(id);

                if (existingPatient == null)
                {
                    return null;
                }

                //Map values with fields -update
                //existing Patients data and newdata

                existingPatient.PatientName = pat.PatientName;
                existingPatient.Dob = pat.Dob;
                existingPatient.Gender = pat.Gender;
                existingPatient.BloodGroup = pat.BloodGroup;
                existingPatient.PhoneNumber = pat.PhoneNumber;
                existingPatient.Address = pat.Address;
                existingPatient.Email = pat.Email;
                existingPatient.CreatedDate = pat.CreatedDate;
                existingPatient.IsActive = pat.IsActive;


                //save changes to the database

                await _context.SaveChangesAsync();

                //retrieve the Patients with the related departments

                var patientDetails = await _context.Patients
                .FirstOrDefaultAsync(e => e.PatientId == pat.PatientId);//eager load


                //return the added patient Details record

                return patientDetails;

            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult DeletePatient(int id)
        {
            {
                try
                {

                    //check if patient object is not null
                    if (id == null)
                    {
                        return new JsonResult(new
                        {
                            success = false,
                            message = "invalid Patient Id id"

                        })
                        {
                            StatusCode = StatusCodes.Status400BadRequest
                        };

                    }

                    //ensure the context is not null

                    if (_context == null)
                    {

                        return new JsonResult(new
                        {

                            success = false,
                            message = "Database context is not initialized. "

                        })
                        {
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                    }

                    //find the employee by ID
                    var existingPatient = _context.Patients.Find(id);

                    if (existingPatient == null)
                    {
                        return new JsonResult(new
                        {

                            success = false,
                            message = "Patient Not Found . "

                        })
                        {
                            StatusCode = StatusCodes.Status400BadRequest
                        };
                    }
                    //remove the employee record from the database

                    _context.Patients.Remove(existingPatient);

                    //save changes to the database

                    _context.SaveChangesAsync();

                    return new JsonResult(new
                    {

                        success = false,
                        message = "Patient Deleted Successfully . "

                    })
                    {
                        StatusCode = StatusCodes.Status200OK
                    };




                }

                catch (Exception ex)
                {

                    return new JsonResult(new
                    {

                        success = false,
                        message = "An error accured "

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }




            }
        }


        //Get all department

        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Departments.ToListAsync();
                }
                //return an empty list if context is null
                return new List<Department>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //public async Task<IEnumerable<DoctorAvailability>> GetDoctorAvailabilityWeekAndDepartmentId(int departmentId, DateTime appointmentDate)
        //{
        //    try
        //    {
        //        var weekId = weekDays.GetValueOrDefault(appointmentDate.ToString("ddd").ToUpper());
        //        if (_context.DoctorAvailabilities != null)
        //        {
        //            return await _context.DoctorAvailabilities
        //                .Where(d => d.WeekId == weekId)
        //                .Include(d => d.Doc)
        //                .ThenInclude(d => d.Staff)
        //                .Where(d => d.Doc.Staff.DepartmentId == departmentId)
        //                //.Include(d => d.NewAppointments.Where(a => a.AppointmentDate.Date != appointmentDate.Date))
        //                .ToListAsync();
        //        }

        //        return new List<DoctorAvailability>();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Optionally log the exception
        //        return null; // or handle error more appropriately based on your requirements
        //    }
        //}


        #region 8 - Insert Appointment and return success
        public async Task<ActionResult<NewAppointment>> BookAppointment(NewAppointment appointment)
        {
            try
            {
                if (appointment == null || _context == null)
                    throw new ArgumentNullException(nameof(appointment));

                // Check if the doctor is available on the given date
                var dailyAvailability = await _context.NewAppointments
                    .FirstOrDefaultAsync(d => d.DoctorId == appointment.DoctorId);

                
              
                // Save the appointment
                await _context.NewAppointments.AddAsync(appointment);
                await _context.SaveChangesAsync();

                return appointment; // Return the booked appointment
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error booking appointment: {ex.Message}");
            }
        }

        private async Task<int?> GetDoctorAvailabilityByDoctorId(NewAppointment appointment, object doctorId)
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task<NewAppointment> SaveAppointment(NewAppointment appointment)
        {
           

            try
            {
                

                if (appointment == null)
                {
                    throw new ArgumentNullException(nameof(appointment), "appointment Data is Null");
                }

                

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

            

                await _context.NewAppointments.AddAsync(appointment);

                

                await _context.SaveChangesAsync();

                

                var appointmentDetails = await _context.NewAppointments
                    .FirstOrDefaultAsync(p => p.AppointmentId == appointment.AppointmentId);//eager load


                

                return appointmentDetails;


            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Patient>> GetPatientByPhoneNumber(string ph)
        {
            try
            {
                if (_context != null)
                {
                    //find the Patients by id 

                    var pat = await _context.Patients.FirstOrDefaultAsync(p => p.PhoneNumber == ph);
                    return pat;
                }

                return null;

            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<decimal?> GetConsultationFeeByDoctorId(int doctor)
        {
            try
            {
                if (_context != null)
                {
                    var doctors = await _context.Doctors
                        .FirstOrDefaultAsync(d => d.DoctorId == doctor);

                    return doctors?.ConsultationFee;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }


        public async Task<IEnumerable<Doctor>> GetDoctorsByDepartmentId(int departmentId)
        {
            try
            {
                if (_context != null)
                {
                    // Fetch doctors where the associated staff's DepartmentId matches the provided value
                    var doctors = await _context.Doctors
                        .Include(d => d.Staff) // Ensure the related Staff entity is loaded
                        .Where(d => d.Staff != null && d.Staff.DepartmentId == departmentId)
                        .ToListAsync();

                    return doctors; // Return the list of doctors
                }

                // Return an empty collection if the context is null
                return Enumerable.Empty<Doctor>();
            }
            catch (Exception ex)
            {
                // Log the error (adjust this to use a proper logging framework if available)
                Console.WriteLine($"An error occurred while fetching doctors: {ex.Message}");
                // Return an empty collection on error
                return Enumerable.Empty<Doctor>();
            }
        }



        public async Task<IEnumerable<DoctorAvailability>> GetDoctorAvailabilityByDoctorId(int doctorId)
        {
            try
            {
                return await _context.DoctorAvailabilities
                .Include(da => da.Doctor) // Include Doctor details
                .Where(da => da.DoctorId == doctorId)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                // You might want to log the exception here for debugging
                Console.WriteLine(ex.Message); // Example: log the exception message
                return null; // or handle error more appropriately based on your requirements
            }
        }


        //public async Task<ActionResult<DailyAppointmentAvailability>> AddAppointment(int doctorId, DailyAppointmentAvailability newAppointment)
        //{
        //    try
        //    {
        //        // Get the doctor's availability
        //        var doctorAvailability = await _context.Set<DoctorAvailability>()
        //            .FirstOrDefaultAsync(da => da.DoctorId == doctorId);

        //        if (doctorAvailability == null)
        //        {
        //            return NotFound($"Doctor with ID {doctorId} not found.");
        //        }

        //        // Calculate the token: Total existing appointments for the doctor + 1
        //        int totalAppointments = await _context.Set<DailyAppointmentAvailability>()
        //            .CountAsync(daa => daa.DocAvl != null && daa.DocAvl.DoctorId == doctorId);
        //        newAppointment.Token = totalAppointments + 1;

        //        // Associate the new appointment with the doctor's availability
        //        newAppointment.DocAvlId = doctorAvailability.DocAvlId;

        //        // Add the new appointment
        //        _context.Set<DailyAppointmentAvailability>().Add(newAppointment);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction(nameof(GetTodayAppointsByDoctorId), new { doctorId }, newAppointment);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        public async Task<int> GetTotalAppointmentsByDoctorId(int doctorId)
        {
            // Assuming you have an "Appointments" table with DoctorId as a foreign key
            var totalAppointments = await _context.NewAppointments
                                                   .Where(a => a.DoctorId == doctorId)
                                                   .CountAsync();
            return totalAppointments;
        }

    }
}


