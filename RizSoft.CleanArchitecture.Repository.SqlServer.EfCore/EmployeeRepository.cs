using Microsoft.EntityFrameworkCore;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.Repository.SqlServer.EfCore;

// derive from BaseRepository for Blazor Server Side to avoid concurrency issues;
public class EmployeeRepository : BaseRepository<Employee, int>, IEmployeeRepository
{
    public EmployeeRepository(IDbContextFactory<DataContext> factory) : base(factory)
    {
    }

    public async Task<List<Employee>> ListByCountryAsync(string country)
    {
        using var ctx = CtxFactory.CreateDbContext();
        return await ctx.Employees.Where(e => e.Country == country).ToListAsync();

    }

   
}
