using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Validators;
using MovieManagement.ApplicationServices.Components.OpenWeather;
using MovieManagement.ApplicationServices.Components.PassworHasher;
using MovieManagement.ApplicationServices.Mappings;
using MovieManagement.Authentication;
using MovieManagement.DataAccess;
using MovieManagement.DataAccess.CQRS;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MovieManagementStorageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieManagementDatabaseConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddMediatR(typeof(ResponseBase<>));
builder.Services.AddAutoMapper(typeof(MoviesProfile).Assembly);
builder.Services.AddTransient<IQueryExecutor, QueryExecutor>();
builder.Services.AddTransient<ICommandExecutor, CommandExecutor>();
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<AddMovieRequestValidator>();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Logging.ClearProviders().SetMinimumLevel(LogLevel.Trace);
builder.WebHost.UseNLog();
builder.Services.AddTransient<IOpenWeatherConnector, OpenWeatherConnector>();
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
