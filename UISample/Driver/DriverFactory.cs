using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using UISample.Chrome;
using UISample.Utilities;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace UISample.Driver
{
    public static class DriverFactory
	{
		public static IWebDriver Build(string browser, bool runTestsRemotely, string driverVersion = "Latest")
		{
			IWebDriver webDriver;

			const string nodeURI = "http://23.100.236.70:4446/wd/hub";

			switch (browser.ToLower())
			{
				case "chrome":
					var chromeOptions = new ChromeOptions();
                    var chromeBrowser = new ChromeBrowser();
                    chromeBrowser.StartIfNotRunning();
                    chromeOptions.DebuggerAddress = $"localhost:{chromeBrowser.DebuggingPort}";

                    chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
					new DriverManager().SetUpDriver(new ChromeConfig(), driverVersion);
					webDriver = runTestsRemotely ? new RemoteWebDriver(new Uri(nodeURI), chromeOptions) : new ChromeDriver(chromeOptions);
					break;

				case "chrome-incognito-ignore-certificate":
					var chromeIncogAndIgnCertErrsOptions = new ChromeOptions();
					chromeIncogAndIgnCertErrsOptions.AddArguments("--incognito", "--ignore-certificate-errors");
					chromeIncogAndIgnCertErrsOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
					new DriverManager().SetUpDriver(new ChromeConfig(), driverVersion);
					webDriver = runTestsRemotely ? new RemoteWebDriver(new Uri(nodeURI), chromeIncogAndIgnCertErrsOptions) : new ChromeDriver(chromeIncogAndIgnCertErrsOptions);
					break;

				case "chrome-headless":
					var chromeHeadlessOptions = new ChromeOptions();
					chromeHeadlessOptions.AddArgument("--headless");
					chromeHeadlessOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
					new DriverManager().SetUpDriver(new ChromeConfig(), driverVersion);
					webDriver = runTestsRemotely ? new RemoteWebDriver(new Uri(nodeURI), chromeHeadlessOptions) : new ChromeDriver(chromeHeadlessOptions);
					break;

				case "firefox":
					var firefoxOptions = new FirefoxOptions();
					firefoxOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
					new DriverManager().SetUpDriver(new FirefoxConfig(), driverVersion);
					webDriver = runTestsRemotely ? new RemoteWebDriver(new Uri(nodeURI), firefoxOptions) : new FirefoxDriver(firefoxOptions);
					break;

				case "edge":
					var edgeOptions = new EdgeOptions();
					edgeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
					new DriverManager().SetUpDriver(new EdgeConfig(), driverVersion);
					webDriver = runTestsRemotely ? new RemoteWebDriver(new Uri(nodeURI), edgeOptions) : new EdgeDriver(edgeOptions);
					break;

				case "safari":
					var safariOptions = new SafariOptions();
					safariOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
					webDriver = runTestsRemotely ? new RemoteWebDriver(new Uri(nodeURI), safariOptions) : new SafariDriver(safariOptions);
					break;

				default:
					throw new ApplicationException($"The functionality to run scripts for {browser} driver has not been implemented");
			}

			webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(WaitUtils.defaultImplicitWait);
			webDriver.Manage().Window.Maximize();
			return webDriver;
		}
    }
}