using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace PriyaNewProject.Support
{
    public static class ExtentManager
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _htmlReporter;
        private static readonly object _lock = new();

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                lock (_lock)
                {
                    if (_extent == null)
                    {
                        // Navigate from bin/Debug/netX.X to project root
                        string baseDir = AppContext.BaseDirectory; // bin/Debug/netX.Y/...
                        string projectRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..")); 
                        string reportsDir = Path.Combine(projectRoot, "TestResults", "ExtentReports");
                        Directory.CreateDirectory(reportsDir);
                        string env = Environment.GetEnvironmentVariable("TEST_ENV") ?? "dev";
                        var reportPath = Path.Combine(reportsDir, $"ExtentReport_{env}_{DateTime.Now:yyyyMMdd_HHmmss}.html");
                        _htmlReporter = new ExtentSparkReporter(reportPath);
                        _htmlReporter.Config.DocumentTitle = $"Test Report - {env.ToUpper()}";
                        _htmlReporter.Config.ReportName = $"Playwright + Reqnroll - {env.ToUpper()}";
                        _htmlReporter.Config.Theme = Theme.Dark;

                        _extent = new ExtentReports();
                        _extent.AttachReporter(_htmlReporter);
                        _extent.AddSystemInfo("Environment", env);
                        _extent.AddSystemInfo("Browser", "Playwright Configured");
                    }
                }
            }
            return _extent;
        }
    }
}