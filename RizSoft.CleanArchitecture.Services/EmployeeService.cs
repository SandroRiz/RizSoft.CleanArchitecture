using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.Services;

public class EmployeeService : BaseService<Employee, int>, IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository) : base(repository)
    {
        _repository = repository;
    }

    //sample of Process in Service Layer
    public async Task<Employee> GetEmployeeCardAsync(int employeeId)
    {
        var employee = await _repository.GetAsync(employeeId);

        if (employeeId == 2 || employeeId == 5 || employeeId == 6)
            employee.Gender = "M";
        else
            employee.Gender = "F";

        return employee;
    }

    //sample of mapping a repository method
    public async Task<List<Employee>> ListByCountryAsync(string country)
    {
        return await _repository.ListByCountryAsync(country);   
    }

}

