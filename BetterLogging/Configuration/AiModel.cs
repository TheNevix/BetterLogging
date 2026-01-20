namespace BetterLogging.Configuration
{
    public sealed class AiModel
    {
        public string Provider { get; }
        public string Name { get; }


        internal AiModel(string provider, string name)
        {
            Provider = provider;
            Name = name;
        }

        public override string ToString() => $"{Provider}:{Name}";
    }
}
