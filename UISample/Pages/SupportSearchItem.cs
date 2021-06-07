using OpenQA.Selenium;
using UISample.Extensions;
using Xunit;

namespace UISample.Pages
{
    public class SupportSearchItem : BasePage
    {
        public WrappedWebElement ThumbsUp => Find(By.Id("vote-up"));

        public void ConfirmThumbsUpExist()
        {
            ThumbsUp.ScrollPageTo();
            ThumbsUp.WaitUntilVisible(Timeout);
            Assert.True(ThumbsUp.Displayed);
        }
    }
}