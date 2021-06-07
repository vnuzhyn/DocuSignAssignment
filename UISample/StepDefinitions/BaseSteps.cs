using System.Collections.Generic;
using System.Net.Http;
using TechTalk.SpecFlow;
using UISample.Driver;

namespace UISample.StepDefinitions
{
    public class BaseSteps
    {
		public FeatureContext FeatureContext;
        protected HttpResponseMessage HttpResponseMessage;
        protected Dictionary<string, string> LoanOfficerHeaders;
        protected ConfigReader ConfigReader;
        public ScenarioContext ScenarioContext;

        public BaseSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            FeatureContext = featureContext;
            ScenarioContext = scenarioContext;
            ConfigReader = new ConfigReader();
        }

        protected Navigator Driver => FeatureContext.Get<Navigator>("app");

        public static int BrowserTimeout => new ConfigReader().TimeOut;

	}
}