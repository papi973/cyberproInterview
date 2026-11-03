using System;
using Microsoft.Extensions.Options;
using InterviewApp.Models;
using System.Collections.Generic;

namespace InterviewApp.Services
{
    public class GreetingService(IOptions<GreetingOptions> options) : IGreetingService
    {
        private const string DefaultLanguage = "English";

        private static readonly Dictionary<string, string> Greetings = new(StringComparer.OrdinalIgnoreCase)
        {
            { "English", "Hello!" },
            { "Afrikaans", "Hallo!" },
            { "Zulu", "Sawubona !" }
        };

        private readonly GreetingOptions _options = options.Value;

        public string GetGreetingLanguage()
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
            Console.WriteLine(_options.Message);
        }
    }
}