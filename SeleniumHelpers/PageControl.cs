using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.SeleniumHelpers
{
    public class PageControl
    {
        IWebDriver driver;

        public PageControl(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Click(By by)
        {
            Console.WriteLine("Clicking on " + by.ToString());
            WaitForElement(by).Click();
        }

        public void EnterValue(By by, string value)
        {
            Console.WriteLine("Entering value into " + by.ToString());
            WaitForElement(by).SendKeys(value);
        }

        public string GetText(By by)
        {
            return WaitForElement(by).Text;
        }


        public string GetTitle()
        {
            string tmp = driver.Title;
            return tmp;
        }

        public IWebElement WaitForElement(By by)
        {
            IWebElement el = null;
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    if (driver.FindElement(by).Displayed && driver.FindElement(by).Enabled)
                        return driver.FindElement(by);
                }
                catch (NoSuchElementException e)
                {
                    Console.WriteLine("Waiting for Element : " + by.ToString());
                    Console.WriteLine(e.Message);
                    System.Threading.Thread.Sleep(200);
                }
            }
            return el;
        }

    }
}
