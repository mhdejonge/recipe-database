using Domain;
using Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var configuration = builder.Configuration.Get<Configuration>()!;
var database = new Database(configuration.DatabaseName, configuration.CollectionName);
builder.Services.AddSingleton(database);
builder.Services.AddResponseCompression();
var application = builder.Build();
application.UseHttpsRedirection();
application.UseAuthorization();
application.MapControllers();
application.Run();
