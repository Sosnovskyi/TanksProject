using System;
using System.Configuration;

namespace Tanks.ConsoleUI
{
    internal class TanksConsoleStart
    {
        private static void Main(string[] args)
        {
            var maxX = Int32.Parse(ConfigurationManager.AppSettings["MaxX"]);
            var maxY = Int32.Parse(ConfigurationManager.AppSettings["MaxY"]);
            Console.SetWindowSize(maxX, maxY);
            InitObjects.InitAndStart();
            Console.ReadKey();
        }
    }
}