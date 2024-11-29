using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.RequestResponseModels;
using UKParliament.CodeTest.Web.Controllers;
using Xunit;

namespace UKParliament.CodeTest.Tests.Controllers
{
    public class DepartmentsControllerTests
    {
        private readonly DepartmentsController _controller = null!;
        private readonly Mock<IDepartmentService> _mockService = null!;

        public DepartmentsControllerTests()
        {
            _mockService = new Mock<IDepartmentService>();

            _controller = new DepartmentsController(_mockService.Object);
        }

        [Fact]
        public async Task GetDepartments_ReturnsAListOfDepartments()
        {
            List<Department> departments =
            [
                new() 
                {
                    Id = 1,
                    Name = "Sales"
                }
            ];

            List<DepartmentResponse> responses =
            [
                new()
                {
                    Id = 1,
                    Name = "Sales"
                }
            ];

            _mockService.Setup(service => service.GetDepartments()).ReturnsAsync(responses);

            var response = await _controller.GetDepartments();

            var okResult = response.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(responses);
        }
    }
}
