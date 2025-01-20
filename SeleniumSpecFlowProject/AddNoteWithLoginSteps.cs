using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace SeleniumSpecFlowProject
{
    [Binding]
    public class AddNoteWithLoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private IConfiguration _configuration;
        private string _baseUrl;
        private ScenarioContext _scenarioContext;

        public AddNoteWithLoginSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("specflow.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            _baseUrl = _configuration["frontend:BaseUrl"];

            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have navigated to the login page")]
        public void GivenIHaveNavigatedToTheLoginPage()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/");
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
            _driver.ExecuteJavaScript("return localStorage.getItem('jwt_token');");

            Assert.That(_driver.FindElement(By.Id("login-id")).Displayed);
        }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            GivenIHaveNavigatedToTheLoginPage();
            WhenIEnterValidCredentials();
            ThenIShouldBeLoggedInSuccessfully();
        }

        [When(@"I navigate to the add note page")]
        public void WhenINavigateToTheNotesPage()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/addUpdateNote");
            Task.Delay(5000).Wait();
        }

        [When(@"I add a new note with title ""(.*)"" and content ""(.*)""")]
        public void WhenIAddANewNoteWithTitleAndContent(string title, string content)
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/addUpdateNote?isView=false");
            _driver.FindElement(By.CssSelector("input[placeholder='Enter your title']")).SendKeys(title);
            _driver.FindElement(By.CssSelector("input[placeholder='Enter your content']")).SendKeys(content);
            _driver.FindElement(By.Id("submit-id")).Click();

            // Store the note details in the scenario context
            _scenarioContext["NoteTitle"] = title;
            _scenarioContext["NoteContent"] = content;
        }

        [Then(@"I should be navigated to the note list page")]
        public void ThenIShouldBeNavigatedToTheNoteListPage()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/noteList");
        }

        [Then(@"I should see the note with the title ""(.*)"" in the notes list")]
        public void ThenIShouldSeeTheNoteWithTheTitleInTheNotesList(string title)
        {
            Assert.That(_scenarioContext["NoteTitle"], Is.EqualTo(title));
        }
    }
}
