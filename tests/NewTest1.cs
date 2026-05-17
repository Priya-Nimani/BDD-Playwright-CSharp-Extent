// using Microsoft.Playwright.NUnit;
// using Microsoft.Playwright;
// using NUnit.Framework;
// using System.Text.RegularExpressions;

// namespace MyTestsDemo;

// [Parallelizable(ParallelScope.Self)]
// [TestFixture]
// public class Tests : PageTest
// {
//     [SetUp]
//      public async Task Setup()
//     {
//         await Context.Tracing.StartAsync(new()
//         {
//             Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
//             Screenshots = true,
//             Snapshots = true,
//             Sources = true
//         });
//     }

//     [TearDown]
//      public async Task TearDown()
//     {
//         var failed = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
//             || TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;
        
//         string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
//         string traceFileName = $"trace_{timestamp}.zip";
//         string traceFilePath = Path.Combine(Directory.GetCurrentDirectory(), traceFileName);

//         await Context.Tracing.StopAsync(new()
//         {
//             Path = failed ? traceFilePath:null,
//         });
//     }
    
//     [Test]
//     [Category("Smoke")]

//     public async Task MyTest()
//     {
//         await Page.GotoAsync("https://playwright.dev");
//         await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
//         await Page.ClickAsync("text=Get Started");
//         await Expect(Page).ToHaveURLAsync(new Regex(".*docs/intro.*"));
//     }
// }
