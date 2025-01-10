using CaseManagement.API.Filters;
using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Providers;
using CaseManagement.Business.Queries;
using CaseManagement.Business.Service;
using CaseManagement.Business.Services;
using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Commands;
using CaseManagement.DataAccess.Entities;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.API
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddScoped<IPersonCommandHandler, PersonCommandHandler>();
            services.AddScoped<SignupManager>();
            services.AddScoped<HashHelper>();
            services.AddScoped<SmsServiceprovider>();
            services.AddScoped<OtpProvider>();
            services.AddScoped<OtpManager>();
            services.AddScoped<IOtpCommandHandler, OtpCommandHandler>();
            services.AddScoped<JwtTokenProvider>();
            services.AddScoped<LoginManager>();
            services.AddScoped<IPersonQueryHandler, PersonQueryHandler>();
            services.AddScoped<IAdminQueryHandler, AdminQueryHandler>();
            services.AddScoped<RoleIdProvider>();
            services.AddScoped<ISearchCasesQueryHandler, SearchCaseQueryHandler>();
            services.AddScoped<CaseSearchManager>();
            services.AddScoped<IFileCommandHandler, FileCommandHandler>();
            services.AddScoped<FileManager>();
            services.AddMemoryCache();
            services.AddScoped<CaseCreationManager>();
            services.AddScoped<ICaseCommandHandler, CaseCommandHandler>();
            services.AddScoped<ICaseFileCommandHandler, CaseFileCommandHandler>();

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;

        }

        public static IServiceCollection AddInfraStructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CaseManagementContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DBConnectionString"));
            });

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(configuration.GetConnectionString("DBConnectionString"))
                .ScanIn(typeof(CaseManagement.DataAccess.Migrations.CreateInitialSchemaAndSeedLookupConstants).Assembly).For.Migrations());
            
            return services;
        }
    
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
