using MediatR;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Dapper.FluentMap;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Internal;
using Newtonsoft.Json.Converters;
using Questao5.Application.Behaviours;
using Questao5.Filters;
using Questao5.Infrastructure.Database.Contracts;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Mapping;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options => 
        options.SerializerSettings.Converters.Add(new StringEnumConverter()));

FluentMapper.Initialize(configuration =>
{
    configuration.AddMap(new TransactionMapping());
    configuration.AddMap(new AccountMapping());
    configuration.AddMap(new IdempotencyMapping());
});
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnhandledExceptionBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddFluentValidationClientsideAdapters();

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();

builder.Services.AddControllers(options =>options.Filters.Add<ApiExceptionFilterAttribute>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
app.Services.GetService<IDatabaseBootstrap>().Setup();

app.Run();

// Informaçõees úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html