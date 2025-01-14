using System;
using FluentMigrator.Runner;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using noteapi;
using noteapi.Migrations;
using noteapi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddSwaggerGen(options => {

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                }
            },
            new List < string > ()
        }
        });
});


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.Run();

