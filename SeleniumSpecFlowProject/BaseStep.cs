using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.Extensions;

namespace SeleniumSpecFlowProject
{

    public class BaseStep
    {
        public readonly IWebDriver _driver;
        public readonly WebDriverWait _wait;
        public IConfiguration _configuration;
        public string _baseUrl;
        public ScenarioContext _scenarioContext;


        public BaseStep(IWebDriver driver, ScenarioContext scenarioContext)
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

        public void GivenIHaveNavigatedToTheLoginPage()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/");
        }

        public void WhenIEnterValidCredentials()
        {
            _driver.FindElement(By.Id("username-id")).SendKeys("test");
            _driver.FindElement(By.Id("password-id")).SendKeys("123456");
            _driver.FindElement(By.Id("login-id")).Click();
            _driver.Navigate().GoToUrl($"{_baseUrl}/dashboard/noteList");
        }

        public void ThenIShouldBeLoggedInSuccessfully()
        {
            _driver.ExecuteJavaScript("return localStorage.getItem('jwt_token');");

            Assert.That(_driver.Url == $"{_baseUrl}/dashboard/noteList");
        }
    }
}
