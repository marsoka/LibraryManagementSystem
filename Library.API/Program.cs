using FluentValidation;
using Library.DAL.Repositories.Interfaces;
using Library.DAL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

// 2. إضافة خدمات توليد الـ Swagger OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // هذا السطر يقوم بإعداد مولد السواجر

// الطريقة الحديثة والمدعومة في AutoMapper 16+ لتعريف الـ Profiles
builder.Services.AddAutoMapper(cfg =>
{
    // أخبر الأوتو مابر أن يبحث تلقائياً عن كلاسات الـ Profile داخل مشروعك
    cfg.AddMaps(typeof(AuthorProfile).Assembly);
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();

builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(); // لتوليد ملف الـ JSON الخاص بالـ API
    app.UseSwaggerUI(); // لتفعيل الواجهة الرسومية التي تراها في المتصفح
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/weatherforecast/10", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast10");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
