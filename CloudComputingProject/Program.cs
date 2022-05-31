using CloudComputingProject.Infrastructure;
using CloudComputingProject.Model.Profile;
using CloudComputingProject.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<TrainProfile>());
builder.Services.AddTransient<ITrainService, TrainService>();
builder.Services.AddTransient<IQueueService, QueueService>();
builder.Services.AddTransient<IStationService, StationService>();
var accountEndpoint = builder.Configuration["CosmosDb:Account"];
var accountKey = builder.Configuration["CosmosDb:Key"];
var dbName = builder.Configuration["CosmosDb:dbName"];
builder.Services.AddDbContext<TrainDbContext>(x => x.UseCosmos(accountEndpoint, accountKey, dbName));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
