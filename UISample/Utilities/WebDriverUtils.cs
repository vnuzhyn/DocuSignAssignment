using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CMFG.DLX.Automation.UIFramework.Utilities
{
	public static class WebDriverUtils
	{
		public static void Hover(IWebElement webElement, IWebDriver webDriver)
		{
			var actions = new Actions(webDriver);
			actions.MoveToElement(webElement).Perform();
		}

		public static void HighlightElement(IWebElement webElement, IWebDriver webDriver, string color = "green")
		{
			var jsDriver = (IJavaScriptExecutor) webDriver;
			var highlightJavascript = $@"arguments[0].style.cssText = ""border-width: 1px; border-style: solid; border-color: {color}"";";
			jsDriver.ExecuteScript(highlightJavascript, webElement);
		}

		public static void ClickOnCertainCoordinates(int x, int y, IWebDriver webDriver)
		{
			var actions = new Actions(webDriver);
			actions.MoveByOffset(x, y).Click().Perform();
		}

		public static IWebElement FindElementIfExists(IWebDriver driver, By by)
		{
			var webElements = driver.FindElements(by);
			return webElements.Count >= 1 ? webElements.First() : null;
		}
	}
}