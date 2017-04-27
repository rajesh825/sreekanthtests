using System;
using SourceBoxWebPages;

namespace SourceBoxUserManager
{
    class Program
    {
        static void Main(string[] args)
        {
            if ((args == null) || (args.Length == 0))
            {
                Program.PrintUsage();
                return;
            }
            
            if (args.Length != 2)
            {
                Console.WriteLine("\nInvalid input.");
                PrintUsage();
                return;
            }

            string filename = args[1];
            Helper helper = new Helper();
            switch(args[0].ToUpper())
            {
                case "ADD":
                    {
                        helper.AddUsersFromCSV(filename);
                        break;
                    }

                case "DISABLE":
                    {
                        helper.DisbleUsersFromCSV(filename);
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Unsupported operation requested.");
                        PrintUsage();
                        break;
                    }
            }
        }

        static void PrintUsage()
        {
            string binaryName = "SourceBoxUserManager.exe";
            Console.WriteLine("\nUsage:");
            Console.WriteLine("\t{0} <Operation> <Parameter>\n\n", binaryName);

            Console.WriteLine("\tAdd users from csv file");
            Console.WriteLine("\t\t{0} ADD <Input CSV FileName\n", binaryName);
            Console.WriteLine("\tExample:");
            Console.WriteLine("\t\t{0} ADD users.csv\n\n", binaryName);

            Console.WriteLine("\tDisable users from input text file");
            Console.WriteLine("\t\t{0} DISABLE <Input FileName>\n", binaryName);
            Console.WriteLine("\tExample:");
            Console.WriteLine("\t\t{0} DISABLE users.txt\n\n", binaryName);

            Console.WriteLine("*** NOTE: Set the environment name in {0}.config file ***\n\n", binaryName);
        }
    }
}
