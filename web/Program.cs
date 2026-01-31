using Domain;
using Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var configuration = builder.Configuration.Get<Configuration>()!;
builder.Services.AddSingleton(configuration);
builder.Services.AddSingleton<Database>();
builder.Services.AddTransient<ImportService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddResponseCompression();
var application = builder.Build();
application.UseCors();
application.MapControllers();
await application.RunAsync();
