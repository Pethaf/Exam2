using DAL;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSpaStaticFiles(configuration => {
    configuration.RootPath = "ReactApp/build";
    
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "react",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7208");
                      });
});

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors("react");

app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
        name: "article",
        pattern: "News/{*blob}",
        defaults: new { controller = "News", action = "NewsPage" }
    ));
app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
    name: "default", 
    pattern: "{controller=News}/{action=StartPage}/{id?}"));

app.UseRouting();

app.UseSpaStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ReactApp";
    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("https://localhost:44423");
    }
});

app.Run();
