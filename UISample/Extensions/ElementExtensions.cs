using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Xunit;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UISample.Extensions
{
	public static class ElementExtensions
	{
		public static bool ContinueOnFailure =>
			UIStartup.ServiceProvider.GetRequiredService<IDebugSettings>().ContinueOnFailure;

		private static IWebDriver GetDriver(IWebElement element)
		{
			if (element is WrappedWebElement webElement)
			{
				return webElement.WrappedDriver;
			}

			var remoteWebElement = (RemoteWebElement) element;
			return remoteWebElement.WrappedDriver;
		}

		private static IJavaScriptExecutor GetJavaScriptExecutor(IWebElement element)
		{
			var driver = GetDriver(element);
			return (IJavaScriptExecutor) driver;
		}

		public static IWebElement ScrollPageTo(this IWebElement element)
		{
			var jse = GetJavaScriptExecutor(element);
			jse.ExecuteScript("arguments[0].scrollIntoView({block:'center'})", element);
			return element;
		}

		public static IWebElement ClearAndEnterText(this IWebElement element, string text)
		{
			element.SendKeys(Keys.Control + "a");
			element.SendKeys(Keys.Delete);
			element.SendKeys(text);
			return element;
		}

		public static void CheckOrUncheckCheckBoxOrToggle(this IWebElement element, bool checkBoxNeedsToBeChecked)
		{
			var checkBoxIsOn = element.Selected;
			if (checkBoxNeedsToBeChecked && !checkBoxIsOn)
			{
				element.ScrollPageTo().Click();
			}
			else if (!checkBoxNeedsToBeChecked && checkBoxIsOn)
			{
				element.ScrollPageTo().Click();
			}
		}

		public static void SelectRadioButton(this IWebElement element, bool radioButtonNeedsToBeSelected)
		{
			if (radioButtonNeedsToBeSelected)
			{
				element.Click();
			}
		}

		public static void AssertTextEqual(this IWebElement element, string expected)
		{
			try
			{
				var actual = element.Text;
				Assert.Equal(expected, actual);
			}
			catch (Exception e)
			{
				if (!ContinueOnFailure)
				{
					throw;
				}

				HandleContinueOnFailure(element, e);
			}
		}

		public static void AssertTextContains(this IWebElement element, string expectedSubstring)
		{
			try
			{
				var actualString = element.Text;
				Assert.Contains(expectedSubstring, actualString);
			}
			catch (Exception e)
			{
				if (!ContinueOnFailure)
				{
					throw;
				}

				HandleContinueOnFailure(element, e);
			}
		}

		public static void AssertTrue(this IWebElement element, Func<IWebElement, bool> condition)
		{
			try
			{
				Assert.True(condition(element));
			}
			catch (Exception e)
			{
				if (!ContinueOnFailure)
				{
					throw;
				}

				HandleContinueOnFailure(element, e);
			}
		}

		public static void HandleContinueOnFailure(IWebElement element, Exception e)
		{
			var logger = UIStartup.GetLogger<IWebElement>();
			var logStart = "";
			if (element is WrappedWebElement webElement)
			{
				logStart = $"{webElement.PageName}, {webElement.ElementName}: ";
			}

			logger?.LogError(e, @$"{logStart}Exception occurred during assertion, continuing...");
		}

		public static IWebElement WaitUntilClickable(this IWebElement element, int timeout)
		{
			var driver = GetDriver(element);
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
			wait.Until(ExpectedConditions.ElementToBeClickable(element));
			return element;
		}

        public static IWebElement Highlight(this IWebElement element, string borderWidth = "1px", string borderStyle = "solid", string color = "green")
		{
			var jsDriver = GetJavaScriptExecutor(element);
			var highlightJavascript = $@"arguments[0].style.cssText = ""border-width: {borderWidth}; border-style: {borderStyle}; border-color: {color}"";";
			jsDriver.ExecuteScript(highlightJavascript, element);
			return element;
		}

		public static IWebElement Hover(this IWebElement element)
		{
			var driver = GetDriver(element);
			var actions = new Actions(driver);
			actions.MoveToElement(element).Perform();
			return element;
		}
	}
}