using Ardalis.Result;
using UKParliament.CodeTest.Data.Repositories;
using UKParliament.CodeTest.Services.Mappers;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;
        private IDepartmentMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IDepartmentMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DepartmentResponse>>> GetDepartments()
        {
            var departments = await _departmentRepository.GetDepartments();

            return Result.Success(_mapper.MapToResponse(departments));
        }
    }
}
