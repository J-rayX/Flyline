using Microsoft.OpenApi.Models;
using Flyline.Data;
using Flyline.Domain.Entities;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add db context
builder.Services.AddDbContext<Entities>(options =>
options.UseInMemoryDatabase(databaseName: "Flights"),
ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen( c =>
{
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7261"
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]+ e.ActionDescriptor.RouteValues["controller"]}");
});

// Add a singleton of type Entities to the dependency injection ecosystem
builder.Services.AddSingleton<Entities>();

var app = builder.Build();


// SEEDING DATABASE
var entities = app.Services.CreateScope().ServiceProvider.GetRequiredService<Entities>();
var random = new Random();

Flight[] flightsToSeed = new Flight[]
{
    new (   Guid.NewGuid(),
            "Overland",
            random.Next(90, 5000).ToString(),
                new TimePlace("Lagos",DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Benin",DateTime.Now.AddHours(random.Next(4, 10))),
                    2),
        new (   Guid.NewGuid(),
            "Arik Airways",
            random.Next(90, 5000).ToString(),
                new TimePlace("Kaduna",DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlace("Ibadan",DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
        new (   Guid.NewGuid(),
            "Green Africa",
                random.Next(90, 5000).ToString(),
                new TimePlace("Ilorin",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Port Harcourt",DateTime.Now.AddHours(random.Next(4, 18))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
            "Aero",
                random.Next(90, 5000).ToString(),
                new TimePlace("Amsterdam",DateTime.Now.AddHours(random.Next(1, 21))),
                new TimePlace("Glasgow, Scotland",DateTime.Now.AddHours(random.Next(4, 21))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
            "Value Jet",
                random.Next(90, 5000).ToString(),
                new TimePlace("Abuja",DateTime.Now.AddHours(random.Next(1, 23))),
                new TimePlace("London, England",DateTime.Now.AddHours(random.Next(4, 25))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
            "Azman",
                random.Next(90, 5000).ToString(),
                new TimePlace("Zurich",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Warsaw",DateTime.Now.AddHours(random.Next(4, 19))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
            "Dana Air",
                random.Next(90, 5000).ToString(),
                new TimePlace("Praha Ruzyne",DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlace("Paris",DateTime.Now.AddHours(random.Next(4, 58))),
                    random.Next(1, 853))
};
entities.Flights.AddRange(flightsToSeed);

entities.SaveChanges();


app.UseCors(builder => builder
    .WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
