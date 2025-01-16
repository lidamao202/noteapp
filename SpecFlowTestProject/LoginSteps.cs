using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using NUnit;

namespace SpecFlowTestProject;

[Binding]
public class LoginSteps
{
    private IWebDriver _driver;

    [BeforeScenario]
    public void Setup()
    {
        _driver = new ChromeDriver();
    }

    [AfterScenario]
    public void TearDown()
    {
        _driver.Quit();
    }

    [Given(@"I am on the login page")]
    public void GivenIAmOnTheLoginPage()
    {
        _driver.Navigate().GoToUrl("http://localhost:5173/");
    }

    [When(@"I enter valid credentials")]
    public void WhenIEnterValidCredentials()
    {
        _driver.FindElement(By.Id("username")).SendKeys("testuser");
        _driver.FindElement(By.Id("password")).SendKeys("testpassword");
    }

    [When(@"I click the login button")]
    public void WhenIClickTheLoginButton()
    {
        _driver.FindElement(By.Id("loginButton")).Click();
    }

    [Then(@"I should be redirected to the dashboard")]
    public void ThenIShouldBeRedirectedToTheDashboard()
    {
        var dashboardUrl = "http://localhost:5173/";
        Assert.AreEqual(dashboardUrl, _driver.Url);
    }
}
