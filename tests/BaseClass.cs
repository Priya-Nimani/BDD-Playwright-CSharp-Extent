// using Microsoft.Playwright;
// using NUnit.Framework;
// using AventStack.ExtentReports;
// using AventStack.ExtentReports.Reporter;
// using System;
// using System.IO;
// using System.Threading.Tasks;
// using AventStack.ExtentReports.Reporter.Config;
// using NUnit.Framework.Interfaces;

// public abstract class TestBase
// {
//     protected IPlaywright _playwright;
//     protected IBrowser _browser;
//     protected IPage _page;

//     private static ExtentReports _extent;
//     protected ExtentTest _test;

//     private static string _reportPath;

//     [OneTimeSetUp]
//     public async Task GlobalSetup()
//     {
//         // Create report folder
//         _reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TestReports");
//         Directory.CreateDirectory(_reportPath);

//         // Setup Extent HTML Reporter
//         ExtentSparkReporter  htmlReporter = new ExtentSparkReporter(Path.Combine(_reportPath, "ExtentReport.html"));
//         htmlReporter.Config.DocumentTitle = "Playwright Test Report";
//         htmlReporter.Config.ReportName = "Automation Results";
//         htmlReporter.Config.Theme = Theme.Dark;

//         _extent = new ExtentReports();
//         _extent.AttachReporter(htmlReporter);

//         // Launch Playwright
//         _playwright = await Playwright.CreateAsync();
//         _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
//     }

//     [SetUp]
//     public async Task Setup()
//     {
//         _page = await _browser.NewPageAsync();
//         string testName = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}";
//         _test = _extent.CreateTest(testName);
//         await TraceHelper.StartTracingAsync(_page.Context);
//     }       

//     [TearDown]
//     public async Task TearDown()        
//     {
//         var outcome = TestContext.CurrentContext.Result.Outcome.Status;
//         var message = TestContext.CurrentContext.Result.Message;

//         if (outcome == TestStatus.Passed)
//             _test.Pass("Test passed");
//         else if (outcome == TestStatus.Failed)
//             _test.Fail($"Test failed: {message}");

//         var tracePath = outcome == TestStatus.Failed ? Path.Combine(
//             TestContext.CurrentContext.WorkDirectory,
//             "playwright-traces",
//             $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
//         ) : null;
//         await TraceHelper.StopTracingAsync(_page.Context, $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}");
//         await _page.CloseAsync();
//     }

//     [OneTimeTearDown]
//     public async Task GlobalTeardown()
//     {
//         await _browser.CloseAsync();
//         _extent.Flush();
//     }
// }
