using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services.Mappers
{
    public interface IDepartmentMapper
    {
        DepartmentResponse MapToResponse(Department department);
        IEnumerable<DepartmentResponse> MapToResponse(IEnumerable<Department> departments);
    }
}