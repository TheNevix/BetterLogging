using System.Text.RegularExpressions;

namespace BetterLogging.Core
{
    public static class RedactionEngine
    {
        private static readonly Regex[] SensitivePatterns =
        {
        // API Keys (generic)
        new Regex(@"(?i)(api[_-]?key\s*[:=]\s*)([a-z0-9-_]{16,})"),

        // Bearer tokens / JWTs
        new Regex(@"eyJ[a-zA-Z0-9_-]+?\.[a-zA-Z0-9_-]+?\.[a-zA-Z0-9_-]+"),

        // Connection strings
        new Regex(@"(?i)(password\s*=\s*)([^;]+)"),

        // Emails
        new Regex(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}")
    };

        public static string Redact(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var result = input;

            foreach (var regex in SensitivePatterns)
            {
                result = regex.Replace(result, "***REDACTED***");
            }

            return result;
        }
    }
}
