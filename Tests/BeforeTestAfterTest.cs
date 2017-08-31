using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using SeleniumCSharp.TestRailApis;
using SeleniumCSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.Tests
{
   public class BeforeTestAfterTest : CommonMethods
    {
        public static ExtentReports extent;
        public static ExtentHtmlReporter htmlReporter;
        public static String runID;
        private static String projectID = "8";

        [OneTimeSetUp]
        public void SetupReporting()
        {
            if (htmlReporter == null)
            {

                htmlReporter = ExtentReporter.getHtmlReport();
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
                
            }

            if(runID == null || runID == "")
            {
                runID = createTestRun(projectID);
            }

           

        }

        [OneTimeTearDown]
        public void GenerateReport()
        {
            extent.Flush();
        }

    }
}
