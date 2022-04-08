using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Example.Data;
using Example.Models;
using Microsoft.Extensions.Configuration;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
//using Serilog.Sinks.console;
using Serilog.Sinks.File;

var builder = WebApplication.CreateBuilder(args);
builder.AddJsonFile("appsettings.json");
//builder.UseSerilog();


//dependency injection
// Add services to the container
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .CreateLogger();
Log.Information("this is log");

//builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//builder.Logging.AddConsole();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>();
var context=new ApplicationDbContext();
context.Database.EnsureCreated();
var app = builder.Build();

// Configure the HTTP request pipeline. if in developer environmet we need to use app.UseDeveloerExceptionPage()
          
 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //app.UseDeveloerExceptionPage()
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//middlweare methods
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Routing generrate the links//conventionalbasedrouting
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
