using OpenQA.Selenium;
using SeleniumCSharp.TestRailApis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.Utilities
{
    public class CommonMethods : TestRailMethods
    {

        public String TakeScreenshot(IWebDriver driver)
        {
            String path = getCurrentProjectPath() + "/Screenshot";
            createDirectory(path);

            StringBuilder TimeAndDate = new StringBuilder(DateTime.Now.ToString());
            TimeAndDate.Replace("/", "_");
            TimeAndDate.Replace(":", "_");
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(path+"/screenshot-" + TimeAndDate + ".jpeg", ScreenshotImageFormat.Jpeg);
            return path +"/screenshot-" + TimeAndDate + ".jpeg";
        }

        public static string getCurrentProjectPath()
        {
            String path = System.AppDomain.CurrentDomain.BaseDirectory;
            path = path.Substring(0, path.IndexOf(@"\bin"));
            return path;
        }

        public void createDirectory(String path) {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
