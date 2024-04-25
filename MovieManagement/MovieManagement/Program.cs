using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<MovieManagementStorageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieManagementDatabaseConnection")));
builder.Services.AddMediatR(typeof(ResponseBase<>));

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
