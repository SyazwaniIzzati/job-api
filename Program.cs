using JobApi.Repositories;
using JobApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IJobRepository, InMemoryJobRepository>();
builder.Services.AddSingleton<IApplicationRepository, InMemoryApplicationRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();