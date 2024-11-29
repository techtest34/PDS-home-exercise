using FluentAssertions;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services.Mappers;
using UKParliament.CodeTest.Services.RequestResponseModels;
using Xunit;

namespace UKParliament.CodeTest.Tests.Mappers
{
    public class PersonMapperTests
    {
        private PersonMapper _mapper;

        public PersonMapperTests()
        {
            _mapper = new PersonMapper();
        }

        [Fact]
        public void MapToResponse_WithItem_MapsToResponse()
        {
            var person = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(2000, 12, 25),
                DepartmentId = 2,
            };
            var response = new PersonResponse
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(2000, 12, 25),
                DepartmentId = 2,
            };

            var result = _mapper.MapToResponse(person);

            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void MapToResponse_WithList_MapsToResponse()
        {
            List<Person> people =
            [
                new()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateOnly(2000, 12, 25),
                    DepartmentId = 2,
                }
            ];
            List<PersonResponse> responses =
            [
                new()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateOnly(2000, 12, 25),
                    DepartmentId = 2,
                }
            ];

            var result = _mapper.MapToResponse(people);

            result.Should().BeEquivalentTo(responses);
        }

        [Fact]
        public void MapToPerson_WithItem_MapsToPerson()
        {
            var request = new PersonRequest
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(2000, 12, 25),
                DepartmentId = 2,
            };
            var person = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(2000, 12, 25),
                DepartmentId = 2,
            };

            var result = _mapper.MapToPerson(request);

            result.Should().BeEquivalentTo(person);
        }
    }
}
