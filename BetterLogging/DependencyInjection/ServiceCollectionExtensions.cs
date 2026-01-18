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
            services.AddSingleton<IAiProvider>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<BetterLoggingOptions>>().Value;

                return options.Model switch
                {
                    AiModel.Gemini => new GeminiProvider(options.ApiKey),
                    _ => throw new NotSupportedException("Unsupported AI model")
                };
            });

            services.AddSingleton<IAiExceptionExplainer, AiExceptionExplainer>();

            return services;
        }
    }
}
