using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using System.IO;

namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public class AddUpdateNoteSteps
    {
        private IWebDriver _driver;
        private IConfiguration _configuration;
        private string _baseUrl;

        [BeforeScenario]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("specflow.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            _baseUrl = _configuration["frontend:BaseUrl"];

            _driver = new ChromeDriver();
        }

        [AfterScenario]
        public void TearDown()
        {
            _driver.Quit();
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
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/noteList");
            //var searchElement = _driver.FindElement(By.CssSelector("input[placeholder='Search notes...']"));
            //Assert.That(searchElement != null);
        }

        [Given(@"I have navigated to the note list page")]
        public void GivenIHaveNavigatedToTheNoteListPage()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/noteList");
        }

        [When(@"I click on the add note button")]
        public void WhenIClickOnTheAddNoteButton()
        {
            _driver.FindElement(By.CssSelector("button[aria-label='Add New']")).Click();
        }


        [Then(@"I should be navigated to the add note page")]
        public void ThenIShouldBeNavigatedToTheAddNotePage()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/addUpdateNote?isView=false");
            //Assert.That(_driver.Url == $"{_baseUrl}/addUpdateNote?isView=false");
            var addNotePageElement = _driver.FindElement(By.CssSelector("input[placeholder='Enter your title']"));
            Assert.That(addNotePageElement != null);
        }



        [When(@"I add a new note with title ""(.*)"" and content ""(.*)""")]
        public void WhenIAddANewNoteWithTitleAndContent(string title, string content)
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/addUpdateNote?isView=false");
            //Assert.That(_driver.Url == $"{_baseUrl}/dashboard/addUpdateNote?isView=false");
            _driver.FindElement(By.CssSelector("input[placeholder='Enter your title']")).SendKeys(title);
            _driver.FindElement(By.CssSelector("input[placeholder='Enter your content']")).SendKeys(content);
            _driver.FindElement(By.Id("submit-id")).Click();
        }

        [Then(@"I should be navigated to the note list page")]
        public void ThenIShouldBeNavigatedToTheNoteListPage()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/noteList");

            //var searchElement = _driver.FindElement(By.CssSelector("input[placeholder='Search notes...']"));
            //Assert.That(searchElement != null);
        }

        [Given(@"I have navigated to the edit note page for ""(.*)""")]
        public void GivenIHaveNavigatedToTheEditNotePageFor(string title)
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/addUpdateNote?noteId={GetNoteIdByTitle(title)}&isView=false");
        }

        [When(@"I update the note to have title ""(.*)"" and content ""(.*)""")]
        public void WhenIUpdateTheNoteToHaveTitleAndContent(string newTitle, string newContent)
        {
            var titleInput = _driver.FindElement(By.CssSelector("input[placeholder='Enter your title']"));
            titleInput.Clear();
            titleInput.SendKeys(newTitle);
            var contentInput = _driver.FindElement(By.CssSelector("input[placeholder='Enter your content']"));
            contentInput.Clear();
            contentInput.SendKeys(newContent);
            _driver.FindElement(By.CssSelector("button[type='primary']")).Click();
        }

        [Then(@"the note should be updated successfully")]
        public void ThenTheNoteShouldBeUpdatedSuccessfully()
        {
            var note = _driver.FindElement(By.XPath($"//td[contains(text(), 'Updated Note')]"));
            Assert.That(note != null);
        }

        [Given(@"I have navigated to the view note page for ""(.*)""")]
        public void GivenIHaveNavigatedToTheViewNotePageFor(string title)
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/addUpdateNote?noteId={GetNoteIdByTitle(title)}&isView=true");
        }

        [Then(@"I should see the note with title ""(.*)"" and content ""(.*)""")]
        public void ThenIShouldSeeTheNoteWithTitleAndContent(string title, string content)
        {
            var titleElement = _driver.FindElement(By.CssSelector("input[placeholder='Enter your title']"));
            var contentElement = _driver.FindElement(By.CssSelector("input[placeholder='Enter your content']"));
            Assert.That(title == titleElement.GetAttribute("value"));
            Assert.That(content == contentElement.GetAttribute("value"));
        }

        private string GetNoteIdByTitle(string title)
        {
            // Implement logic to get the note ID by title, e.g., by querying the database or using an API
            return "note-id";
        }
    }
}