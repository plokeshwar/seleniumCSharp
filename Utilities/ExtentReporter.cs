using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.Utilities
{
    public class ExtentReporter
    {

        public static ExtentHtmlReporter getHtmlReport() {

            String path = getCurrentProjectPath() + "/Reports";
            new CommonMethods().createDirectory(path);

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path+"/Automation-"+getTimeStamp()+".html");
            htmlReporter.Configuration().Theme = Theme.Dark;
            htmlReporter.Configuration().DocumentTitle = "DocWorks Testing Report";
            htmlReporter.Configuration().ReportName = "DocWorks Regression Testing Report";
            return htmlReporter;
        }

        public static string getCurrentProjectPath() {
           String path = System.AppDomain.CurrentDomain.BaseDirectory;
            path = path.Substring(0, path.IndexOf(@"\bin"));
            return path;
        }

        public static string getTimeStamp() {
            StringBuilder TimeAndDate = new StringBuilder(DateTime.Now.ToString());
            TimeAndDate.Replace("/", "_");
            TimeAndDate.Replace(":", "_");
            return TimeAndDate.ToString();
        }

    }
}
