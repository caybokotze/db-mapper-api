using Core.Api.Services;

var builder = WebApplication.CreateSlimBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<DbConnectionFactory>();
builder.Services.AddTransient<DatabaseFactory>();
builder.Services.AddTransient<MsSqlService>();

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