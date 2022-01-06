using Microsoft.AspNetCore.Components;
using RizSoft.CleanArchitecture.Domain.Models;
using System.Net.Http.Json;

namespace RizSoft.CleanArchitecture.BlazorWASM.Pages;

public partial class Index
{
    [Inject] HttpClient httpClient { get; set; }
    public List<Employee> Employees { get; set; }
    public string Country { get; set; } = "UK";
    protected override async Task OnInitializedAsync()
    {
        Employees = await httpClient.GetFromJsonAsync<List<Employee>>("https://localhost:7202/employees");
    }

    protected async Task FilterEmployees()
    {
        if (String.IsNullOrEmpty(Country))
            Employees = await httpClient.GetFromJsonAsync<List<Employee>>("https://localhost:7202/employees");
        else
            Employees = await httpClient.GetFromJsonAsync<List<Employee>>($"https://localhost:7202/employees/{Country}");
    }
}
