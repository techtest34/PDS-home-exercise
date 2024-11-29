using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Data.Repositories;
using Xunit;

namespace UKParliament.CodeTest.Tests.Respoitories
{
    public class DepartmentRepositoryTests
    {
        private readonly Mock<IPersonManagerContext> _mockContext;
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentRepositoryTests()
        {
            _mockContext = new Mock<IPersonManagerContext>();

            _departmentRepository = new DepartmentRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetDepartments_ReturnsAListOfDepartments()
        {
            var departments = new List<Department>
            {
                new() { Name = "Sales" },
                new() { Name = "Marketing" }
            }
            .AsQueryable().BuildMockDbSet();

            _mockContext.Setup(context => context.Departments).Returns(departments.Object);

            var result = await _departmentRepository.GetDepartments();

            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(departments.Object);
        }
    }
}
