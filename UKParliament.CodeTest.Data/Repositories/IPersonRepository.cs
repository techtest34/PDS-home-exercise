using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetPerson(int id);
        Task<IEnumerable<Person>> GetPeople();
        Task<Person> CreatePerson(Person person);
        Task<Person> UpdatePerson(Person person);
    }
}