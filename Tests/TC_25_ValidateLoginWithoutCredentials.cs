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
    class TC_25_ValidateLoginWithoutCredentials : BeforeTestAfterTest
    {
        private String testcaseID = "25";
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
        public void ValidateLoginWithoutCredentials()
        {
            String expected = "Please enter at least 5 characters.";
            test = extent.CreateTest("Validate Login Without Credentials", 
                "When user enters no credentials, user should be displayed with error message.");
            loginPage = new LoginPage(driver, test);

            try
            {
                loginPage.EnterUserName("p");

                String actual = loginPage.GetUserNameError();

                test.AddScreenCaptureFromPath(TakeScreenshot(driver));
                Assert.AreEqual(expected, actual, "User displayed with error message.");
                addTestCaseStatus("Pass", testcaseID, "Test Case Passed"); 
                test.Pass("User displayed with error message.");
            }
            catch (AssertionException)
            {
                addTestCaseStatus("Fail", testcaseID, "Test Case Failed");
                test.Fail("User is not displayed with error message.");
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
