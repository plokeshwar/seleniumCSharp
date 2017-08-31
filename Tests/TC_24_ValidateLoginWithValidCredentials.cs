using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumCSharp.Pages;
using SeleniumCSharp.SeleniumHelpers;
using SeleniumCSharp.TestRailApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.Tests
{
    [TestFixture]
    class TC_24_ValidateLoginWithValidCredentials : BeforeTestAfterTest
    {
        private String testcaseID = "24";
        private static IWebDriver driver;
        private static ExtentTest test;
        LoginPage loginPage;
       
        [SetUp]
        public void InitBrowser()
        {
           updateTestRun(testcaseID);
           driver = new DriverFactory().Create();
            
        }

        [Test]
        public void ValidateLoginWithValidCredentials()
        {
            String expected = "Unity - Game Engine";
            test = extent.CreateTest("Validate Login With Valid Credentials", 
                "When user enters valid credentials, user should navigate to home page.");
            loginPage = new LoginPage(driver, test);

            try
            {
                loginPage.EnterUserName("pravin");
                loginPage.EnterPassword("pravin");
                loginPage.ClickLogin();
                String actual = loginPage.GetTitle();

                test.AddScreenCaptureFromPath(TakeScreenshot(driver));
                Assert.AreEqual(expected, actual, "Page Navigated to Home Page.");
                addTestCaseStatus("Pass", testcaseID, "Test Case Passed"); 
                test.Pass("User navigated to home page.");
            }
            catch (AssertionException)
            {
                addTestCaseStatus("Fail", testcaseID, "Test Case Failed");
                test.Fail("User did navigate to home page.");
                throw;
            }
        }

        [TearDown]
        public void CloseBrowser()
        {

            driver.Quit();
        }

    }
}
