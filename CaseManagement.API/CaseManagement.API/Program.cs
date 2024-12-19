using CaseManagement.Business.Common;
using CaseManagement.Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;


using FluentMigrator.Runner;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CaseManagement.DataAccess.Queries;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using CaseManagement.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));

// Add services to the container
builder.Services.AddSingleton<SmsServiceprovider>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DBConnectionString"))
        .ScanIn(typeof(CaseManagement.DataAccess.Migrations.CreateInitialSchemaAndSeedLookupConstants).Assembly).For.Migrations());
builder.Services.AddDbContext<CaseManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));
builder.Services.AddScoped<SignupService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<JwtTokenProvider>();
builder.Services.AddScoped<PersonQueryHandler>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<JwtTokenValidatorMiddleware>();

// Add CORS policy to allow any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyOrigin()  // Accept requests from any origin
              .AllowAnyMethod()  // Accept any HTTP method (GET, POST, PUT, etc.)
              .AllowAnyHeader(); // Accept any headers
    });
});

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
app.UseWhen(context => !context.Request.Path.Value.ToLower().Trim().Contains(@"/login"),
    applicationBUilder => applicationBUilder.UseMiddleware<JwtTokenValidatorMiddleware>());
//app.UseMiddleware<JwtTokenValidatorMiddleware>();

// Enable CORS globally
app.UseCors("AllowAnyOrigin");

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}


app.Run();
