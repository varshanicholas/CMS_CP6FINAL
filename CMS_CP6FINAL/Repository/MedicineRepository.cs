using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly CmsCamp6finalContext _context;

        public MedicineRepository(CmsCamp6finalContext context)
        {
            _context = context;
        }

        // Fetch all medicines
        public async Task<IEnumerable<Medicine>> GetMedicines()
        {
            return await _context.Medicines.ToListAsync();
        }

        // Fetch medicine by ID
        public async Task<Medicine> GetMedicineById(int id)
        {
            return await _context.Medicines.FindAsync(id);
        }

        // Search medicine by name
        public async Task<IEnumerable<Medicine>> GetMedicineByName(string name)
        {
            return await _context.Medicines
                .Where(m => m.MedicineName.Contains(name))
                .ToListAsync();
        }

        // Add a new medicine
        public async Task<bool> AddMedicine(Medicine medicine)
        {
            if (medicine == null) return false;

            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update an existing medicine
        public async Task<Medicine> UpdateMedicine(int id, Medicine medicine)
        {
            var existingMedicine = await _context.Medicines.FindAsync(id);
            if (existingMedicine == null) return null;

            existingMedicine.MedicineName = medicine.MedicineName;
            existingMedicine.MedicineCategoryId = medicine.MedicineCategoryId;
            // Update other fields as necessary

            await _context.SaveChangesAsync();
            return existingMedicine;
        }

        // Delete a medicine
        public JsonResult DeleteMedicine(int id)
        {
            try
            {
                // Check if medicine ID is valid
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid medicine ID."
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                // Ensure the database context is initialized
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized."
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                // Find the medicine by ID
                var existingMedicine = _context.Medicines.Find(id);

                if (existingMedicine == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Medicine not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                // Mark the medicine as inactive instead of deleting
                existingMedicine.IsActive = false;

                // Save changes to the database
                _context.SaveChangesAsync();

                return new JsonResult(new
                {
                    success = true,
                    message = "Medicine marked as inactive successfully."
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                // Log the error if needed
                Console.WriteLine($"Error: {ex.Message}");

                return new JsonResult(new
                {
                    success = false,
                    message = "An error occurred while updating the medicine status."
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }



    }
}
