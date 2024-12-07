using AllResultsPattern.Services;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IArdalisResult,ArdalisResult>();
builder.Services.AddScoped<IErrorResult, ErrorOrResult>();
builder.Services.AddScoped<IFluentResultWay, FluentResultWay>();
builder.Services.AddScoped<ICSharpFunctionalResult, CSharpFunctionalResult>();
builder.Services.AddScoped<ICustomResult, CustomResultWay>();

// for different layer , we can use from assebmlies containing
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
