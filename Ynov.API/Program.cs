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

DotNetEnv.Env.Load();
var connectionString = Environment.GetEnvironmentVariable("YNOV_BOARD_POSTGRES");

builder.Services.AddDbContext<TrellodDbContext>(options =>
    options.UseNpgsql(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IBoardServices, BoardServices>();
builder.Services.AddScoped<ICardServices, CardServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ILabelServices, LabelServices>();
builder.Services.AddScoped<IPasswordServices, PasswordServices>();
builder.Services.AddScoped<IChecklistServices, ChecklistServices>();
builder.Services.AddScoped<IChecklistItemServices, ChecklistItemServices>();

// Repositories
builder.Services.AddScoped<IBoardRepository, DatabaseBoardRepository>();
builder.Services.AddScoped<ICardRepository, DatabaseCardRepository>();
builder.Services.AddScoped<IUserRepository, DatabaseUserRepository>();
builder.Services.AddScoped<ILabelRepository, DatabaseLabelRepository>();
builder.Services.AddScoped<IChecklistItemRepository, DatabaseChecklistItemRepository>();
builder.Services.AddScoped<IChecklistRepository, DatabaseChecklistRepository>();

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