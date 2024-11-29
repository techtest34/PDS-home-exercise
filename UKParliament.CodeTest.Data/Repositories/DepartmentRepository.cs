using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private IPersonManagerContext _context = null!;

        public DepartmentRepository(IPersonManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }
    }
}
