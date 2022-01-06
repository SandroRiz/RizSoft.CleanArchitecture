using Microsoft.EntityFrameworkCore;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.Repository.SQLite.EfCore;


public class EmployeeRepository : BaseRepository<Employee, int>, IEmployeeRepository
{
    public EmployeeRepository(DataContext context) : base(context)
    {
    }

    public async Task<List<Employee>> ListByCountryAsync(string country)
    {
        return await Context.Employees.Where(e => e.Country == country).ToListAsync();
    }
}
