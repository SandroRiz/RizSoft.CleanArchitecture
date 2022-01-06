using Microsoft.AspNetCore.Components;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.BlazorServer.Pages;

public partial class Index
{
    [Inject] IEmployeeService employeeService { get; set; }
    public List<Employee> Employees { get; set; }
    public string Country { get; set; } = "UK";
    protected override async Task OnInitializedAsync()
    {
        Employees = await employeeService.ListAsync();
    }

    protected async Task FilterEmployees()
    {
        if (String.IsNullOrEmpty(Country))
            Employees = await employeeService.ListAsync();
        else
            Employees = await employeeService.ListByCountryAsync(Country);
    }
}
