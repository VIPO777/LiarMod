using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LiarMod.ConsoleUtils
{
    internal static class InternalConsole
    {
        [DllImport("kernel32.dll")]
        internal static extern int AllocConsole();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        public static void ShowConsole()
        {
            InternalConsole.SetForegroundWindow(InternalConsole.GetConsoleWindow());
        }

        public static void Load()
        {
            InternalConsole.AllocConsole();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            });
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
            Console.Title = "Console Bitches";
            Console.Clear();
            Console.WriteLine("Console initialized.");
            InternalConsole.ShowConsole();
        }

        public static string AskInput(string question)
        {
            InternalConsole.ShowConsole();
            Console.Write(question);
            while (Console.KeyAvailable)
            {
                Console.ReadKey();
            }
            string text = Console.ReadLine();
            if (!(text == string.Empty))
            {
                return text;
            }
            return null;

        }
    }
}
