using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public class TimeGreetingService(TimeProvider timeProvider) : ITimeGreetingService
    {
        public string GetTimeGreeting()
        {
            var currentHour = timeProvider.GetLocalNow().Hour;
            return currentHour switch
            {
                < 12 => "Good morning",
                < 17 => "Good afternoon",
                _ => "Good evening"
            };
        }
    }
}
