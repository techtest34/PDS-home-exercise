using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services.Mappers
{
    public class DepartmentMapper : IDepartmentMapper
    {
        public IEnumerable<DepartmentResponse> MapToResponse(IEnumerable<Department> departments)
        {
            return departments.Select(MapToResponse);
        }

        public DepartmentResponse MapToResponse(Department department)
        {
            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name
            };
        }
    }
}
