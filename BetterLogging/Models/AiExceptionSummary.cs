using System;
using System.Collections.Generic;
using System.Text;

namespace BetterLogging.Models
{
    public sealed class AiExceptionSummary
    {
        public string ShortSummary { get; init; } = string.Empty;
        public string RootCause { get; init; } = string.Empty;
        public string SuggestedFix { get; init; } = string.Empty;
        public string? Confidence { get; init; }

        public override string ToString()
        {
            return $"{ShortSummary}\nCause: {RootCause}\nFix: {SuggestedFix}";
        }
    }

}
