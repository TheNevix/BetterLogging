namespace BetterLogging.Core
{
    public static class ExceptionSerializer
    {
        public static string Serialize(
            Exception exception,
            int maxStackTraceLines,
            bool redactSensitiveData)
        {
            var stackTrace = exception.StackTrace?
                .Split(Environment.NewLine)
                .Take(maxStackTraceLines);

            var text = $"""
                Exception Type:
                {exception.GetType().FullName}

                Message:
                {exception.Message}

                Stack Trace:
                {string.Join(Environment.NewLine, stackTrace ?? Enumerable.Empty<string>())}

                Inner Exception:
                {exception.InnerException?.Message}
                """;

            return redactSensitiveData
                ? RedactionEngine.Redact(text)
                : text;
        }
    }

}
