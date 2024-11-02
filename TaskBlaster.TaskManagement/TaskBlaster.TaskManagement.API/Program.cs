using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;

var builder = WebApplication.CreateBuilder(args);


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