using Ardalis.Result;
using FluentAssertions;
using Moq;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Data.Repositories;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.Mappers;
using UKParliament.CodeTest.Services.RequestResponseModels;
using Xunit;

namespace UKParliament.CodeTest.Tests.Services
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IDepartmentRepository> _mockRepository;
        private readonly DepartmentService _departmentService;
        private readonly Mock<IDepartmentMapper> _mockMapper = null!;

        public DepartmentServiceTests()
        {
            _mockRepository = new Mock<IDepartmentRepository>();
            _mockMapper = new Mock<IDepartmentMapper>();

            _departmentService = new DepartmentService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetDepartments_ReturnsAListOfDepartments()
        {
            var departments = new List<Department>
            {
                new() { Name = "Sales" },
                new() { Name = "Marketing" }
            };

            var responses = new List<DepartmentResponse>
            {
                new() { Name = "Sales" },
                new() { Name = "Marketing" }
            };

            _mockRepository.Setup(repository => repository.GetDepartments()).ReturnsAsync(departments);
            _mockMapper.Setup(mapper => mapper.MapToResponse(departments)).Returns(responses);

            var result = await _departmentService.GetDepartments();

            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().HaveCount(2);
            result.Value.Should().BeEquivalentTo(departments);
        }
    }
}
