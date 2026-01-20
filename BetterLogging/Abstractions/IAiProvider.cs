using BetterLogging.Configuration;

namespace BetterLogging.Abstractions
{
    public interface IAiProvider
    {
        string ProviderName { get; }

        Task<string> GenerateAsync(
            string prompt,
            AiModel model,
            CancellationToken cancellationToken = default);
    }
}
