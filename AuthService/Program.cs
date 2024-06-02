using AuthService.AppCore.QueueServices;
using AuthService.Extensions;
using AuthService.Persistence.ApplicationContext;
using MediatR;
using Common.Services.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AuthService.Persistence.ModelBuilders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddCachingService();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.CustomDependencyInjection(builder.Configuration);

string dbConstring = Environment.GetEnvironmentVariable(builder.Configuration.GetConnectionString("Default") ?? string.Empty, EnvironmentVariableTarget.Process) ?? builder.Configuration.GetValue<string>("ConnectionStrings:Default");
builder.Services.AddHostedService<QueueServices>();
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(dbConstring, options => options.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds))
    .UseLoggerFactory(LoggerFactory.Create(buildr =>
    {
        if (builder.Environment.IsDevelopment())
        {
            buildr.AddDebug();
        }
    }))
    );

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(Program).Assembly);


builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SimplifiedUI Auth APIs",
        Version = "1.0.0",
        Contact = new OpenApiContact { Email = "applicationdev@simplifiedui.com", Name = "SimplifiedUIAuthSvc" }
    });
    
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

try { app.SeedRoleData().Wait(); } catch (Exception ex) { Log.Error(ex.Message, ex); }

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpolifiedUI AuthSvc  v1");
        c.DefaultModelsExpandDepth(-1);
    });
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
