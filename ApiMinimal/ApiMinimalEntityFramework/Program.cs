using ApiMinimalEntityFramework.Data;
using ApiMinimalEntityFramework.GenresEndpoints;
using ApiMinimalEntityFramework.Repositories;
using ApiMinimalEntityFramework.Repositories.Imp;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// Add DBContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add CQRS
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
// Add Endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add repositories
builder.Services.AddScoped<IRepositoryGenre, RepositoryGenre>();
// add automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// add cache
builder.Services.AddOutputCache();
// ---------------------------------------------------------

// Configure the HTTP request pipeline
var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();
app.UseSwaggerUI();

// Enable middleware to use Cors
app.UseCors();
// Enable middleware to use cache
app.UseOutputCache();

// pipeline endpoints
app.MapGroup("/genres").MapGenres();

app.Run();



