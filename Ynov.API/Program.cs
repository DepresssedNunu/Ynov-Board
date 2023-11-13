using Microsoft.EntityFrameworkCore;

using Ynov.API;
using Ynov.Business.IRespositories;
using Ynov.Business.IServices;
using Ynov.Business.Services;
using Ynov.Data.Contexts;
using Ynov.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BoardDbContext>(opt =>
    opt.UseInMemoryDatabase("CSTrelllo"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IBoardServices, BoardServices>();
builder.Services.AddScoped<ICardServices, CardServices>();

// Repositories
builder.Services.AddScoped<IBoardRepository, InMemoryBoardRepository>();
builder.Services.AddScoped<ICardRepository, InMemoryCardRepository>();

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

Init.Test();

app.Run();