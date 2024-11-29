using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private IPersonManagerContext _context = null!;

        public PersonRepository(IPersonManagerContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetPerson(int id)
        {
            return await _context.People.AsNoTracking().FirstOrDefaultAsync(person => person.Id == id);
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await _context.People.AsNoTracking().ToListAsync();
        }

        public async Task<Person> CreatePerson(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();

            return person;
        }
    }
}
