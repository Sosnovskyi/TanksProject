using System;

namespace TanksGame.ConsoleUI
{
    public class Drawing
    {
        private static Object sync = new Object();

        public static void Draw(int x, int y, string symbol)
        {
            lock (sync)
            {
                Console.SetCursorPosition(x + 1, y + 1);
                Console.Write("{0}", symbol);
            }
        }   
    }
}