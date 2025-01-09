using CMS_CP6FINAL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Repository
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetMedicines();
        Task<Medicine> GetMedicineById(int id);
        Task<IEnumerable<Medicine>> GetMedicineByName(string name);
        Task<bool> AddMedicine(Medicine medicine);

        Task<Medicine> UpdateMedicine(int id, Medicine medicine);
        JsonResult DeleteMedicine(int id);


    }
}
