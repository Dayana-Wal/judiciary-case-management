using CaseManagement.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfraStructureService(builder.Configuration);
builder.Services.ConfigureCors();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomMiddleware();
// Enable CORS globally
app.UseCors("AllowAnyOrigin");
app.MapControllers();

//Run migrations
app.MigrateDatabase();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}
app.Run();
