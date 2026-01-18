using BetterLogging.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BetterLogging.Core
{
    public static class ResponseParser
    {
        public static AiExceptionSummary Parse(string rawResponse)
        {
            var cleaned = ExtractJson(rawResponse);

            try
            {
                return JsonSerializer.Deserialize<AiExceptionSummary>(
                    cleaned,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })!;
            }
            catch
            {
                return Fallback(rawResponse);
            }
        }

        private static string ExtractJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // Remove markdown fences: ``` or ```json
            input = Regex.Replace(input, @"```(?:json)?", "", RegexOptions.IgnoreCase)
                         .Trim();

            // Extract first JSON object
            var start = input.IndexOf('{');
            var end = input.LastIndexOf('}');

            if (start >= 0 && end > start)
                return input.Substring(start, end - start + 1);

            return input;
        }

        private static AiExceptionSummary Fallback(string raw)
        {
            return new AiExceptionSummary
            {
                ShortSummary = "AI analysis failed",
                RootCause = "The AI response could not be parsed as structured JSON",
                SuggestedFix = "Inspect raw AI response and adjust prompt or parser",
                Confidence = "Low"
            };
        }
    }

}
