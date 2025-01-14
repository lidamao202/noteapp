using System;
using FluentMigrator.Runner;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using noteapi;
using noteapi.Migrations;
using noteapi.Repository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
             builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
});


builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<Database>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
      .AddFluentMigratorCore()
      .ConfigureRunner(c => c.AddSqlServer2012()
          .WithGlobalConnectionString(builder.Configuration["ConnectionStrings:AppDbContext"])
          .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.Run();

