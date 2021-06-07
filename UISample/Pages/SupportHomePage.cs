using OpenQA.Selenium;
using UISample.Extensions;

namespace UISample.Pages
{
    public class SupportHomePage : BasePage
    {
        public WrappedWebElement SearchInput => Find(By.Id("home-search-input"));
        public WrappedWebElement SearchButton => Find(By.Id("home-search-submit"));

        public void SearchFor(string keyword)
        {
            SearchInput.Clear();
            SearchInput.ScrollPageTo().WaitUntilClickable(Timeout).SendKeys(keyword);
            SearchButton.WaitUntilClickable(Timeout).Click();
            //WaitUtils.Waiter(2);
        }
    }
}