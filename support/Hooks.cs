using Reqnroll;
using AventStack.ExtentReports;
using Microsoft.Playwright;

namespace PriyaNewProject.Support
{
    [Binding]
    public class Hooks
    {
        public readonly TestContext _context;
        private static ExtentReports _extent = null!;

        private static EnvironmentSettings _settings = null!; 

        public Hooks(TestContext context)
        {
            _context = context;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _settings = ConfigManager.LoadSettings();
            _extent = ExtentManager.GetExtent();
        }

        [BeforeScenario]
        public async Task BeforeScenario(ScenarioContext scenarioContext)
        {
            _context.ScenarioName = scenarioContext.ScenarioInfo.Title;
            _context.ExtentTest = _extent.CreateTest(_context.ScenarioName);

            try
            {
                _context.Playwright = await Playwright.CreateAsync();
                _context.Browser = await _context.Playwright[_settings.Browser].LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = _settings.Headless
                });
                _context.ExtentTest.Info($"Starting scenario on browser: {_context.Browser.BrowserType.Name}");
                var videosDir = Path.Combine(Directory.GetCurrentDirectory(), "TestResults/videos");
                Directory.CreateDirectory(videosDir);

                _context.BrowserContext = await _context.Browser.NewContextAsync(new BrowserNewContextOptions
                {
                     RecordVideoDir = videosDir,
                     RecordVideoSize = new RecordVideoSize { Width = 1280, Height = 720 }
                });

                _context.Page = await _context.BrowserContext.NewPageAsync();

                await _context.BrowserContext.Tracing.StartAsync(new TracingStartOptions
                {
                    Screenshots = true,
                    Snapshots = true,
                    Sources = true
                });
            }
            catch (Exception ex)
            {
                _context?.ExtentTest?.Fail($"Error in BeforeScenario: {ex.Message}");
                Console.WriteLine($"[BeforeScenario Error] {ex}");
            }
        }

        [AfterScenario]
        public async Task AfterScenario(ScenarioContext scenarioContext)
        {
            bool isFailed = scenarioContext.TestError != null
                            || scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError
                            || scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending
                            || scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.BindingError;

            try
            {
                string traceFile = "null";

                // Stop tracing and save file
                if (_context?.BrowserContext != null)
                {
                    try
                    {
                        var tracesDir = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "traces");
                        Directory.CreateDirectory(tracesDir);

                        traceFile = Path.Combine(tracesDir, $"{_context.ScenarioName}_{DateTime.Now:yyyyMMdd_HHmmss}.zip");
                        await _context.BrowserContext.Tracing.StopAsync(new TracingStopOptions
                        {
                            Path = traceFile
                        });
                    }
                    catch (Exception ex)
                    {
                        _context?.ExtentTest?.Warning($"Trace stop failed: {ex.Message}");
                    }
                }

                // Mark pass/fail/skip
                if (isFailed)
                {
                    if (_context?.Page != null)
                    {
                        try
                        {
                            var screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                            Directory.CreateDirectory(screenshotsDir);

                            var screenshotFile = Path.Combine(screenshotsDir, $"{_context.ScenarioName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                            await _context.Page.ScreenshotAsync(new PageScreenshotOptions
                            {
                                Path = screenshotFile,
                                FullPage = true
                            });

                            _context?.ExtentTest?.Fail($"Scenario failed: {scenarioContext.TestError?.Message ?? "Unknown error"}")
                                .AddScreenCaptureFromPath(screenshotFile);
                        }
                        catch (Exception ex)
                        {
                            _context?.ExtentTest?.Warning($"Screenshot failed: {ex.Message}");
                        }
                        // Add trace link if exists
                        if (!string.IsNullOrEmpty(traceFile) && File.Exists(traceFile))
                        {
                            _context?.ExtentTest?.Info($"<a href='file:///{traceFile}'>Download Playwright Trace</a>");
                        }

                        // Close the page first to finalize the video
                        if (_context?.Page != null)
                        {
                            try
                            {
                                // Close page to finalize video
                                await _context.Page.CloseAsync();
                                var video = _context.Page.Video;
                                if (video != null)
                                {
                                    var videoPath = await video.PathAsync();
                                    if (!string.IsNullOrEmpty(videoPath) && File.Exists(videoPath))
                                    {
                                        _context?.ExtentTest?.Info($"<a href='file:///{videoPath}'>Download Test Video</a>");
                                    }
                                    else
                                    {
                                        _context?.ExtentTest?.Warning("Video file not found after page close.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _context?.ExtentTest?.Warning($"Page close failed: {ex.Message}");
                            }
                        }
                    }   
                    else
                    {
                        _context?.ExtentTest?.Fail($"Scenario failed: {scenarioContext.TestError?.Message ?? "Unknown error"}");
                    }
                }
                else if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
                {
                    _context?.ExtentTest?.Pass("Scenario passed successfully.");
                }
                else
                {
                    _context?.ExtentTest?.Skip($"Scenario skipped: {scenarioContext.ScenarioExecutionStatus}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AfterScenario Error] {ex}");
                _context?.ExtentTest?.Warning($"AfterScenario encountered an error: {ex.Message}");
            }
            finally
            {
                // Always close browser & dispose Playwright
                try
                {
                    if (_context?.Browser != null)
                        await _context.Browser.CloseAsync();
                }
                catch { }

                try
                {
                    _context?.Playwright?.Dispose();
                }
                catch { }
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent?.Flush();
        }
    }
}
