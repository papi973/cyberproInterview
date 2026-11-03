using InterviewApp.Models;
using InterviewApp.Services;
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

        var greetingService = host.Services.GetRequiredService<IGreetingService>();
        greetingService.Run();
    }
}