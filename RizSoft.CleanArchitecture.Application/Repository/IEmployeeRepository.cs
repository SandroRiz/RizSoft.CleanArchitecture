using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.Application;

public interface IEmployeeRepository : IBaseRepository<Employee, int>
{
   Task<IEnumerable<Employee>> ListByCountryAsync(string country);

   

}

