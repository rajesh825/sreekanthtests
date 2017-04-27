using System;
using System.Collections.Specialized;
using System.Configuration;

namespace SourceBoxWebPages
{
    public static class AppConfigHandler
    {
        private static NameValueCollection configData;
        static AppConfigHandler()
        {
            string environment = ConfigurationManager.AppSettings["environment"];
            configData = (NameValueCollection)ConfigurationManager.GetSection(environment);
            Console.WriteLine("Loading data for environment: {0} ", environment);
        }

        public static string GetValue(string key)
        {
            string value = configData[key];
            Console.WriteLine("APP.CONFIG:: Key-{0} Value-{1} ", key, value);
            return value;
        }
    }
}
