using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Application.Mappers;
using NetMovilAPI.Application.Presenters;
using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Application.UseCases.CategoryUseCases;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Endpoints;
using NetMovilAPI.Infraestructure.DataAccess;
using NetMovilAPI.Infraestructure.DataAccess.Repositories.CategoryRepositories;
using NetMovilAPI.Infraestructure.Models.Shared;
using NetMovilAPI.Infraestructure.Models.UserModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de entidades de IdentityDbContext
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();

// Add category services to the container.
builder.Services.AddScoped<IQueryRepository<Category, CategoryEntity>, CategoryQueryRepository>();
builder.Services.AddScoped<IActionRepository<CategoryEntity>, CategoryActionRepository>();
builder.Services.AddScoped<IPresenter<CategoryEntity, CategoryViewModel>, CategoryPresenter>();
builder.Services.AddScoped<IMapper<CategoryRequestDTO, CategoryEntity>, CategoryMapper>();
builder.Services.AddScoped<GetCategoryUseCase<Category, CategoryEntity, CategoryViewModel>>();
builder.Services.AddScoped<GetCategoryByIdUseCase<Category, CategoryEntity, CategoryViewModel>>();
builder.Services.AddScoped<PostCategoryUseCase<CategoryRequestDTO, CategoryEntity, CategoryViewModel>>();
builder.Services.AddScoped<UpdateCategoryUseCase<CategoryRequestDTO, CategoryEntity, CategoryViewModel>>();
builder.Services.AddScoped<DeleteCategoryUseCase<CategoryEntity, CategoryViewModel>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

app.MapCategoryEndpoints();


app.Run();
