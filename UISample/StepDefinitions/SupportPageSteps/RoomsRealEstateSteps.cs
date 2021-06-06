using TechTalk.SpecFlow;

namespace UISample.StepDefinitions.SupportPageSteps
{
    public class RoomsRealEstateSteps : BaseSteps
    {
        [Given(@"consumer is at the support home page")]
        public void GivenConsumerIsAtTheSupportHomePage()
        {
            //hooks
        }

        [When(@"consumer is searching for '(.*)'")]
        public void WhenConsumerIsSearchingFor(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"search response contains (.*) total results")]
        public void WhenSearchResponseContainsTotalResults(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"consumer is searching and clicking on '(.*)'")]
        public void WhenConsumerIsSearchingAndClickingOn(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"thumbs up button from the '(.*)' section exists")]
        public void ThenThumbsUpButtonFromTheSectionExists(string p0)
        {
            ScenarioContext.Current.Pending();
        }

    }
}