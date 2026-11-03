using System;
using Microsoft.Extensions.Options;
using InterviewApp.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging; 

namespace InterviewApp.Services
{
    public class GreetingService(IOptions<GreetingOptions> options, ILogger<GreetingService> logger, ITimeGreetingService timeGreetingService) : IGreetingService
    {

        private const string DefaultLanguage = "English";

        private static readonly Dictionary<string, string> Greetings = new(StringComparer.OrdinalIgnoreCase)
        {
            { "English", "Hello!" },
            { "Afrikaans", "Hallo!" },
            { "Zulu", "Sawubona!" }
        };

        private readonly GreetingOptions _options = options.Value;

        public string GetLanguageGreeting()
        {
            logger.LogInformation("GreetingService started for language '{Language}'\n", _options.Language);

            if (!Greetings.TryGetValue(_options.Language ?? "", out var greeting))
            {
                return Greetings[DefaultLanguage];
            }

            return $"{greeting} {_options.Message}";
        }

        public string GetGreeting()
        {
            return $"{timeGreetingService.GetTimeGreeting()}. {GetLanguageGreeting()} \n\n";
        }
    }
}