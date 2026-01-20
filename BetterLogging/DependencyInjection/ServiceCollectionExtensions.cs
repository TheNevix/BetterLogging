using BetterLogging.Abstractions;
using BetterLogging.Configuration;
using BetterLogging.Core;
using BetterLogging.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BetterLogging.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterLoggingServices(
            this IServiceCollection services,
            Action<BetterLoggingOptions> configure)
        {
            services.Configure(configure);

            // Todo: Uncomment and implement AI providers when ready
            services.AddKeyedSingleton<IAiProvider>("Gemini", (sp, key) =>
            {
                var options = sp.GetRequiredService<IOptions<BetterLoggingOptions>>().Value;
                return new GeminiProvider(options.ApiKey);
            });

            services.AddSingleton<IAiExceptionExplainer, AiExceptionExplainer>();

            return services;
        }
    }
}
