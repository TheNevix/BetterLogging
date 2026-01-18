namespace BetterLogging.Abstractions
{
    public interface IAiProvider
    {
        Task<string> GenerateAsync(
            string prompt,
            CancellationToken cancellationToken = default);
    }
}
