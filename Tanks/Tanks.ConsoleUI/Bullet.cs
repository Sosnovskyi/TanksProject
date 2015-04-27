using System;
using Tanks.GameEngine;

namespace Tanks.ConsoleUI
{
    internal class Bullet : BulletEngine
    {
        public override void BulletDraw(BulletEngine bulletEngine)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(bulletEngine.X + 1, bulletEngine.Y + 1);
            Console.Write('*');
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override void BulletErase(BulletEngine bulletEngine)
        {
            Console.SetCursorPosition(bulletEngine.X + 1, bulletEngine.Y + 1);
            Console.Write(' ');
        }
    }
}