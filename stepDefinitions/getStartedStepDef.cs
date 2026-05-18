using PriyaNewProject.PageObjects;
using Reqnroll;
using PriyaNewProject.Support;

namespace PriyaNewProject.StepDefinitions
{
    [Binding]
    public class GetStartedStepDef  
    {
        private ScenarioContext scenarioContext;
        private readonly GetStartedPage _getStartedPage;

        public GetStartedStepDef(Hooks hooks, GetStartedPage getStartedPage, ScenarioContext scenarioContext)
        {
            _getStartedPage = getStartedPage;
            this.scenarioContext = scenarioContext;
        }   

        [Then(@"Get Started page title is {string}")]
        public async Task ThenGetStartedPageTitleIs(string expectedTitle)
        {
            await _getStartedPage.CheckTitleContains(expectedTitle);
        }   

        [Then("the page has a heading with the name of {string}")]
        public async Task ThenThePageHasAHeadingWithTheNameOf(string InstallationHeading)
        {
            await _getStartedPage.CheckInstallationHeadingIsVisible(InstallationHeading);
        }
    }
}