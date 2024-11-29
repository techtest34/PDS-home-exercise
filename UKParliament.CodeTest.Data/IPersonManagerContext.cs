using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data
{
    public interface IPersonManagerContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Person> People { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}