using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using TechTalk.SpecFlow;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow.Assist;

namespace SeleniumSpecFlowProject.StepDefinitions
{
    [Binding]
    [Scope(Feature = "NoteList")]
    public class NoteListSteps: BaseStep
    {
        public NoteListSteps(IWebDriver driver, ScenarioContext scenarioContext):base(driver, scenarioContext)
        {
        }

        [Given(@"Set fields on view")]
        public void GivenSetFieldsOnView(Table table)
        {
            foreach (var row in table.Rows)
            {
                var title = row["Title"];
                var content = row["Content"];
                AddNoteToTable(title, content);
                break;
            }

        }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            GivenIHaveNavigatedToTheLoginPage();
            WhenIEnterValidCredentials();
            ThenIShouldBeLoggedInSuccessfully();
        }



        private void AddNoteToTable(string title, string content)
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/addUpdateNote?isView=false");
            _driver.FindElement(By.CssSelector("input[placeholder='Enter your title']")).SendKeys(title);
            _driver.FindElement(By.CssSelector("input[placeholder='Enter your content']")).SendKeys(content);
            _driver.FindElement(By.Id("submit-id")).Click();
        }

        [When(@"I navigate to the note list page")]
        public void WhenINavigateToTheNoteListPage()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/noteList");
            //Task.Delay(2000).Wait();
        }

        [Then(@"I should see the note list")]
        public void ThenIShouldSeeTheNoteList()
        {
            Assert.That(_driver.FindElement(By.Id("noteListTable")).Displayed);
        }

        [Then(@"I should see the note with the title ""(.*)"" in the notes list")]
        public void ThenIShouldSeeTheNoteWithTheTitleInTheNotesList(string title)
        {
            var noteTitle = _driver.FindElement(By.XPath($"//td[contains(text(), '{title}')]"));
            Assert.That(noteTitle != null);
        }
    }
}

