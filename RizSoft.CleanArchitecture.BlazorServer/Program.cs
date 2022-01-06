using Microsoft.EntityFrameworkCore;
using RizSoft.CleanArchitecture.Application;
using RizSoft.CleanArchitecture.Repository.SqlServer.EfCore;
using RizSoft.CleanArchitecture.Repository.SqlServer.EfCore.Factory;
using RizSoft.CleanArchitecture.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContextFactory<NorthwindDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
