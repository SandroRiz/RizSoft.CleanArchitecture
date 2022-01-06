using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RizSoft.CleanArchitecture.Repository.SqlServer.EfCore;
using RizSoft.CleanArchitecture.Services;

// Non dependency Injection, directly use of Implementations

var builder = new ConfigurationBuilder()
           .AddJsonFile($"appsettings.json", true, true);

var config = builder.Build();
var connectionString = config["ConnectionStrings:Db"];

var options = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<DataContext>(), connectionString).Options;

EmployeeRepository repository = new(new DataContext(options));
EmployeeService svc = new EmployeeService(repository);

var employees = await svc.ListByCountryAsync("UK");
foreach (var employee in employees)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName}");
}

Console.ReadLine();
