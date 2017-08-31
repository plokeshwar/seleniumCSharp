using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
    class TC_26_ValidateFailedCase : BeforeTestAfterTest
    {
        private String testcaseID = "26";
        private static IWebDriver driver;
        private static ExtentTest test;
        
       



        [SetUp]
        public void InitBrowser()
        {
            updateTestRun(testcaseID);
            driver = new DriverFactory().Create();
        }

        [Test]
        public void ValidFailedLogin()
        {
            test = extent.CreateTest("Validate Failed Login");
            try
            {
                test.AddScreenCaptureFromPath(TakeScreenshot(driver));
                Assert.IsTrue(false);
                addTestCaseStatus( "Pass", testcaseID, "Test Case Passed"); 
                test.Pass("Assertion passed");
                
                
            }
            catch (AssertionException)
            {
                addTestCaseStatus( "Fail", testcaseID, "Test Case Failed");
                test.Fail("Assertion failed");
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
