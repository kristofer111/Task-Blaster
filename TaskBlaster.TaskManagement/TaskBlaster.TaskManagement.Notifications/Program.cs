using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskBlaster.TaskManagement.DAL.Implementations;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITaskRepository, TaskRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serviceIpOptions = new ServiceIpOptions();
builder.Configuration.GetSection("ServiceIp").Bind(serviceIpOptions);
builder.Services.AddSingleton(serviceIpOptions);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration.GetValue<string>("Auth0:Authority");
    options.Audience = builder.Configuration.GetValue<string>("Auth0:Audience");
});


// Todo: Setup all required middlewares to run Hangfire background processing service

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(builder.Configuration
        .GetConnectionString("HangfireDatabaseConnectionString")));


builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
HangfireJobsConfigurator.ConfigureRecurringJobs();

app.MapControllers();

app.Run();