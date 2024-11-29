using Ardalis.Result;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services
{
    public interface IDepartmentService
    {
        Task<Result<IEnumerable<DepartmentResponse>>> GetDepartments();
    }
}