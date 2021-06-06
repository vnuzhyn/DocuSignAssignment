using CMFG.DLX.Automation.UIFramework.Utilities;
using OpenQA.Selenium;
using UISample.Extensions;

namespace UISample.Pages
{
    public class SupportPage : BasePage
    {
        public WrappedWebElement SearchInput => Find(By.Id("home-search-input"));
        public WrappedWebElement SearchButton => Find(By.Id("home-search-input"));

        public void SearchForConsumer(string keyword)
        {
            SearchInput.Clear();
            SearchInput.ScrollPageTo().WaitUntilClickable(Timeout).SendKeys(keyword);
            SearchButton.WaitUntilClickable(Timeout).Click();
            WaitUtils.Waiter(2);
        }
    }
}