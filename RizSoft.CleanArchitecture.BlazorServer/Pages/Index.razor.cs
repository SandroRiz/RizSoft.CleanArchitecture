using Microsoft.AspNetCore.Components;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.BlazorServer.Pages;

public partial class Index
{
    [Inject] IEmployeeService EmployeeService { get; set; }
    public List<Employee>? Employees { get; set; }
    public string Country { get; set; } = "UK";
    protected override async Task OnInitializedAsync()
    {
        Employees = await EmployeeService.ListAsync();

        await EmployeeService.GetEmployeeCardAsync(1);
    }

    protected async Task FilterEmployees()
    {
        if (String.IsNullOrEmpty(Country))
            Employees = await EmployeeService.ListAsync();
        else
            Employees = await EmployeeService.ListByCountryAsync(Country);
    }
}
