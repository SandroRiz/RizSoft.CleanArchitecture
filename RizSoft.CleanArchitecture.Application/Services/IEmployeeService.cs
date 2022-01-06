
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.Application;

public interface IEmployeeService : IBaseService<Employee, int>
{

    Task<Employee> GetEmployeeCardAsync(int employeeId);

    Task<List<Employee>> ListByCountryAsync(string country);

}

