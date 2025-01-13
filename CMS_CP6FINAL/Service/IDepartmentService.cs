using CMS_CP6FINAL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_CP6FINAL.Service
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();
    }
}
