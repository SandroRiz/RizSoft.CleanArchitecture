using Microsoft.AspNetCore.Components;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.BlazorServer.Components
{
    public partial class EmployeeCard
    {
        // the right path is this; only for test purpose what below
        //[Parameter]
        //public Employee Employee { get; set; }

        [Parameter]
        public int EmployeeId { get; set; }
        
        protected Employee Employee { get; set; } = new Employee();

        [Inject] IEmployeeService EmployeeService { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            Employee = await EmployeeService.GetEmployeeCardAsync(EmployeeId);
        }
    }
}