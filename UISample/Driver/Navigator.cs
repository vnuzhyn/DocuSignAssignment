using System;
using OpenQA.Selenium;
using UISample.Pages;

namespace UISample.Driver
{
	public class Navigator
	{
		public readonly ExceptionLogger ExceptionLogger = new ExceptionLogger();

		/// <param name="driver">
		///     <see cref="IWebDriver" /> Web driver
		/// </param>
		/// <param name="baseUrl">
		///     <see cref="Url" /> Base Url, should be taken from config
		/// </param>
		public Navigator(IWebDriver driver, Uri baseUrl)
		{
			Driver = driver;
			BasePage.Url = baseUrl;
		}

		public IWebDriver Driver { get; }

		public TComponent Page<TComponent>()
			where TComponent : BasePage, new()
		{
			var page = new TComponent();
			page.Init(this);
			return page;
		}
	}
}