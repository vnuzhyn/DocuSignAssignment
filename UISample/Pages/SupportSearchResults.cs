using OpenQA.Selenium;
using UISample.Extensions;
using UISample.Utilities;
using Xunit;

namespace UISample.Pages
{
    public class SupportSearchResults : BasePage
    {
        public WrappedWebElement SearchResultInfo => Find(By.Id("inbenta-total-results"));
        public WrappedWebElement ShowMoreButton => Find(By.Id("show-more-button"));
        public WrappedWebElement RoomsDocs => Find(By.XPath("//span[contains(text(), 'Download and Print Documents - DocuSign Rooms')]"));

        public void AssertTextContains(IWebElement element, string expectedSubstring)
        {
            string text = element.Text;
            Assert.Contains(expectedSubstring, text);
        }

        public void ClickOnRoomsDocs()
        {
            ScrollAndShowMore();
            ScrollAndShowMore();
            RoomsDocs.WaitUntilClickable(Timeout).Click();
        }

        public void ScrollAndShowMore()
        {
            WaitUtils.Waiter(2);
            ShowMoreButton.ScrollPageTo().Click();
        }
    }
}