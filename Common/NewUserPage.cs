using System;
using TestStack.Seleno.PageObjects.Locators;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.Extensions;
using OQAS = OpenQA.Selenium;
using OQASUI = OpenQA.Selenium.Support.UI;


namespace SourceBoxWebPages
{
    public class NewUserPage : Page
    {

        private const string UserNameID = "username";
        private const string PasswordID = "passwd";
        private const string PasswordRetypeID = "passwd_retype";
        private const string FullNameID = "fullname";
        private const string BranchID = "branch";
        private const string EmailID = "email";
        private const string MonsterCATID = "monstercat";

        public void WaitForPageToLoad()
        {
            OQASUI.IWait<OQAS.IWebDriver> wait = new OQASUI.WebDriverWait(Host.Driver, TimeSpan.FromSeconds(60.00));
            wait.Until(d => ((OQAS.IJavaScriptExecutor)Host.Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public string FullName
        {
            get
            {
                return Find.Element(By.Name(FullNameID)).GetControlValueAs<String>();
            }
            set
            {
                Find.Element(By.Name(FullNameID)).SendKeys(value);
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

        public string Branch
        {
            get
            {
                return Find.Element(By.Name(BranchID)).GetControlValueAs<String>();
            }

            set
            {
                Find.Element(By.Name(BranchID)).SendKeys(value);
            }

        }


        public string Email
        {
            get
            {
                return Find.Element(By.Name(EmailID)).GetControlValueAs<String>();
            }

            set
            {
                Find.Element(By.Name(EmailID)).SendKeys(value);
            }

        }

        public string MonsterCAT
        {
            get
            {
                return Find.Element(By.Name(MonsterCATID)).GetControlValueAs<String>();
            }

            set
            {
                Find.Element(By.Name(MonsterCATID)).SendKeys(value);
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

        public string PasswordRetype
        {
            get
            {
                return Find.Element(By.Name(PasswordRetypeID)).GetControlValueAs<String>();
            }

            set
            {
                Find.Element(By.Name(PasswordRetypeID)).SendKeys(value);
            }

        }

        public void PasswordRetypePressEnter()
        {
            Find.Element(By.Name(PasswordRetypeID)).SendKeys(OQAS.Keys.Enter);
        }
    }
}
