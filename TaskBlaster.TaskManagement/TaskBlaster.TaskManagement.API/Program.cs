using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.API.Services.Implementations;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Implementations;
using TaskBlaster.TaskManagement.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddDbContext<TaskManagementDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("TaskBlasterDatabaseConnectionString"),
        b => b.MigrationsAssembly("TaskBlaster.TaskManagement.API")
    )
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();