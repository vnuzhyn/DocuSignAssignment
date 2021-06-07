using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UISample.Utilities
{
	public interface IWaitProvider
	{
		public IWait<IWebDriver> CreateWait(IWebDriver driver, TimeSpan timeout);
	}

	public class DefaultWaitProvider : IWaitProvider
	{
		public IWait<IWebDriver> CreateWait(IWebDriver driver, TimeSpan timeout)
		{
			return new WebDriverWait(driver, timeout);
		}
	}
}