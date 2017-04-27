using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SourceBoxWebPages
{
    enum UserData
    {
        UserName = 0,
        FullName = 1,
        Branch = 2,
        EmailAddress = 3,
        MonsterCAT = 4,
        Password
    };

    public class Helper
    {
        HomePage homepage = null;

        public void DisbleUsersFromCSV(string filename)
        {
            string[] users = System.IO.File.ReadAllLines(filename);
            homepage = Host.Instance.NavigateToInitialPage<HomePage>();
            LoginToSourcebox();            

            foreach(string data in users)
            {
                string userName = data.Trim().ToLower(); // sourcebox site automatically lowering the case.
                if (userName.Length != 0)
                {
                    Console.WriteLine("\nProcessing user: {0}", userName);
                    UsersPage usersPage = SearchUser(userName);
                    usersPage.ChangeUserState(false);
                }
            }

            homepage.GotoLogoutPage();
            Console.WriteLine("\n");
        }

        public void AddUsersFromCSV(string filename)
        {
            List<string[]> users = ParseCSV(filename);

            homepage = Host.Instance.NavigateToInitialPage<HomePage>();
            LoginToSourcebox();

            //UserName,FullName,Branch,EmailAddress,MonsterCAT,Password
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

        private UsersPage SearchUser(string userName)
        {
            UsersPage usersPage = homepage.GotoUsersPage();
            usersPage.WaitForPageToLoad();
            usersPage.FindText(userName);
            return usersPage;
        }

        #region Helpers
        private void CreateNewUser(string userName, string fullName, string email, string password, string monsterCAT = "", string branch = "")
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
            newUserPage.PasswordRetypePressEnter();
            Host.TakeScreenShot("CreateNewUser");
            Thread.Sleep(5000);
        }

        private void LoginToSourcebox()
        {
            LoginPage loginPage = homepage.GotoLoginPage();
            loginPage.WaitForPageToLoad();
            loginPage.AccountName = AppConfigHandler.GetValue("AccountName");
            loginPage.UserName = AppConfigHandler.GetValue("UserName");
            loginPage.Password = AppConfigHandler.GetValue("Password") + Environment.NewLine;
            loginPage.PressEnter();
            Host.TakeScreenShot("LoginToSourcebox");
            Thread.Sleep(5000);
        }
       
        public List<string[]> ParseCSV(string path, bool headerPresent = false)
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

                    if(fields.Length != 6 )
                    {
                        throw new Exception("Missing fields. Expected is 6.");
                    }
                    
                    ////foreach (string fieldValue in fields)
                    ////{
                    ////    Console.WriteLine("\t{0}", fieldValue);
                    ////}
                    ////Do more stuff here with each field.
                }
            }

            return parsedData;
        }
        #endregion
    }
}
