using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using CMFG.DLX.Automation.UIFramework.Utilities;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using SeleniumExtras.WaitHelpers;

namespace CMFG.DLX.Automation.UIFramework
{
	public class WrappedWebElement : IWebElement, IWrapsElement
	{
		private readonly Lazy<IWebElement> _element;
		private readonly ILogger _logger;

		public readonly By By;
		public readonly IWebDriver WrappedDriver;

		public WrappedWebElement(ILogger logger, IWebDriver driver, By by, string pageName, string elementName)
		{
			_logger = logger;
			By = by;
			WrappedDriver = driver;
			PageName = pageName;
			ElementName = elementName;
			_element = new Lazy<IWebElement>(() => driver.FindElement(By));
		}

		public string PageName { get; }

		public string ElementName { get; }

		private IWebElement Element
		{
			get
			{
				try
				{
					return _element.Value;
				}
				catch (NoSuchElementException inner)
				{
					throw new NoSuchElementException($"Unable to locate element {ElementName} on {PageName}", inner);
				}
			}
		}

		public string TagName
		{
			get
			{
				LogDebug();
				return Element.TagName;
			}
		}

		public string Text
		{
			get
			{
				LogDebug();
				return Element.Text;
			}
		}

		public bool Enabled
		{
			get
			{
				LogDebug();
				return Element.Enabled;
			}
		}

		public bool Selected
		{
			get
			{
				LogDebug();
				return Element.Selected;
			}
		}

		public Point Location
		{
			get
			{
				LogDebug();
				return Element.Location;
			}
		}

		public Size Size
		{
			get
			{
				LogDebug();
				return Element.Size;
			}
		}

		public bool Displayed
		{
			get
			{
				LogDebug();
				return Element.Displayed;
			}
		}

		public void Clear()
		{
			LogDebug();
			Element.Clear();
		}

		public void Click()
		{
			LogDebug();
			Element.Click();
		}

		public IWebElement FindElement(By by)
		{
			LogDebug();
			return Element.FindElement(by);
		}

		public ReadOnlyCollection<IWebElement> FindElements(By by)
		{
			LogDebug();
			return Element.FindElements(by);
		}

		public string GetAttribute(string attributeName)
		{
			LogDebug();
			return Element.GetAttribute(attributeName);
		}

		public string GetCssValue(string propertyName)
		{
			LogDebug();
			return Element.GetCssValue(propertyName);
		}

		public string GetProperty(string propertyName)
		{
			LogDebug();
			return Element.GetProperty(propertyName);
		}

		public void SendKeys(string text)
		{
			LogDebug();
			Element.SendKeys(text);
		}

		public void Submit()
		{
			LogDebug();
			Element.Submit();
		}

		public IWebElement WrappedElement => Element;

		public bool Exists()
		{
			return TryWaitUntilExists(0);
		}

		public bool TryWaitUntilExists(int timeout)
		{
			try
			{
				WaitUntilExists(timeout);
				return true;
			}
			catch (WebDriverTimeoutException)
			{
				return false;
			}
		}

		public WrappedWebElement WaitUntilExists(int timeout)
		{
			var wait = WaitUtils.CreateWait(WrappedDriver, TimeSpan.FromSeconds(timeout));
			WaitUtils.IgnoreImplicitWait(WrappedDriver, () => { wait.Until(ExpectedConditions.ElementExists(By)); });
			return this;
		}

		public WrappedWebElement WaitUntilVisible(int timeout)
		{
			WaitUtils.WaitUntilElementToBeVisible(By, timeout, WrappedDriver);
			return this;
		}

		private void LogDebug([CallerMemberName] string callerName = "")
		{
			_logger?.LogDebug($"{PageName}, {ElementName}: {callerName}");
		}
	}
}