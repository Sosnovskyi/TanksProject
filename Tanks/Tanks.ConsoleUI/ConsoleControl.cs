using System;
using System.IO;
using Tanks.GameEngine;

namespace Tanks.ConsoleUI
{
    internal class ConsoleControl : ControlBase
    {
        public override ControlActions GetAction()
        {
            ControlActions action = 0;
            var keyInfo = new ConsoleKeyInfo();
            keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    action = ControlActions.MoveLeft;
                    break;
                case ConsoleKey.RightArrow:
                    action = ControlActions.MoveRight;
                    break;
                case ConsoleKey.UpArrow:
                    action = ControlActions.MoveUp;
                    break;
                case ConsoleKey.DownArrow:
                    action = ControlActions.MoveDown;
                    break;
                case ConsoleKey.Spacebar:
                    action = ControlActions.Shoot;
                    break;
            }
            return action;
        }

        public static bool[,] GetMapArray(string path, int maxX, int maxY)
        {
            var mapArray = new bool[maxY, maxX];
            string readToEnd;
            using (var lReader = new StreamReader(path))
            {
                readToEnd = lReader.ReadToEnd();
                var lineSymbols = readToEnd.ToCharArray();
                var indexX = 0;
                var indexY = 0;
                for (var i = 0; i < lineSymbols.Length; i++)
                {
                    if (indexY == maxX)
                    {
                        indexY = 0;
                        indexX++;
                    }
                    if (lineSymbols[i] == '*')
                    {
                        mapArray[indexX, indexY] = true;
                        indexY++;
                    }
                    else if (lineSymbols[i] == ' ')
                    {
                        mapArray[indexX, indexY] = false;
                        indexY++;
                    }
                }
            }
            return mapArray;
        }
    }
}