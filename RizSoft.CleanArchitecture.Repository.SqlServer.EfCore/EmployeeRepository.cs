using Microsoft.EntityFrameworkCore;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.Repository.SqlServer.EfCore;


public class EmployeeRepository : BaseRepository<Employee, int>, IEmployeeRepository
{
    public EmployeeRepository(NorthwindDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Employee>> ListByCountryAsync(string country)
    {
        return await Context.Employees.Where(e => e.Country == country).ToListAsync();
    }
}
