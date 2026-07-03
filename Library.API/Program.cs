using FluentValidation;
using FluentValidation.AspNetCore;
using Library.API.Extensions;
using Library.DAL.Repositories.Implementations;
using Library.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSwaggerServices();

builder.Services.AddValidationResponses();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(); // لتوليد ملف الـ JSON الخاص بالـ API
    app.UseSwaggerUI(); // لتفعيل الواجهة الرسومية التي تراها في المتصفح
}


app.UseHttpsRedirection();

app.UseApplicationMiddlewares();

app.UseAuthorization();

app.MapControllers();


app.Run();

