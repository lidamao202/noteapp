using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SpecFlowTestProject
{
    [Binding]
    public class NoteSteps
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

        [Given(@"I am on the note creation page")]
        public void GivenIAmOnTheNoteCreationPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5173/notelist");
        }

        [When(@"I enter the note details")]
        public void WhenIEnterTheNoteDetails()
        {
            _driver.FindElement(By.Id("title")).SendKeys("Test Note");
            _driver.FindElement(By.Id("content")).SendKeys("This is a test note.");
        }

        [When(@"I click the save button")]
        public void WhenIClickTheSaveButton()
        {
            _driver.FindElement(By.Id("saveButton")).Click();
        }

        [Then(@"the note should be created successfully")]
        public void ThenTheNoteShouldBeCreatedSuccessfully()
        {
            var successMessage = _driver.FindElement(By.Id("successMessage")).Text;
            Assert.AreEqual("Note created successfully", successMessage);
        }

        [Given(@"I am on the notes page")]
        public void GivenIAmOnTheNotesPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5173/notelist");
        }

        [When(@"I request to view all notes")]
        public void WhenIRequestToViewAllNotes()
        {
            // Assuming the notes are loaded automatically on the page
        }

        [Then(@"I should see a list of all my notes")]
        public void ThenIShouldSeeAListOfAllMyNotes()
        {
            var notesList = _driver.FindElements(By.ClassName("note-item"));
            Assert.IsTrue(notesList.Count > 0);
        }

        [Given(@"I am on the note update page")]
        public void GivenIAmOnTheNoteUpdatePage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5173/notelist");
        }

        [When(@"I update the note details")]
        public void WhenIUpdateTheNoteDetails()
        {
            var titleField = _driver.FindElement(By.Id("title"));
            titleField.Clear();
            titleField.SendKeys("Updated Test Note");

            var contentField = _driver.FindElement(By.Id("content"));
            contentField.Clear();
            contentField.SendKeys("This is an updated test note.");
        }

        [When(@"I click the update button")]
        public void WhenIClickTheUpdateButton()
        {
            _driver.FindElement(By.Id("updateButton")).Click();
        }

        [Then(@"the note should be updated successfully")]
        public void ThenTheNoteShouldBeUpdatedSuccessfully()
        {
            var successMessage = _driver.FindElement(By.Id("successMessage")).Text;
            Assert.AreEqual("Note updated successfully", successMessage);
        }

        [When(@"I delete a note")]
        public void WhenIDeleteANote()
        {
            _driver.FindElement(By.Id("deleteButton_1")).Click();
        }

        [Then(@"the note should be deleted successfully")]
        public void ThenTheNoteShouldBeDeletedSuccessfully()
        {
            var successMessage = _driver.FindElement(By.Id("successMessage")).Text;
            Assert.AreEqual("Note deleted successfully", successMessage);
        }
    }
}
