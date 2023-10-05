using Authentication.Data;
using Authentication.Models;
using Authentication.Service;
using Authentication.Service.IService;
using Authentication.Utility;
using HmsMessageBus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding Db Connection
builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")); });

// Register identityFramework
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Service Registration
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IJWTokenGenerator, JwtService>();
builder.Services.AddScoped<IMessageBus, MessageBus>();

//Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//configuring JwtOptions
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
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
