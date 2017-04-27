using System;
using SourceBoxWebPages;
using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Threading;

namespace KforceUIAutomation
{
    enum UserData
    {
        UserName=0, 
        FullName=1, 
        Branch=2, 
        EmailAddress=3, 
        MonsterCAT=4, 
        Password
    };

    [TestFixture]
    public class TKCreateUsers
    {
        HomePage homepage;
        
        #region Helpers
        public void CreateNewUser(string userName, string fullName, string email, string password, string monsterCAT = "", string branch = "")
        {
            NewUserPage newUserPage = homepage.GotoNewUserPage();
            newUserPage.WaitForPageToLoad();
            newUserPage.UserName = userName.ToLower();
            newUserPage.FullName = fullName;
            if (branch.Trim() != string.Empty)
            {
                newUserPage.Branch = branch;
            }

            newUserPage.Email = email;
            if (monsterCAT.Trim() != string.Empty)
            {
                newUserPage.MonsterCAT = monsterCAT;
            }

            newUserPage.Password = password;
            newUserPage.PasswordRetype = password + Environment.NewLine;
            Host.TakeScreenShot("CreateNewUser");
            Thread.Sleep(5000);
        }

        public void LoginToSourcebox()
        {
            LoginPage loginPage = homepage.GotoLoginPage();
            loginPage.WaitForPageToLoad();
            loginPage.AccountName = AppConfigHandler.GetValue("AccountName");
            loginPage.UserName = AppConfigHandler.GetValue("UserName");
            loginPage.Password = AppConfigHandler.GetValue("Password") + Environment.NewLine;
            Host.TakeScreenShot("LoginToSourcebox");
            Thread.Sleep(5000);
        }
        public void SearchUser(string userName)
        {
            UsersPage usersPage = homepage.GotoUsersPage();
            usersPage.WaitForPageToLoad();
            usersPage.FindText(userName);
        }

        public List<string[]> parseCSV(string path, bool headerPresent = false)
        {
            List<string[]> parsedData = new List<string[]>();

            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.Delimiters = new string[] { "," };

                // Ignore header information
                if (headerPresent)
                {
                    parser.ReadLine();
                }

                while (true)
                {
                    string[] fields = parser.ReadFields();
                    if (fields == null)
                    {
                        break;
                    }

                    parsedData.Add(fields);
                    Console.WriteLine("{0} field(s)", fields.Length);
                    //foreach (string fieldValue in fields)
                    //{
                    //    Console.WriteLine("\t{0}", fieldValue);
                    //}
                    //Do more stuff here with each field.
                }

            }

            return parsedData;
        }



        [Test]
        public void SearchUserTest()
        {
            homepage = Host.Instance.NavigateToInitialPage<HomePage>();
            LoginToSourcebox();

            UsersPage usersPage = homepage.GotoUsersPage();
            usersPage.WaitForPageToLoad();
            usersPage.FindText("fakeuser1");

            homepage.GotoLogoutPage();
        }

        [Test]
        public void AppConfigSectionTest()
        {
            //string environment = ConfigurationManager.AppSettings["environment"];

            //NameValueCollection test = (NameValueCollection)ConfigurationManager.GetSection("PROD");
            //Console.WriteLine(test["host"]);

            //test = (NameValueCollection)ConfigurationManager.GetSection("QA");
            //Console.WriteLine(test["host"]);

            //test = (NameValueCollection)ConfigurationManager.GetSection(environment);
            //Console.WriteLine(test["host"]);
            //Console.WriteLine(test["LoginPage"]);
            //Console.WriteLine(test["LogoutPage"]);
            //Console.WriteLine(test["NewUserPage"]);
            //Console.WriteLine(test["UsersPage"]);

            Console.WriteLine(AppConfigHandler.GetValue("host"));
            Console.WriteLine(AppConfigHandler.GetValue("LoginPage"));
            Console.WriteLine(AppConfigHandler.GetValue("LogoutPage"));
            Console.WriteLine(AppConfigHandler.GetValue("NewUserPage"));
            Console.WriteLine(AppConfigHandler.GetValue("UsersPage"));
        }
        #endregion

        [Test]
        public void AddUsersFromCSV()
        {
            homepage = Host.Instance.NavigateToInitialPage<HomePage>();
            LoginToSourcebox();

            //UserName,FullName,Branch,EmailAddress,MonsterCAT,Password
            List<string[]> users = parseCSV(@"c:\temp\tkusers.csv");
            foreach (string[] user in users)
            {
                ////(string userName, string fullName, string email, string password, string monsterCAT = "", string branch = "")
                CreateNewUser(
                    user[(int)UserData.UserName],
                    user[(int)UserData.FullName],
                    user[(int)UserData.EmailAddress],
                    user[(int)UserData.Password],
                    user[(int)UserData.MonsterCAT],
                    user[(int)UserData.Branch]);

                SearchUser(user[(int)UserData.UserName]);
            }

            homepage.GotoLogoutPage();
        }

        [Test]
        public void AddUserTest()
        {
            homepage = Host.Instance.NavigateToInitialPage<HomePage>();
            LoginToSourcebox();

            //UserName,FullName,Branch,EmailAddress,MonsterCAT,Password
            //fakeuser1,fakeuser1,,fakeuser1@kforce.com,1,password
            string key = "fakeuser_100";
            CreateNewUser(key + "userName", key + "fullName", key + "email@kforce.com", key + "password", key + "monsterCAT", key + "branch");
            SearchUser(key + "userName");

            homepage.GotoLogoutPage();
        }

        [Test]
        public void ParserTest()
        {
            List<string[]> users = parseCSV(@"c:\temp\tkusers.csv");
            foreach(string[]user in users)
            {
                foreach (string data in user)
                {
                    Console.WriteLine("\t{0}", data);
                }

                Console.WriteLine(Environment.NewLine);
            }
        }
    }
}
