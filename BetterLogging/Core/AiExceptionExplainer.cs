using BetterLogging.Abstractions;
using BetterLogging.Configuration;
using BetterLogging.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterLogging.Core
{
    public sealed class AiExceptionExplainer : IAiExceptionExplainer
    {
        private readonly IAiProvider _provider;
        private readonly BetterLoggingOptions _options;

        public AiExceptionExplainer(
            IAiProvider provider,
            IOptions<BetterLoggingOptions> options)
        {
            _provider = provider;
            _options = options.Value;
        }

        public async Task<AiExceptionSummary> ExplainAsync(
            Exception exception,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(exception);

            var serializedException = ExceptionSerializer.Serialize(
                exception,
                _options.MaxStackTraceLines,
                _options.RedactSensitiveData);

            var prompt = PromptBuilder.Build(serializedException);

            var rawResponse = await _provider.GenerateAsync(
                prompt,
                cancellationToken);

            return ResponseParser.Parse(rawResponse);
        }
    }

}
