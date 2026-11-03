using System;
using Microsoft.Extensions.Options;
using InterviewApp.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging; 

namespace InterviewApp.Services
{
    public class GreetingService(IOptions<GreetingOptions> options, ILogger<GreetingService> logger) : IGreetingService
    {
        private const string DefaultLanguage = "English";

        private static readonly Dictionary<string, string> Greetings = new(StringComparer.OrdinalIgnoreCase)
        {
            { "English", "Hello" },
            { "Afrikaans", "Hallo" },
            { "Zulu", "Sawubona" }
        };

        private readonly GreetingOptions _options = options.Value;

        public string GetLanguageGreeting()
        {
            if (string.IsNullOrWhiteSpace(_options.Language))
            {
                return Greetings[DefaultLanguage];
            }

            if (Greetings.TryGetValue(_options.Language, out var greeting))
            {
                return $"{greeting}! {_options.Language}";
            }

            return Greetings[DefaultLanguage];
        }

        public void Run()
        {
            try
            {
                logger.LogInformation("Running greeting service.");
                logger.LogInformation("Greeting message: {Message}", _options.Message);
                Console.WriteLine(GetLanguageGreeting());

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to display greeting.");
            }
        }
    }
}