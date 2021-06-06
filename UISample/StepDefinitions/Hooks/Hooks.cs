using CMFG.DLX.Automation.UIFramework.Utilities;
using OpenQA.Selenium;
using System;
using System.Drawing;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;
using UISample.Driver;

namespace UISample.StepDefinitions.Hooks
{
	[Binding]
	internal class Hooks
	{
		private const string App = "app";
		protected static readonly ConfigReader EnvironmentReader = new ConfigReader();
        private static readonly string Url = EnvironmentReader.BaseUrl;
        private static readonly int TimeOut = EnvironmentReader.TimeOut;
        private static readonly string Browser = EnvironmentReader.Browser;
        private static readonly bool RunRemotely = EnvironmentReader.RunTestsRemotely;

		[BeforeFeature]
		public static void BeforeFeature(FeatureContext feature)
		{
			Console.WriteLine($"Starting feature: {feature.FeatureInfo.Title}");
		}

		[AfterFeature]
		public static void AfterFeature(FeatureContext feature)
		{
			Console.WriteLine($"Completed feature: {feature.FeatureInfo.Title}");
		}

		[BeforeScenario]
		public void BeforeScenario(FeatureContext feature, ScenarioContext scenario)
		{
			Console.WriteLine($"Starting scenario: {scenario.ScenarioInfo.Title}");

			if (!feature.ContainsKey(App))
			{
                LaunchBrowserAndNavigateToSupportPage(feature);
			}
		}

		[AfterScenario]
		public void AfterScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
		{
			Console.WriteLine($"Completed scenario: {scenarioContext.ScenarioInfo.Title}");

			var driver = featureContext.Get<Navigator>(App).Driver;

            featureContext.Remove(App);
			driver.Quit();
		}

		[AfterStep]
		public void AfterStep(FeatureContext feature, ScenarioContext scenario)
		{
			var driver = feature.Get<Navigator>(App).Driver;
			if (scenario.TestError != null)
			{
				ScreenshotUtil.TakeScreenshot(
					driver,
					feature.FeatureInfo.Title.ToIdentifier(),
					scenario.ScenarioInfo.Title.ToIdentifier(),
					$"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} \n{scenario.ScenarioInfo.Title} \n{scenario.TestError.Message}");
			}
		}

		private static Navigator LaunchBrowser()
		{

            IWebDriver driver = null;

			try
            {
                DriverFactory.Build(Browser, RunRemotely);

                driver.Manage().Window.Size = new Size(1920, 1080);
				

				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOut);
				driver.Navigate().GoToUrl(Url);
				driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(180);

				return new Navigator(driver, new Uri(Url));
			}
			catch (Exception)
			{
				driver?.Close();
				driver?.Quit();
				throw;
			}
		}

		private static void LaunchBrowserAndNavigateToSupportPage(FeatureContext feature)
		{
			var driver = LaunchBrowser();
			WaitUtils.WaitUntilPageIsLoaded(driver.Driver, TimeOut);
			feature.Add(App, driver);
        }
	}
}