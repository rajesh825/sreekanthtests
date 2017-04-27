using System;
using TestStack.Seleno.PageObjects.Locators;
using TestStack.Seleno.PageObjects;
using OQAS = OpenQA.Selenium;
using OQASUI = OpenQA.Selenium.Support.UI;

namespace SourceBoxWebPages
{
    public class UsersPage : Page
    {
        private const string GrantAccessCheckboxID = "enabled";

        public void ChangeUserState(bool expectedState)
        {
            System.Threading.Thread.Sleep(5000);
            var element = Find.Element(By.Name(GrantAccessCheckboxID));
            if (element != null)
            {
                bool currentState = element.Selected;
                Console.WriteLine("Found user checkbox with status {0}", currentState.ToString());

                if (expectedState != currentState)
                {
                    element.Click();
                    System.Threading.Thread.Sleep(5000);

                    var saveButton = Find.Element(By.CssSelector("input[value=Store]"));
                    if (saveButton != null)
                    {
                        Console.WriteLine("Found submit button. Clicking");
                        saveButton.Click();
                    }
                    else
                    {
                        throw new Exception("Unable to find submit button.");
                    }
                        
                }
                System.Threading.Thread.Sleep(10000);
            }
            else
            {
                throw new Exception("Unable to find user checkbox");
            }
        }

        public void FindText(string searchText)
        {
            var element = Find.Element(By.LinkText(searchText));
            if (element != null)
            {
                element.Click();
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("Found link with search text: " + searchText);
            }
            else
            {
                throw new Exception("Unable to find link with search text: " + searchText);
            }
        }

        public void WaitForPageToLoad()
        {
            OQASUI.IWait<OQAS.IWebDriver> wait = new OQASUI.WebDriverWait(Host.Driver, TimeSpan.FromSeconds(60.00));
            wait.Until(d => ((OQAS.IJavaScriptExecutor)Host.Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
