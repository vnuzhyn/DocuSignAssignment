using System;
using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UISample.Driver;
using UISample.Extensions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UISample.Pages
{
	public class BasePage
	{
		public static Uri Url;
		protected IWebDriver Driver;
		protected Navigator App { get; set; }
        public static readonly int Timeout = new ConfigReader().TimeOut;

		public void Init(Navigator app)
		{
			App = app;
			Driver = app.Driver;
		}

		protected IWebElement WaitForElementPresence(By locator)
		{
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
			return element;
		}

		public void ScrollPageToElement(IWebElement webElement)
		{
			webElement.ScrollPageTo();
		}

		protected void ScrollPageToElementAndClick(IWebElement webElement)
		{
			webElement.ScrollPageTo().Click();
		}

		protected void ScrollPageToElementClearAndEnterText(IWebElement webElement, string text)
		{
			webElement.ScrollPageTo().ClearAndEnterText(text);
		}

		protected string ScrollPageToElementGetText(IWebElement webElement)
		{
			return webElement.ScrollPageTo().Text;
		}

		protected void ClearAndEnterText(IWebElement element, string text)
		{
			element.ClearAndEnterText(text);
		}

		protected void CheckOrUncheckCheckBoxOrToggle(IWebElement checkBoxWebElement, bool checkBoxNeedsToBeChecked)
		{
			checkBoxWebElement.CheckOrUncheckCheckBoxOrToggle(checkBoxNeedsToBeChecked);
		}

		protected void SelectRadioButton(IWebElement radioButtonWebElement, bool radioButtonNeedsToBeSelected)
		{
			radioButtonWebElement.SelectRadioButton(radioButtonNeedsToBeSelected);
		}

		protected bool NoSuchElement(By locatorKey)
		{
			try
			{
				Driver.FindElement(locatorKey);
				return false;
			}
			catch (NoSuchElementException e)
			{
				return true;
			}
		}

		public void AssertTextEqual(By by, string expected)
		{
			AssertTextEqual(Find(by), expected);
		}

		public void AssertTextEqual(IWebElement element, string expected)
		{
			element.AssertTextEqual(expected);
		}

		public WrappedWebElement Find(By by, [CallerMemberName] string elementName = "")
		{
			var pageName = GetType().Name;
			var logger = UIStartup.GetLogger<IWebElement>();
			return new WrappedWebElement(logger, Driver, by, pageName, elementName);
		}
	}
}