using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp
{
    class Program
    {

        IWebDriver driver;

        
        
        [SetUp]
        public void Launch() {
            driver = new ChromeDriver();
            Console.WriteLine("Browser Opened Successfully.");
            driver.Navigate().GoToUrl("http://demoqa.com");
            Console.WriteLine("Browser navigated to : "+driver.Title);
        }

        [Test]
        public void Execute() {

            driver.Navigate().GoToUrl("http://www.google.com");
            StringAssert.Contains("Google", driver.Title);
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Browserstack");
            query.Submit();

            
        }

        [TearDown]
        public void Close() {
            driver.Close();
            driver.Quit();
            }

    }
}
