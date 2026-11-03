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
    public record GreetUserQuery : IRequest<string>;

    public class GreetUserHandler(IGreetingService greetingService) : IRequestHandler<GreetUserQuery, string>
    {
        public Task<string> Handle(GreetUserQuery request, CancellationToken cancellationToken)
            => Task.FromResult(greetingService.GetGreeting());
    }
}
