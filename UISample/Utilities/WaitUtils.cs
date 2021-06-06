using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using UISample;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CMFG.DLX.Automation.UIFramework.Utilities
{
    public static class WaitUtils
	{
		public static int defaultImplicitWait = 3;

		internal static IWait<IWebDriver> CreateWait(IWebDriver driver, TimeSpan timeout)
		{
			var provider = UIStartup.ServiceProvider.GetRequiredService<IWaitProvider>();
			return provider.CreateWait(driver, timeout);
		}

		public static void Waiter(int seconds)
		{
			Thread.Sleep(seconds * 1000);
		}

		public static void WaitUntilElementToBeClickable(IWebElement webElement, int timeout, IWebDriver webDriver)
		{
			IgnoreImplicitWait(
				webDriver,
				() =>
				{
					var wait = CreateWait(webDriver, TimeSpan.FromSeconds(timeout));
					wait.Until(ExpectedConditions.ElementToBeClickable(webElement));
				});
		}

		public static void WaitUntilElementToBeVisible(By elementLocator, int timeout, IWebDriver webDriver)
		{
			IgnoreImplicitWait(
				webDriver,
				() =>
				{
					var wait = CreateWait(webDriver, TimeSpan.FromSeconds(timeout));
					wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
				});
		}

		public static void WaitUntilPageIsLoaded(IWebDriver webDriver, int seconds)
		{
			webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(seconds);
		}

		public static TOutput IgnoreImplicitWait<TOutput>(IWebDriver webDriver, Func<TOutput> func)
		{
			var implicitWait = GetImplicitWait(webDriver);
			webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
			try
			{
				var result = func();
				webDriver.Manage().Timeouts().ImplicitWait = implicitWait;
				return result;
			}
			catch
			{
				webDriver.Manage().Timeouts().ImplicitWait = implicitWait;
				throw;
			}
		}

		public static void IgnoreImplicitWait(IWebDriver webDriver, Action action)
		{
			IgnoreImplicitWait(
				webDriver,
				() =>
				{
					action();
					return true;
				});
		}

		public static TimeSpan GetImplicitWait(IWebDriver webDriver)
		{
			try
			{
				return webDriver.Manage().Timeouts().ImplicitWait;
			}
			catch (NotImplementedException)
			{
				// Some web drivers (like BrowserStack) do not allow you to read the implicit wait
				// If that's the case, then return a default wait value
				return TimeSpan.FromSeconds(defaultImplicitWait);
			}
		}
	}
}