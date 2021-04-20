using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CMFG.DLX.Automation.UIFramework.Utilities
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