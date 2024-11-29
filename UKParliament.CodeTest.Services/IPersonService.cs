using Ardalis.Result;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services;

public interface IPersonService
{
    Task<Result<PersonResponse>> GetPerson(int id);
    Task<Result<IEnumerable<PersonResponse>>> GetPeople();
    Task<Result<PersonResponse>> CreatePerson(PersonRequest person);
    Task<Result<PersonResponse>> UpdatePerson(int id, PersonRequest request);
}