using Base.Infrastructure;
using Base.Infrastructure.Extensions;
using SampleApi.Data.Mssql;
using SampleApi.Data.SQLite;
using SampleApi.Interfaces;

#nullable disable

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    var configuration = builder.Configuration;

    services
        .AddMSSQL<SampleMssqlContext>(configuration.GetConnectionString("MssqlConnection"))
        .AddScoped<ISampleMssqlContext>(provider => provider.GetService<SampleMssqlContext>());

    services
        .AddSQLite<SampleSqliteContext>(configuration.GetConnectionString("SqliteConnection"))
        .AddScoped<ISampleSqliteContext>(provider => provider.GetService<SampleSqliteContext>());

    // Inject Infrastructure layer
    services.AddInfrastructure();

    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}



var app = builder.Build();

{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

app.Run();
