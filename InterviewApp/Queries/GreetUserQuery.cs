using InterviewApp.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Queries
{
    public record GreetUserQuery : IRequest<string>;

    public class GreetUserHandler(IGreetingService greetingService, ISender sender) : IRequestHandler<GreetUserQuery, string>
    {
        public async Task<string> Handle(GreetUserQuery request, CancellationToken cancellationToken)
        { 
            var timeGreeting = await sender.Send(new GetTimeGreetingQuery(), cancellationToken);
            return $"{timeGreeting}. {greetingService.GetLanguageGreeting()}";
        }
    }
}
