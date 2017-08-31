using System;
using System.Configuration;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using SeleniumCSharp.Utilities;

namespace SeleniumCSharp.SeleniumHelpers
{
    public enum DriverToUse
    {
        InternetExplorer,
        Chrome,
        Firefox
    }

    public class DriverFactory
    {
        private static readonly FirefoxProfile FirefoxProfile = CreateFirefoxProfile();

        private static FirefoxProfile CreateFirefoxProfile()
        {
            var firefoxProfile = new FirefoxProfile();
            firefoxProfile.SetPreference("network.automatic-ntlm-auth.trusted-uris", "http://localhost");
            return firefoxProfile;
        }

        public IWebDriver Create()
        {
            IWebDriver driver;
            var driverToUse = ConfigurationHelper.Get<DriverToUse>("DriverToUse");
            var browserStackIndicator = ConfigurationHelper.Get<bool>("UseBrowserStack");
            var url = ConfigurationHelper.Get<String>("TargetUrl");

            if (browserStackIndicator)
            {
                driver = CreateGridDriver(driverToUse);
            }
            else
            {
                switch (driverToUse)
                {
                    case DriverToUse.InternetExplorer:
                        driver = new InternetExplorerDriver(AppDomain.CurrentDomain.BaseDirectory, new InternetExplorerOptions(), TimeSpan.FromMinutes(5));
                        break;
                    case DriverToUse.Firefox:
                        driver = new FirefoxDriver();
                        break;
                    case DriverToUse.Chrome:
                        ChromeOptions options = new ChromeOptions();
                        options.AddArguments("--disable-extensions");
                        driver = new ChromeDriver(options);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            driver.Manage().Window.Maximize();
            var timeouts = driver.Manage().Timeouts();

            
            // Suppress the onbeforeunload event first. This prevents the application hanging on a dialog box that does not close.
            ((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);

            return driver;
        }

        public static IWebDriver CreateGridDriver(DriverToUse driverToUse)
        {
            var gridUrl = ConfigurationManager.AppSettings["GridUrl"];
            var desiredCapabilities = DesiredCapabilities.InternetExplorer();
            switch (driverToUse)
            {
                case DriverToUse.Firefox:
                    desiredCapabilities = DesiredCapabilities.Firefox();
                    break;
                case DriverToUse.InternetExplorer:
                    desiredCapabilities = DesiredCapabilities.InternetExplorer();
                    break;
                case DriverToUse.Chrome:
                    desiredCapabilities = DesiredCapabilities.Chrome();
                    break;
            }
            var browserStackUser = ConfigurationHelper.Get<String>("BrowserStackUser");
            var browserStackKey = ConfigurationHelper.Get<String>("BrowserStackKey");
            desiredCapabilities.SetCapability("browserstack.user", browserStackUser);
            desiredCapabilities.SetCapability("browserstack.key", browserStackKey);
            var remoteDriver = new RemoteWebDriver(new Uri(gridUrl), desiredCapabilities);
           // var nodeHost = remoteDriver.GetNodeHost();
            //Debug.WriteLine("Running tests on host " + nodeHost);
            return remoteDriver;
        }

        
    }
}