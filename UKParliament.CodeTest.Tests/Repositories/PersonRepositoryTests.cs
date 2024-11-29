using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Data.Repositories;
using Xunit;

namespace UKParliament.CodeTest.Tests.Respoitories
{
    public class PersonRepositoryTests
    {
        private readonly Mock<IPersonManagerContext> _mockContext;
        private readonly PersonRepository _personRepository;

        public PersonRepositoryTests()
        {
            _mockContext = new Mock<IPersonManagerContext>();

            _personRepository = new PersonRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetPerson_WhenPersonExists_ReturnsPerson()
        {
            var people = new List<Person>
            {
                new() { Id = 1, FirstName = "John", LastName = "Doe" }
            }
            .AsQueryable().BuildMockDbSet();

            _mockContext.Setup(context => context.People).Returns(people.Object);

            var result = await _personRepository.GetPerson(1);

            result.Should().BeEquivalentTo(people.Object.First());
        }

        [Fact]
        public async Task GetPerson_WhenPersonDoesNotExist_ReturnsNull()
        {
            var people = new List<Person>().AsQueryable().BuildMockDbSet();

            _mockContext.Setup(context => context.People).Returns(people.Object);

            var result = await _personRepository.GetPerson(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetPeople_ReturnsAListOfPeople()
        {
            var people = new List<Person>
            {
                new() { FirstName = "John", LastName = "Doe" },
                new() { FirstName = "Jane", LastName = "Bloggs" }
            }
            .AsQueryable().BuildMockDbSet();

            _mockContext.Setup(context => context.People).Returns(people.Object);

            var result = await _personRepository.GetPeople();

            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(people.Object);
        }

        [Fact]
        public async Task AddPerson_CallsAddOnContext()
        {
            var person = new Person { FirstName = "John", LastName = "Doe" };
            var people = new List<Person>().AsQueryable().BuildMockDbSet();

            _mockContext.Setup(context => context.People).Returns(people.Object);

            await _personRepository.CreatePerson(person);

            _mockContext.Verify(context => context.People.AddAsync(person, It.IsAny<CancellationToken>()));
            _mockContext.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task UpdatePerson_CallsUpdateOnContext()
        {
            var person = new Person { FirstName = "John", LastName = "Doe" };
            var people = new List<Person>().AsQueryable().BuildMockDbSet();

            _mockContext.Setup(context => context.People).Returns(people.Object);

            await _personRepository.UpdatePerson(person);

            _mockContext.Verify(context => context.People.Update(person));
            _mockContext.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
