using CaseManagement.Business.Common;
using CaseManagement.Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;


using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Commands;
using CaseManagement.DataAccess.Entities;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));

builder.Services.AddDbContext<CaseManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString"))
);

// Add services to the container
builder.Services.AddSingleton<SmsServiceprovider>();

builder.Services.AddScoped<IPersonCommandHandler, PersonCommandHandler>();
builder.Services.AddScoped<SignupManager>(); 
builder.Services.AddScoped<HashHelper>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DBConnectionString"))
        .ScanIn(typeof(CaseManagement.DataAccess.Migrations.CreateInitialSchemaAndSeedLookupConstants).Assembly).For.Migrations());
builder.Services.AddScoped<SignupService>();

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
