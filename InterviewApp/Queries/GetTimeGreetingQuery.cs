using InterviewApp.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Queries
{
    public record GetTimeGreetingQuery : IRequest<string>;

    public class GetTimeGreetingHandler(ITimeGreetingService timeGreetingService): IRequestHandler<GetTimeGreetingQuery, string>
    {
        public Task<string> Handle(GetTimeGreetingQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(timeGreetingService.GetTimeGreeting());
        }
    }
}
