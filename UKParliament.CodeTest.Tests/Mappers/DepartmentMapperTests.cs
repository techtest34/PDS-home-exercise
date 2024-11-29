using FluentAssertions;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services.Mappers;
using UKParliament.CodeTest.Services.RequestResponseModels;
using Xunit;

namespace UKParliament.CodeTest.Tests.Mappers
{
    public class DepartmentMapperTests
    {
        private DepartmentMapper _mapper;

        public DepartmentMapperTests()
        {
            _mapper = new DepartmentMapper();
        }

        [Fact]
        public void MapToResponse_WithItem_MapsToResponse()
        {
            var department = new Department
            {
                Id = 1,
                Name = "Sales"
            };
            var response = new DepartmentResponse
            {
                Id = 1,
                Name = "Sales"
            };

            var result = _mapper.MapToResponse(department);

            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void MapToResponse_WithList_MapsToResponse()
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

            var result = _mapper.MapToResponse(departments);

            result.Should().BeEquivalentTo(responses);
        }
    }
}
