using OpenQA.Selenium;
using UISample.Extensions;
using UISample.Utilities;
using Xunit;

namespace UISample.Pages
{
    public class SupportSearchItem : BasePage
    {
        public WrappedWebElement ThumbsUp => Find(By.Id("vote-up"));

        public void ConfirmThumbsUpExist()
        {
            WaitUtils.WaitUntilPageIsLoaded(Driver, Timeout);
            ThumbsUp.ScrollPageTo();
            Assert.True(ThumbsUp.Displayed);
        }
    }
}