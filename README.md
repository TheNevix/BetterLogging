![BetterLogging Banner](https://github.com/TheNevix/BetterLogging/blob/master/assets/banner.png)
# BetterLogging

![NuGet](https://img.shields.io/nuget/v/BetterLogging) ![NuGet Downloads](https://img.shields.io/nuget/dt/BetterLogging)

⚠️ **Status:** Pre-release (alpha)  
This package is a work in progress toward a stable, feature-rich release.

---

## Overview

**BetterLogging** brings AI-assisted error analysis to .NET logging.

When an exception occurs, BetterLogging can send structured exception data to an AI model to:
- Summarize what the error means
- Explain common root causes
- Suggest possible fixes

The goal is to make debugging faster—especially in complex or hard-to-reproduce scenarios.

---

## ⚠️ Important Notes

- This package sends exception data to an external AI provider.
- Use selectively in production (e.g., error paths, sampled logs).
- AI responses are non-deterministic and should not be treated as authoritative.
- This package does **not** automatically catch exceptions — you opt in explicitly.

---

## Installation

Because this package is currently **pre-release**, install with:

```bash
dotnet add package BetterLogging --prerelease
```
---

## Usage

### Register service
Add the custom services in your program.cs.

```csharp
services.AddBetterLoggingServices(options =>
{
    options.Model = Gemini.Models.FlashLite2_5;
    options.ApiKey = "YOUR API-KEY";
});
```

### Example usage in code
Use IAiExceptionExplainer AI to pass your exception and receive a detailed result.

```csharp
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
```

---

## License

This project is licensed under the MIT License.
