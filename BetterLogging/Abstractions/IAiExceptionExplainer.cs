using BetterLogging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterLogging.Abstractions
{
    public interface IAiExceptionExplainer
    {
        Task<AiExceptionSummary> ExplainAsync(
            Exception exception,
            CancellationToken cancellationToken = default);
    }
}
