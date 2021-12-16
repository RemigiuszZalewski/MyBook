using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _timer;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            var elapsedTime = _timer.ElapsedMilliseconds;

            if (elapsedTime > 1000)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogInformation("MyBookAPI request {requestName} was running {elapsedTime} {@request}", requestName, elapsedTime, request);
            }

            return response;
        }
    }
}
