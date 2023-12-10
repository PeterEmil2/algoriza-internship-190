using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using RepositoryLayer.RepositoryFolder;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<appcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring"));
});
builder.Services.AddIdentity<applicationUser, IdentityRole>()
    .AddEntityFrameworkStores<appcontext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAdminRepository, RepositoryLayer.RepositoryFolder.AdminRepository>();
builder.Services.AddScoped<IDoctorRepository, RepositoryLayer.RepositoryFolder.DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, RepositoryLayer.RepositoryFolder.PatientRepository>();

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
