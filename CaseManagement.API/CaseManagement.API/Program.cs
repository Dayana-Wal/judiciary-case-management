using CaseManagement.API.Filters;
using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Services;
using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Commands;
using CaseManagement.DataAccess.Entities;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using CaseManagement.API.Middlewares;
using CaseManagement.Business.Service;
using CaseManagement.Business.Queries;
using System.Text.Json.Serialization;
using CaseManagement.Business.Commands;
using CaseManagement.Business.Providers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddDbContext<CaseManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddScoped<IPersonCommandHandler, PersonCommandHandler>();
builder.Services.AddScoped<SignupManager>();
builder.Services.AddScoped<HashHelper>();
builder.Services.AddSingleton<SmsServiceprovider>();
builder.Services.AddSingleton<OtpProvider>();
builder.Services.AddScoped<OtpManager>();
builder.Services.AddScoped<IOtpCommandHandler, OtpCommandHandler>();
builder.Services.AddScoped<JwtTokenProvider>();
builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<PersonQueryHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DBConnectionString"))
        .ScanIn(typeof(CaseManagement.DataAccess.Migrations.CreateInitialSchemaAndSeedLookupConstants).Assembly).For.Migrations());

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
app.UseWhen(context => !context.Request.Path.Value.ToLower().Trim().Contains(@"/login") &&
        !context.Request.Path.Value.ToLower().Trim().Contains("/signup") &&
        !context.Request.Path.Value.ToLower().Trim().Contains("/generate") &&
        !context.Request.Path.Value.ToLower().Trim().Contains("/verify"),
        applicationBUilder => applicationBUilder.UseMiddleware<JwtAuthMiddleware>());
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
