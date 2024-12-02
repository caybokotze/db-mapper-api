using Core.Api.Configuration;
using Core.Api.Services;

var builder = WebApplication.CreateSlimBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<Database>(builder.Configuration.GetSection("DbMapper"));
builder.Services.AddTransient<DatabaseConnectionFactory>();

builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();