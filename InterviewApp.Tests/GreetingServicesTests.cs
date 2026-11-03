using InterviewApp.Models;
using InterviewApp.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace InterviewApp.Tests
{
    public class GreetingServicesTests
    {
        private static GreetingService CreateService(string language)
        {
            var options = Options.Create(new GreetingOptions { Message = "Welcome!", Language = language });
            var time = new Mock<ITimeGreetingService>();
            time.Setup(t => t.GetTimeGreeting()).Returns("Good morning");
            return new GreetingService(options, NullLogger<GreetingService>.Instance, time.Object);
        }

        [Theory]
        [InlineData("English", "Hello!")]
        [InlineData("Afrikaans", "Hallo!")]
        [InlineData("Zulu", "Sawubona!")]
        public void GetGreeting_UsesConfiguredLanguage(string language, string expected)
        {
            Assert.Equal($"Good morning. {expected} Welcome!", CreateService(language).GetGreeting());
        }

        [Fact]
        public void GetGreeting_UnsupportedLanguage_FallsBackToEnglish()
        {
            Assert.Equal("Good morning. Hello!", CreateService("Klingon").GetGreeting());
        }
    }
}
