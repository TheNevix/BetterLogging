namespace BetterLogging.Core
{
    public static class PromptBuilder
    {
        public static string Build(string exceptionText)
        {
            return $"""
                You are a senior .NET software engineer.

                Analyze the following exception and return a JSON object
                with EXACTLY these fields:

                - shortSummary (string, max 20 words)
                - rootCause (string, concise technical explanation)
                - suggestedFix (string, concrete actionable steps)
                - confidence (string: High, Medium, or Low)

                Rules:
                - Do NOT guess missing information
                - Do NOT invent stack frames
                - Base conclusions ONLY on the exception data
                - Keep the response concise
                - Output VALID JSON ONLY

                Exception:
                {exceptionText}
                """;
        }
    }

}
