using JobSchedulerWorker;
using Quartz.Impl;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Quartz.NET POC";
});

var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

var scheduler = await new StdSchedulerFactory().GetScheduler();

builder.Services.AddSingleton(configuration);

builder.Services.AddSingleton(scheduler);

var host = builder.Build();
host.Run();
