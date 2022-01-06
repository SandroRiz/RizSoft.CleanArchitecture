using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;

namespace RizSoft.CleanArchitecture.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<IndexModel> _logger;


        public List<Employee> Employees { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IEmployeeService EmployeeService)
        {
            _logger = logger;
            _employeeService = EmployeeService;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            Employees = await _employeeService.ListByCountryAsync("UK");
            return Page();
        }
    }
}