using Ardalis.Result;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Data.Repositories;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.Mappers;
using UKParliament.CodeTest.Services.RequestResponseModels;
using Xunit;

namespace UKParliament.CodeTest.Tests.Services
{
    public class PersonServiceTests
    {
        private readonly Mock<IPersonMapper> _mockMapper = null!;
        private readonly Mock<IValidator<PersonRequest>> _mockValidator = null!;
        private readonly Mock<IPersonRepository> _mockRepository;

        private readonly PersonService _personService;

        public PersonServiceTests()
        {
            _mockRepository = new Mock<IPersonRepository>();
            _mockMapper = new Mock<IPersonMapper>();
            _mockValidator = new Mock<IValidator<PersonRequest>>();

            _personService = new PersonService(_mockRepository.Object, _mockMapper.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task GetPerson_WhenPersonDoesNotExist_ReturnsNotFoundResult()
        {
            _mockRepository.Setup(repository => repository.GetPerson(1)).ReturnsAsync(null as Person);

            var result = await _personService.GetPerson(1);

            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Fact]
        public async Task GetPerson_WhenPersonExists_ReturnsPersonResponse()
        {
            const int PersonId = 1;
            var person = new Person
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

            _mockRepository.Setup(repository => repository.GetPerson(PersonId)).ReturnsAsync(person);
            _mockMapper.Setup(mapper => mapper.MapToResponse(person)).Returns(response);

            var result = await _personService.GetPerson(PersonId);

            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().Be(response);
            _mockRepository.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public async Task GetPeople_ReturnsAListOfPersonResponse()
        {
            const int PersonId = 1;
            List<Person> people = [
                new()
                {
                    Id = PersonId,
                    FirstName = "John",
                    LastName = "Doe",
                }
            ];

            List<PersonResponse> responses = [
                new()
                {
                    Id = PersonId,
                    FirstName = "John",
                    LastName = "Doe",
                }
            ];

            _mockRepository.Setup(repository => repository.GetPeople()).ReturnsAsync(people);
            _mockMapper.Setup(mapper => mapper.MapToResponse(people)).Returns(responses);

            var result = await _personService.GetPeople();

            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().BeEquivalentTo(responses);
            _mockRepository.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public async Task CreatePerson_WithAnInvalidRequest_ReturnsAnInvalidResult()
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

            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Property", "Property is invalid"));
            _mockValidator.Setup(validator => validator.ValidateAsync(request, CancellationToken.None)).ReturnsAsync(validationResult).Verifiable(Times.Once);

            var result = await _personService.CreatePerson(request);

            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().BeEquivalentTo(validationErrors);
            _mockValidator.Verify();
        }

        [Fact]
        public async Task CreatePerson_WithAValidRequest_ReturnsAPersonResponse()
        {
            var request = new PersonRequest
            {
                FirstName = "John",
                LastName = "Doe",
            };
            var person = new Person
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

            _mockValidator.Setup(validator => validator.ValidateAsync(request, CancellationToken.None)).ReturnsAsync(new FluentValidation.Results.ValidationResult()).Verifiable(Times.Once);
            _mockMapper.Setup(mapper => mapper.MapToPerson(request)).Returns(person).Verifiable(Times.Once);
            _mockMapper.Setup(mapper => mapper.MapToResponse(person)).Returns(response).Verifiable(Times.Once);
            _mockRepository.Setup(repository => repository.CreatePerson(person)).ReturnsAsync(person);

            var result = await _personService.CreatePerson(request);

            result.Status.Should().Be(ResultStatus.Created);
            result.Value.Should().Be(response);
            _mockValidator.Verify();
            _mockMapper.Verify();
            _mockRepository.Verify();
        }

        [Fact]
        public async Task UpdatePerson_WhenPersonDoesNotExist_ReturnsNotFound()
        {
            const int PersonId = 1;
            var request = new PersonRequest
            {
                Id = PersonId,
                FirstName = "John",
                LastName = "Doe",
            };

            _mockRepository.Setup(repository => repository.GetPerson(PersonId)).ReturnsAsync(null as Person).Verifiable(Times.Once);

            var result = await _personService.UpdatePerson(PersonId, request);

            result.Status.Should().Be(ResultStatus.NotFound);
            _mockRepository.Verify();
        }

        [Fact]
        public async Task UpdatePerson_WhenPersonIdDoesNotMatchTheRequest_ReturnsInvalid()
        {
            var request = new PersonRequest
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
            };

            var result = await _personService.UpdatePerson(2, request);

            result.Status.Should().Be(ResultStatus.Invalid);
            _mockRepository.Verify();
        }

        [Fact]
        public async Task UpdatePerson_WithAnInvalidRequest_ReturnsAnInvalidResult()
        {
            const int PersonId = 1;
            var request = new PersonRequest
            {
                Id = PersonId,
                FirstName = "John",
                LastName = "Doe",
            };
            var person = new Person
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

            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Property", "Property is invalid"));
            _mockValidator.Setup(validator => validator.ValidateAsync(request, CancellationToken.None)).ReturnsAsync(validationResult).Verifiable(Times.Once);
            _mockRepository.Setup(repository => repository.GetPerson(PersonId)).ReturnsAsync(person);

            var result = await _personService.UpdatePerson(PersonId, request);

            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().BeEquivalentTo(validationErrors);
            _mockValidator.Verify();
        }

        [Fact]
        public async Task UpdatePerson_WithAValidRequest_ReturnsAPersonResponse()
        {
            const int PersonId = 1;
            var request = new PersonRequest
            {
                Id = PersonId,
                FirstName = "John",
                LastName = "Doe",
            };
            var person = new Person
            {
                Id = PersonId,
                FirstName = "Joseph",
                LastName = "Doe",
            };
            var response = new PersonResponse
            {
                Id = PersonId,
                FirstName = "Joseph",
                LastName = "Doe",
            };

            _mockValidator.Setup(validator => validator.ValidateAsync(request, CancellationToken.None)).ReturnsAsync(new FluentValidation.Results.ValidationResult()).Verifiable(Times.Once);
            _mockMapper.Setup(mapper => mapper.MapToPerson(request)).Returns(person).Verifiable(Times.Once);
            _mockMapper.Setup(mapper => mapper.MapToResponse(person)).Returns(response).Verifiable(Times.Once);
            _mockRepository.Setup(repository => repository.UpdatePerson(person)).ReturnsAsync(person).Verifiable(Times.Once);
            _mockRepository.Setup(repository => repository.GetPerson(PersonId)).ReturnsAsync(person).Verifiable(Times.Once);

            var result = await _personService.UpdatePerson(PersonId, request);

            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().Be(response);
            _mockValidator.Verify();
            _mockMapper.Verify();
            _mockRepository.Verify();
        }
    }
}
