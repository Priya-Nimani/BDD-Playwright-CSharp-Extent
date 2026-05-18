using Microsoft.Playwright;
using PriyaNewProject.Support;

namespace PriyaNewProject.PageObjects
{
    public class HomePage (Hooks hooks)
    {
        private readonly IPage _page = hooks._context.Page;

        public async Task GoTo()
        {
            await _page.GotoAsync("https://www.playwright.dev");
        } 

        public async Task CheckTitleContains(string expectedTitle)
        {
            Assert.That(await _page.TitleAsync(), Does.Contain(expectedTitle));
        }

        public async Task ClickGetStarted()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();
        }
    }
}