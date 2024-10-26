using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLink.Application.IUseCases;
using MiniLink.Application.UseCases;
using MiniLink.Domain.Dtos;
using MiniLink.Domain.Repositories;
using MiniLink.Infrastructure.Context;
using MiniLink.Infrastructure.Migrations;
using MiniLink.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
            );

        var response = new ApiResponse<object>(
            success: false,
            message: "Um ou mais erros de validação ocorreram.",
            data: null,
            errors: errors.SelectMany(kvp => kvp.Value).ToList()
        );

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var applicationConnectionString = Environment.GetEnvironmentVariable("SQL_CONN_APPLICATION")
    ?? throw new InvalidOperationException("Variável de ambiente SQL_CONN_APPLICATION não encontrada");
var migrationConnectionString = Environment.GetEnvironmentVariable("SQL_CONN_MIGRATION")
    ?? throw new InvalidOperationException("Variável de ambiente SQL_CONN_MIGRATION não encontrada");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(applicationConnectionString));

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(migrationConnectionString)
        .ScanIn(typeof(V001_CreateLinksSequence).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<IShortenLinkUseCase, ShortenLinkUseCase>();

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
