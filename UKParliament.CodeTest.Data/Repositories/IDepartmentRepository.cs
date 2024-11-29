using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
    }
}