using System.Text;
using System.Text.Json.Serialization;
using Application.Extensions;
using Domain.Utils;
using Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(p =>
		p.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(p =>
	{
		p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	}
).AddJwtBearer(p =>
{
	p.RequireHttpsMetadata = false;
	p.SaveToken = true;
	p.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.ASCII.GetBytes(Secret.Key)),
		ValidateIssuer = false,
		ValidateAudience = false
	};
});

var configuration = builder.Configuration;

var host = configuration["DBHOST"] ?? "localhost";
var port = configuration["DBPORT"] ?? "3306";
var password = configuration["DBPASSWORD"] ?? "gq7cyo4e";

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseMySql($"server={host};port={port};username=docker_user;password={password};database=cp_data",
		new MariaDbServerVersion(new Version(10, 8, 3))));

builder.Services.AddRepositories();
builder.Services.AddServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
