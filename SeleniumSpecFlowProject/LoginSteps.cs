using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SeleniumSpecFlowProject
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver _driver;

        [Given(@"I have navigated to the login page")]
        public void GivenIHaveNavigatedToTheLoginPage()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://localhost:5173/");
        }

        [When(@"I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            _driver.FindElement(By.Id("username-id")).SendKeys("test");
            _driver.FindElement(By.Id("password-id")).SendKeys("123456");
            _driver.FindElement(By.Id("login-id")).Click();
        }

        [Then(@"I should be logged in successfully")]
        public void ThenIShouldBeLoggedInSuccessfully()
        {
            _driver.Navigate().GoToUrl("http://localhost:5173/notelist");
            var loggedInElement = _driver.FindElement(By.Id("search-id"));
            Assert.That(loggedInElement != null);

            _driver.Quit();
        }

    }
}
