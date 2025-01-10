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
app.UseCors("AllowAnyOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomMiddleware();
// Enable CORS globally

app.MapControllers();

//Run migrations
app.MigrateDatabase();

app.MapControllers();
app.Run();
