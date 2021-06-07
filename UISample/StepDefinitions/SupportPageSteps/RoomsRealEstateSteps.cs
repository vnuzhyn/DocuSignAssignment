using TechTalk.SpecFlow;
using UISample.Pages;
using Xunit;

namespace UISample.StepDefinitions.SupportPageSteps
{
    [Binding]
    public class RoomsRealEstateSteps : BaseSteps
    {
        public RoomsRealEstateSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext)
        {
        }

        [Given(@"consumer is at the support home page")]
        public void GivenConsumerIsAtTheSupportHomePage()
        {
            //hooks
        }

        [When(@"consumer is searching for '(.*)'")]
        public void WhenConsumerIsSearchingFor(string input)
        {
            Driver.Page<SupportHomePage>().SearchFor(input);
        }

        [When(@"search response contains (.*) total results")]
        public void WhenSearchResponseContainsTotalResults(int resultsCount)
        {
            var el = Driver.Page<SupportSearchResults>().SearchResultInfo;
            Driver.Page<SupportSearchResults>().AssertTextContains(el, resultsCount.ToString());
        }

        [When(@"consumer is searching and clicking on 'Download and Print Documents - DocuSign Rooms'")]
        public void WhenConsumerIsSearchingAndClickingOn()
        {
            Driver.Page<SupportSearchResults>().ClickOnRoomsDocs();
        }

        [Then(@"thumbs up button from the 'Was this content helpful' section exists")]
        public void ThenThumbsUpButtonFromTheSectionExists()
        {
            Driver.Page<SupportSearchItem>().ConfirmThumbsUpExist();
        }
    }
}