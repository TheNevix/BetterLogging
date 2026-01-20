using BetterLogging;
using BetterLogging.Abstractions;
using BetterLogging.Configuration;
using BetterLogging.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

services.AddLogging(builder => builder.AddConsole());

services.AddBetterLoggingServices(options =>
{
    options.Model = Gemini.Models.Flash2_5;
    options.ApiKey = "AIzaSyCKj0NY2WoC3wImN-GyXQ_gvMToIGxBTtI";
});

var provider = services.BuildServiceProvider();
var logger = provider.GetRequiredService<ILogger<Program>>();

try
{
    ThrowSomething();
}
catch (Exception ex)
{
    var ai = provider.GetRequiredService<IAiExceptionExplainer>();
    var summary = await ai.ExplainAsync(ex);

    logger.LogError("AI Summary:\n{Summary}", summary);

    var p = 5;
}

static void ThrowSomething()
{
    string? value = null;
    Console.WriteLine(value.Length); // 💥
}
