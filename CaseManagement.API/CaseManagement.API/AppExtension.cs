using CaseManagement.API.Middlewares;
using FluentMigrator.Runner;

namespace CaseManagement.API
{
    public static class AppExtension
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseWhen(context => !context.Request.Path.Value.ToLower().Trim().Contains("/login") &&
                                  !context.Request.Path.Value.ToLower().Trim().Contains("/signup") &&
                                  !context.Request.Path.Value.ToLower().Trim().Contains("/generate") &&
                                  !context.Request.Path.Value.ToLower().Trim().Contains("/verify"),
                       applicationBuilder => applicationBuilder.UseMiddleware<JwtAuthMiddleware>());

            return app;
        }

        public static void MigrateDatabase(this IApplicationBuilder app) 
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }

    }
}
