using CMS_CP6FINAL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CMS_CP6FINAL.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly CmsCamp6finalContext _context;

        public DepartmentService(CmsCamp6finalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}
