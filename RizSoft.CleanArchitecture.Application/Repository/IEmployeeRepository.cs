using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.Application;

public interface IEmployeeRepository : IBaseRepository<Employee, int>
{
   Task<List<Employee>> ListByCountryAsync(string country);

   

}

