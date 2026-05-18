using Microsoft.Playwright;
using PriyaNewProject.Support;

namespace PriyaNewProject.PageObjects
{
    public class GetStartedPage (Hooks hooks)
    {
        private readonly IPage _page = hooks._context.Page;

        public async Task CheckInstallationHeadingIsVisible(string headingName)
        {
            Assert.That(await _page.GetByRole(AriaRole.Heading, new() { Name = headingName }).IsVisibleAsync(), Is.True);
        }
            public async Task CheckTitleContains(string expectedTitle)
            {
                Assert.That(await _page.TitleAsync(), Does.Contain(expectedTitle));
            }
    }
}
