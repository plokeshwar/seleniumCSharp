using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumCSharp.SeleniumHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.Pages
{
    public class LoginPage : PageControl
    {
        private ExtentTest test;

        By USERNAME_FIELD = By.Id("userName");
        By PASSWORD_FIELD = By.Id("passWord");
        By LOGIN_BUTTON = By.XPath("//app-login//button");
        By USERNAME_ERROR = By.XPath("//input[@id='userName']/following-sibling::div//small");
        By PASSWORD_ERROR = By.XPath("//input[@id='passWord']/following-sibling::div//small");

        public LoginPage(IWebDriver driver, ExtentTest test) : base(driver)
        {
            this.test = test;
        }

        public void EnterUserName(String username) {
            EnterValue(USERNAME_FIELD, username);
            this.test.Info("Entered Username : "+username);
        }

        public void EnterPassword(String password) {
            EnterValue(PASSWORD_FIELD, password);
            this.test.Info("Entered Password : " + password);
        }

        public void ClickLogin() {
            Click(LOGIN_BUTTON);
            this.test.Info("Clicked on Login Button.");
        }

        public String GetUserNameError() {
            String tmp = GetText(USERNAME_ERROR);
            this.test.Info("Getting error message from username field. = "+tmp);
            return tmp;
        }

    }
}
