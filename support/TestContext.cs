using Microsoft.Playwright;
using AventStack.ExtentReports;

namespace PriyaNewProject.Support
{
    public class TestContext
    {
        public required IPlaywright Playwright { get; set; }
        public required IBrowser Browser { get; set; }
        public required IPage Page { get; set; }
        public required IBrowserContext BrowserContext { get; set; }
        public string ScenarioName { get; set; } = null!;
        public required ExtentTest ExtentTest { get; set; }
    }
}