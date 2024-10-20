using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using MiniLink.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var applicationConnectionString = Environment.GetEnvironmentVariable("SQL_CONN_APPLICATION")
                                  ?? throw new InvalidOperationException("SQL_CONN_APPLICATION not found");
var migrationConnectionString = Environment.GetEnvironmentVariable("SQL_CONN_MIGRATION")
                                ?? throw new InvalidOperationException("SQL_CONN_MIGRATION not found");

builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(applicationConnectionString));
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(migrationConnectionString)
        .ScanIn(typeof(MiniLink.Infrastructure.Migrations.V001_CreateLinksSequence).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    migrationRunner.MigrateUp();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();