using BetterLogging.Abstractions;
using BetterLogging.Configuration;
using BetterLogging.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BetterLogging.Core
{
    public sealed class AiExceptionExplainer : IAiExceptionExplainer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly BetterLoggingOptions _options;

        public AiExceptionExplainer(
            IServiceProvider serviceProvider,
            IOptions<BetterLoggingOptions> options)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public async Task<AiExceptionSummary> ExplainAsync(
            Exception exception,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(exception);

            var model = _options.Model;

            var provider = _serviceProvider
                .GetRequiredKeyedService<IAiProvider>(model.Provider);

            var serializedException = ExceptionSerializer.Serialize(
                exception,
                _options.MaxStackTraceLines,
                _options.RedactSensitiveData);

            var prompt = PromptBuilder.Build(serializedException);

            var rawResponse = await provider.GenerateAsync(
                prompt,
                model,
                cancellationToken);

            return ResponseParser.Parse(rawResponse);
        }
    }

}
