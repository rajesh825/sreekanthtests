namespace SourceBoxWebPages
{
    using System;
    using System.Drawing;

    using OpenQA.Selenium.Firefox;

    using TestStack.Seleno.Configuration;
    using TestStack.Seleno.Configuration.WebServers;
    using OpenQA.Selenium;

    public static class Host
    {
        public static readonly SelenoHost Instance = new SelenoHost();

        private static readonly string HostUrl = AppConfigHandler.GetValue("host");

        static Host()
        {
            Instance.Run(x => x.WithWebServer(new InternetWebServer(HostUrl)));
            //Instance.Run(x => x.WithWebServer(new InternetWebServer(HostUrl + AppConfigHandler.GetValue("LoginPage")))
            //    // comment out this line if you want to run the tests locally
            //    //.WithRemoteWebDriver(RemoteDriver)
            //    );
            Driver.Manage().Window.Size = new Size(1280, 768);
        }

        public static readonly FirefoxDriver Driver = new FirefoxDriver();

        public static void TakeScreenShot(string StepName)
        {
            Screenshot screenshot = Driver.GetScreenshot();
            screenshot.SaveAsFile(
                StepName + DateTime.Now.ToString("yyyyMMdd.HHmmss.fff") + ".jpg",
                ScreenshotImageFormat.Jpeg);
        }


        //public static RemoteWebDriver RemoteDriver()
        //{
        //    return Driver;
        //}

        //private static readonly string ChromeDriverHostUrl = ConfigurationManager.AppSettings["chromeHost"];

        //public static readonly RemoteWebDriver Driver = new RemoteWebDriver(
        //    new Uri(ChromeDriverHostUrl),
        //    DesiredCapabilities.Chrome());
    }
}