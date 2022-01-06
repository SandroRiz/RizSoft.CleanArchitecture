using Microsoft.EntityFrameworkCore;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Domain.Models;
using RizSoft.CleanArchitecture.Repository.SqlServer.EfCore;
using RizSoft.CleanArchitecture.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin();
                      });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<NorthwindDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/employees/", async (IEmployeeService svc) =>
{
    var employees = await svc.ListAsync();
    if (employees != null && employees.Any())
    {
        return Results.Ok(employees);
    }
    else
        return Results.NotFound();

}).Produces<List<Employee>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);


app.MapGet("/employees/{country}", async (string country, IEmployeeService svc) =>
{
    var employees = await svc.ListByCountryAsync(country);
    if (employees != null && employees.Any())
    {
        return Results.Ok(employees);
    }
    else
        return Results.NotFound();

}).Produces<List<Employee>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.UseCors(MyAllowSpecificOrigins);

app.Run();

