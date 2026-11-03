using InterviewApp.Models;
using InterviewApp.Queries;
using InterviewApp.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) => 
            {
                services.Configure<GreetingOptions>(context.Configuration.GetSection("Greeting"));
                services.AddTransient<IGreetingService, GreetingService>();
                services.AddSingleton(TimeProvider.System);
                services.AddTransient<ITimeGreetingService, TimeGreetingService>();
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            })
            .Build();

        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        var options = host.Services.GetRequiredService<IOptions<GreetingOptions>>().Value;

        if (string.IsNullOrWhiteSpace(options.Message))
        {
            logger.LogError("Invalid configuration: Greeting:Message is required.");
            Environment.ExitCode = 1;
            return;
        }
        else if (string.IsNullOrWhiteSpace(options.Language))
        {
            logger.LogError("Invalid configuration: Greeting:Language is required.");
            Environment.ExitCode = 1;
            return;
        }

        try
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var greeting = await mediator.Send(new GreetUserQuery());
            Console.WriteLine(greeting);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to display greeting");
            Environment.ExitCode = 1;
        }
    }
}