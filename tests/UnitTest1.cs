// using System.Text.RegularExpressions;
// using System.Threading.Tasks;
// using Microsoft.Playwright;
// using Microsoft.Playwright.NUnit;
// using NUnit.Framework;
// using AventStack.ExtentReports;
// using AventStack.ExtentReports.Reporter;

// namespace PlaywrightTests;

// [Parallelizable(ParallelScope.Self)]
// [TestFixture]
// public class ExampleTest : TestBase
// {    
//     [Test]
//     [Category("Smoke"), Category("Regression"), Category("Critical")]
//     public async Task HasTitle()
//     {
//         //  _test = _extent.CreateTest("Google Search Test");
//         await _page.GotoAsync("https://playwright.dev");
//         // _test.Info("Navigated to Google");
//         // Expect a title "to contain" a substring.
//         Assert.That(await _page.TitleAsync(), Does.Contain("Playwright"));
//     }

//     [Test]
//     [Category("Regression"), Category("Critical")]
//     public async Task GetStartedLink()
//     {
//         await _page.GotoAsync("https://playwright.dev");

//         // Click the get started link.
//         await _page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

//         // Expects page to have a heading with the name of Installation.
//         Assert.That(await _page.GetByRole(AriaRole.Heading, new() { Name = "Installation" }).IsVisibleAsync(), Is.True);
//     } 
// }