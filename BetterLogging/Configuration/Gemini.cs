namespace BetterLogging.Configuration
{
    public static class Gemini
    {
        public static class Models
        {
            public static readonly AiModel FlashLite2_5 =
                new AiModel("Gemini", "gemini-2.5-flash-lite");

            // Gives a non parseable response
            //public static readonly AiModel Flash2_5 =
            //    new AiModel("Gemini", "gemini-2.5-flash");

            public static readonly AiModel Flash3 =
                new AiModel("Gemini", "gemini-3-flash-preview");
        }
    }
}
