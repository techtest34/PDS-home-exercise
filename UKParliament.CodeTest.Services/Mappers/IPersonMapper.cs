using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services.Mappers
{
    public interface IPersonMapper
    {
        IEnumerable<PersonResponse> MapToResponse(IEnumerable<Person> people);
        PersonResponse MapToResponse(Person person);
        Person MapToPerson(PersonRequest request);
    }
}