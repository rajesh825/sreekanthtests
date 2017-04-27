using System;
using TestStack.Seleno.PageObjects.Locators;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.Extensions;
using OQAS = OpenQA.Selenium;
using OQASUI = OpenQA.Selenium.Support.UI;


namespace SourceBoxWebPages
{
    public class LoginPage : Page
    {

        private const string AccountNameID = "account";
        private const string UserNameID = "username";
        private const string PasswordID = "password";

        public void WaitForPageToLoad()
        {
            OQASUI.IWait<OQAS.IWebDriver> wait = new OQASUI.WebDriverWait(Host.Driver, TimeSpan.FromSeconds(60.00));
            wait.Until(d => ((OQAS.IJavaScriptExecutor)Host.Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public string AccountName
        {
            get
            {
                return Find.Element(By.Name(AccountNameID)).GetControlValueAs<String>();
            }
            set
            {
                Find.Element(By.Name(AccountNameID)).SendKeys(value);
            }
        }

        public string UserName
        {
            get
            {
                return Find.Element(By.Name(UserNameID)).GetControlValueAs<String>();
            }

            set
            {
                Find.Element(By.Name(UserNameID)).SendKeys(value);
            }

        }

        public string Password
        {
            get
            {
                return Find.Element(By.Name(PasswordID)).GetControlValueAs<String>();
            }

            set
            {
                Find.Element(By.Name(PasswordID)).SendKeys(value);
            }

        }


        public void PressEnter()
        {
            
            Find.Element(By.Name(PasswordID)).SendKeys(OQAS.Keys.Enter);
            //Find.Element(By.XPath("//input[@value='submit'][@type='submit']")).Click();
        }
    }
}
