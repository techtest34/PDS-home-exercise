using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services.Mappers
{
    public class PersonMapper : IPersonMapper
    {
        public IEnumerable<PersonResponse> MapToResponse(IEnumerable<Person> people)
        {
            return people.Select(MapToResponse);
        }

        public PersonResponse MapToResponse(Person person)
        {
            return new PersonResponse
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                DepartmentId = person.DepartmentId
            };
        }

        public Person MapToPerson(PersonRequest request)
        {
            return new Person
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                DepartmentId = request.DepartmentId
            };
        }
    }
}
