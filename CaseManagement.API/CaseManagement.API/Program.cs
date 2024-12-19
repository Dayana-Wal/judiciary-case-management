using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Providers;
using CaseManagement.Business.Service;
using CaseManagement.Business.Services;
using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Commands;
using CaseManagement.DataAccess.Entities;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
builder.Services.AddDbContext<CaseManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));

// Add services to the container
builder.Services.AddSingleton<SmsServiceprovider>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddScoped<IPersonCommandHandler, PersonCommandHandler>();
builder.Services.AddScoped<SignupManager>(); 
builder.Services.AddScoped<HashHelper>();
builder.Services.AddSingleton<SmsServiceprovider>();
builder.Services.AddSingleton<OtpProvider>();
builder.Services.AddScoped<OtpManager>();
builder.Services.AddScoped<IOtpCommandHandler, OtpCommandHandler>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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

app.UseAuthorization();

// Enable CORS globally
app.UseCors("AllowAnyOrigin");

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}
app.Run();
