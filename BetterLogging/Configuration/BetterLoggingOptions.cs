using System;
using System.Collections.Generic;
using System.Text;

namespace BetterLogging.Configuration
{
    public sealed class BetterLoggingOptions
    {
        public AiModel Model { get; set; }

        public string ApiKey { get; set; } = string.Empty;

        public int MaxStackTraceLines { get; set; } = 15;

        public bool RedactSensitiveData { get; set; } = true;
    }

}
