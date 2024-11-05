using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.API.Services;
using TaskBlaster.TaskManagement.API.Services.Implementations;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Implementations;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.InputModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IPriorityRepository, PriorityRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IStatusRepository, StatusRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();

builder.Services.AddTransient<IPriorityService, PriorityService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<ITagService, TagService>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IProfileService, ProfileService>();


builder.Services.AddDbContext<TaskManagementDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("TaskBlasterDatabaseConnectionString"),
        b => b.MigrationsAssembly("TaskBlaster.TaskManagement.API")
    )
);

builder.Services.AddMvc();

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options =>
// {
//     options.Authority = builder.Configuration.GetValue<string>("Auth0:Authority");
//     options.Audience = builder.Configuration.GetValue<string>("Auth0:Audience");
//     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true
//     };
// });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("Auth0:Authority");
        options.Audience = builder.Configuration.GetValue<string>("Auth0:Audience");

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

                var user = context.Principal;
                if (user?.Identity?.IsAuthenticated != true)
                {
                    return;
                }

                var emailClaim = user.FindFirst(ClaimTypes.Email)?.Value ?? user.FindFirst("email")?.Value;
                var nameClaim = user.FindFirst(ClaimTypes.Name)?.Value ?? user.FindFirst("name")?.Value;

                var pictureClaim = user.FindFirst("picture")?.Value;
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                if (string.IsNullOrEmpty(emailClaim) || string.IsNullOrEmpty(nameClaim))
                {
                    logger.LogWarning("Missing required claims in token. Email: {EmailClaim}, Name: {NameClaim}",
                        emailClaim, nameClaim);
                    return;
                }

                var userInput = new UserInputModel
                {
                    Email = emailClaim,
                    FullName = nameClaim,
                    ProfileImageUrl = pictureClaim
                };

                try
                {
                    await userService.CreateUserIfNotExistsAsync(userInput);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error creating/updating user from token");
                }
            }
        };
    });

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddControllersAsServices();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();