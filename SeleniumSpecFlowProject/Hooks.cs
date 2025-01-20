using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumSpecFlowProject
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = new ChromeDriver();
            _objectContainer.RegisterInstanceAs(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }
    }
}
