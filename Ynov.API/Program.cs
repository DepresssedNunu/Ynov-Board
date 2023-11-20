using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

using Ynov.Business.IRespositories;
using Ynov.Business.IServices;
using Ynov.Business.Services;
using Ynov.Data.Contexts;
using Ynov.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BoardDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IBoardServices, BoardServices>();
builder.Services.AddScoped<ICardServices, CardServices>();

// Repositories
builder.Services.AddScoped<IBoardRepository, DatabaseBoardRepository>();
builder.Services.AddScoped<ICardRepository, DatabaseCardRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();