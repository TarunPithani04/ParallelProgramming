using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Serilog;
using Serilog.Context;
using WorkerService;

var builder = Host.CreateApplicationBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<LoanService, LoanService>();
builder.Services.AddSingleton<DataActions, DataActions>();
builder.Services.AddDbContext<LoanContext>(options => options.UseSqlServer("Data Source=OSI-L-0399;Initial Catalog=ParallelProgramming;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"), ServiceLifetime.Singleton);
var host = builder.Build();
host.Run();
