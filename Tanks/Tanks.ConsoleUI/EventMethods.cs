using System;
using System.Collections.Generic;
using Tanks.GameEngine;

namespace Tanks.ConsoleUI
{
    public class EventMethods
    {
        public static void MyTankDraw(List<TankFragment> tank)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            for (var i = 0; i < tank.Count; i++)
            {
                if (i == 0 || i == 2)
                {
                    Console.SetCursorPosition(tank[i].X + 1, tank[i].Y + 1);
                    Console.Write(' ');
                }
                else
                {
                    Console.SetCursorPosition(tank[i].X + 1, tank[i].Y + 1);
                    Console.Write('x');
                }
            }
        }

        public static void MyTankErase(List<TankFragment> tank)
        {
            foreach (var t in tank)
            {
                Console.SetCursorPosition(t.X + 1, t.Y + 1);
                Console.Write(' ');
            }
        }

        public static void EnemyTankDraw(List<TankFragment> tank)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (var i = 0; i < tank.Count; i++)
            {
                if (i == 0 || i == 2)
                {
                    Console.SetCursorPosition(tank[i].X + 1, tank[i].Y + 1);
                    Console.Write(' ');
                }
                else
                {
                    Console.SetCursorPosition(tank[i].X + 1, tank[i].Y + 1);
                    Console.Write('x');
                }
            }
        }

        public static void EnemyTankErase(List<TankFragment> tank)
        {
            foreach (var t in tank)
            {
                Console.SetCursorPosition(t.X + 1, t.Y + 1);
                Console.Write(' ');
            }
        }

        public static void TanksBulletDraw(BulletEngine bulletEngine)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(bulletEngine.X + 1, bulletEngine.Y + 1);
            Console.Write('*');
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void TanksBulletErase(BulletEngine bulletEngine)
        {
            Console.SetCursorPosition(bulletEngine.X + 1, bulletEngine.Y + 1);
            Console.Write(' ');
        }

        public static void DrawGameMapBorders(List<int> borders)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(borders[0] + 1, borders[1] + 1);
            for (var i = 0; i <= borders[3] - 1; i++)
            {
                Console.Write('X');
            }
            Console.SetCursorPosition(borders[0] + 1, borders[2]);
            for (var i = 0; i <= borders[3] - 1; i++)
            {
                Console.Write('X');
            }
            for (var i = borders[0] + 1; i <= borders[2]; i++)
            {
                Console.SetCursorPosition(borders[0] + 1, i);
                Console.Write('X');
                Console.SetCursorPosition(borders[3], i);
                Console.Write('X');
            }
        }

        public static void DrawGameMap(int[,] map)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 1)
                    {
                        Console.SetCursorPosition(j + 1, i + 1);
                        Console.Write('+');
                    }
                    else if (map[i, j] == 1)
                    {
                        Console.SetCursorPosition(j + 1, i + 1);
                        Console.Write(' ');
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void InitGameMap(out int[,] map, int[,] array, int sizeX, int sizeY)
        {
            map = new int[sizeX, sizeY];
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = array[i, j];
                }
            }
        }

        public static void GameMapPointErase(int[] elements)
        {
            Console.SetCursorPosition(elements[1] + 1, elements[0] + 1);
            Console.Write(' ');
        }

        public static void WriteResult(string message)
        {
            Console.SetCursorPosition(15, 4);
            Console.Write(message);
        }

        public static ControlActions GetAction()
        {
            ControlActions action = 0;
            var keyInfo = Console.ReadKey(true);
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
    }
}