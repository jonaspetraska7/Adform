using Common.Data;
using Common.Entities;
using Common.Interfaces;
using Common.Services;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLinqToDBContext<AdformDataConnection>((provider, options) => {
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("AdformDb"))
    .UseDefaultLogging(provider);
});

builder.Services.AddScoped<ISquareService, SquareService>();
builder.Services.AddScoped<IPointService, PointService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(5);
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

using (var scope = app.Services.CreateScope())
{
    var dataConnection = scope.ServiceProvider.GetService<AdformDataConnection>();
    var sp = dataConnection.DataProvider.GetSchemaProvider();
    var dbSchema = sp.GetSchema(dataConnection);
    if (!dbSchema.Tables.Any(t => t.TableName == nameof(PointListDto)))
    {
        dataConnection.CreateTable<PointListDto>();
    }
}

app.MapControllers();

app.Run();
