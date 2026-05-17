using Microsoft.Playwright;

public static class TraceHelper
{
    public static async Task StartTracingAsync(IBrowserContext context)
    {
        await context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    public static async Task StopTracingAsync(IBrowserContext context, string testName)
    {
        var fileName = $"trace-{testName}-{DateTime.Now:yyyyMMdd-HHmmss}.zip";

        await context.Tracing.StopAsync(new TracingStopOptions
        {
            Path = fileName
        });

        Console.WriteLine($"Trace saved: {fileName}");
    }
}
