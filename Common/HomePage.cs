using TestStack.Seleno.PageObjects;

namespace SourceBoxWebPages
{
    public class LogoutPage:Page
    {
        //https://ushome.textkernel.com/sourcebox/logout.jsp
    }

    public class HomePage:Page
    {
        static string HostUrl = AppConfigHandler.GetValue("host");

        //https://ushome.textkernel.com/match/login.jsp
        public LoginPage GotoLoginPage()
        {
            return Navigate.To<LoginPage>(HostUrl + AppConfigHandler.GetValue("LoginPage"));
        }

        //https://ushome.textkernel.com/match/logout.jsp
        public LogoutPage GotoLogoutPage()
        {
            return Navigate.To<LogoutPage>(HostUrl + AppConfigHandler.GetValue("LogoutPage"));
        }

        //https://ushome.textkernel.com/match/users.jsp?account=4&newuser=true
        public NewUserPage GotoNewUserPage()
        {
            return Navigate.To<NewUserPage>(HostUrl + AppConfigHandler.GetValue("NewUserPage"));
        }

        //https://ushome.textkernel.com/match/users.jsp
        public UsersPage GotoUsersPage()
        {
            return Navigate.To<UsersPage>(HostUrl + AppConfigHandler.GetValue("UsersPage"));
        }
    }
}
