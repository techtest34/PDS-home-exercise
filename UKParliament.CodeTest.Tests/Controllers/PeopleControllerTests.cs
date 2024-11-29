using Ardalis.Result;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.RequestResponseModels;
using UKParliament.CodeTest.Web.Controllers;
using Xunit;

namespace UKParliament.CodeTest.Tests.Controllers
{
    public class PeopleControllerTests
    {
        private readonly PeopleController _controller = null!;
        private readonly Mock<IPersonService> _mockService = null!;

        public PeopleControllerTests()
        {
            _mockService = new Mock<IPersonService>();

            _controller = new PeopleController(_mockService.Object);
        }

        [Fact]
        public async Task GetPerson_WhenPersonDoesNotExist_ReturnsNotFound()
        {
            const int PersonId = 1;
            _mockService.Setup(service => service.GetPerson(PersonId)).ReturnsAsync(Result.NotFound()).Verifiable(Times.Once);

            var response = await _controller.GetPerson(PersonId);

            response.Result.Should().BeOfType<NotFoundResult>();
            _mockService.Verify();
        }

        [Fact]
        public async Task GetPerson_WhenPersonExists_ReturnsPerson()
        {
            const int PersonId = 1;

            var person = new PersonResponse
            {
                Id = PersonId,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new(2008, 12, 25),
                DepartmentId = 1
            };

            _mockService.Setup(service => service.GetPerson(PersonId)).ReturnsAsync(person).Verifiable(Times.Once);

            var response = await _controller.GetPerson(PersonId);

            var okResult = response.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(person);
            _mockService.Verify();
        }

        [Fact]
        public async Task GetPeople_ReturnsAListOfPeople()
        {
            List<PersonResponse> people =
            [
                new()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new(2008, 12, 25),
                    DepartmentId = 1
                }
            ];

            _mockService.Setup(service => service.GetPeople()).ReturnsAsync(people).Verifiable(Times.Once);

            var response = await _controller.GetPeople();

            var okResult = response.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(people);
            _mockService.Verify();
        }

        [Fact]
        public async Task Create_WhenRequestIsInvalid_ReturnsBadRequest()
        {
            var request = new PersonRequest
            {
                FirstName = "John",
                LastName = "Doe",
            };
            List<ValidationError> validationErrors =
            [
                new()
                {
                    Identifier = "Property",
                    ErrorMessage = "Property is invalid"
                }
            ];

            _mockService.Setup(service => service.CreatePerson(request)).ReturnsAsync(Result.Invalid(validationErrors)).Verifiable(Times.Once);

            var result = await _controller.Create(request);

            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().Be(validationErrors);
            _mockService.Verify();
        }

        [Fact]
        public async Task Create_WhenGivenAValidPerson_ReturnsACreatedResponse()
        {
            var request = new PersonRequest
            {
                FirstName = "John",
                LastName = "Doe",
            };

            var response = new PersonResponse
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
            };

            _mockService.Setup(service => service.CreatePerson(request)).ReturnsAsync(response).Verifiable(Times.Once);

            var result = await _controller.Create(request);

            var created = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            created.ActionName.Should().Be("GetPerson");
            created.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new { id = 1 }));
            var value = created.Value.Should().BeAssignableTo<PersonResponse>().Subject;
            value.Should().Be(response);
            _mockService.Verify();
        }

        [Fact]
        public async Task Update_WhenPersonDoesNotExist_ReturnsNotFound()
        {
            const int PersonId = 1;
            var request = new PersonRequest
            {
                Id = PersonId,
                FirstName = "John",
                LastName = "Doe",
            };

            _mockService.Setup(service => service.UpdatePerson(PersonId, request)).ReturnsAsync(Result.NotFound()).Verifiable(Times.Once);

            var result = await _controller.Update(PersonId, request);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Update_WhenRequestIsInvalid_ReturnsBadRequest()
        {
            const int PersonId = 1;
            var request = new PersonRequest
            {
                Id = PersonId,
                FirstName = "John",
                LastName = "Doe",
            };
            List<ValidationError> validationErrors =
            [
                new()
                {
                    Identifier = "Property",
                    ErrorMessage = "Property is invalid"
                }
            ];

            _mockService.Setup(service => service.UpdatePerson(PersonId, request)).ReturnsAsync(Result.Invalid(validationErrors)).Verifiable(Times.Once);

            var result = await _controller.Update(PersonId, request);

            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().Be(validationErrors);
            _mockService.Verify();
        }

        [Fact]
        public async Task Update_WhenGivenAValidPerson_ReturnsANocontentResponse()
        {
            const int PersonId = 1;
            var request = new PersonRequest
            {
                Id = PersonId,
                FirstName = "John",
                LastName = "Doe",
            };

            var response = new PersonResponse
            {
                Id = PersonId,
                FirstName = "John",
                LastName = "Doe",
            };

            _mockService.Setup(service => service.UpdatePerson(PersonId, request)).ReturnsAsync(response).Verifiable(Times.Once);

            var result = await _controller.Update(PersonId, request);

            var created = result.Should().BeOfType<NoContentResult>().Subject;
            _mockService.Verify();
        }
    }
}
