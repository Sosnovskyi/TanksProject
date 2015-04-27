using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Tanks.GameEngine;

namespace Tanks.ConsoleUI
{
    public class InitObjects
    {
        public static void InitAndStart()
        {
            var enemyTankInitX = Int32.Parse(ConfigurationManager.AppSettings["EnemyTankInitX"]);
            var enemyTankInitY = Int32.Parse(ConfigurationManager.AppSettings["EnemyTankInitY"]);
            var minX = Int32.Parse(ConfigurationManager.AppSettings["MinX"]);
            var maxX = Int32.Parse(ConfigurationManager.AppSettings["MaxX"]);
            var minY = Int32.Parse(ConfigurationManager.AppSettings["MinY"]);
            var maxY = Int32.Parse(ConfigurationManager.AppSettings["MaxY"]);
            var tankInitX = Int32.Parse(ConfigurationManager.AppSettings["TankInitX"]);
            var tankInitY = Int32.Parse(ConfigurationManager.AppSettings["TankInitY"]);
            var filePath = ConfigurationManager.AppSettings["FilePath"];
            var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            const MoveDirection enemyStartDirection = MoveDirection.Left;
            const MoveDirection startDirection = MoveDirection.Right;

            var myTank = new TankEngine();
            myTank.TankDraw += EventMethods.MyTankDraw;
            myTank.TankErase += EventMethods.MyTankErase;

            var enemyTank = new EnemyTankEngine();
            enemyTank.EnemyTankDraw += EventMethods.EnemyTankDraw;
            enemyTank.EnemyTankErase += EventMethods.EnemyTankErase;

            var bullet = new BulletEngine();
            bullet.BulletDraw += EventMethods.TanksBulletDraw;
            bullet.BulletErase += EventMethods.TanksBulletErase;

            var map = new MapBase();
            map.InitMap += EventMethods.InitGameMap;
            map.DrawMap += EventMethods.DrawGameMap;
            map.DrawBorders += EventMethods.DrawGameMapBorders;
            map.MapPointErase += EventMethods.GameMapPointErase;
            map.InitMap(out map.GameMap, GetMapArray(string.Concat(projectPath, filePath), maxX, maxY), maxY, maxX);

            var control = new ControlBase();
            control.GetAction += EventMethods.GetAction;

            var gameEngine = new GameEngineControl(myTank, enemyTank, bullet, control, map,
                enemyStartDirection, startDirection);
            gameEngine.GameIsOver += EventMethods.WriteResult;
            gameEngine.GameProcess(tankInitX, tankInitY, enemyTankInitX, enemyTankInitY, minX, minY, maxY, maxX);
        }

        public static int[,] GetMapArray(string path, int maxX, int maxY)
        {
            var mapArray = new int[maxY, maxX];
            string readToEnd;
            using (var lReader = new StreamReader(path))
            {
                readToEnd = lReader.ReadToEnd();
                var lineSymbols = readToEnd.ToCharArray();
                var indexX = 0;
                var indexY = 0;
                foreach (char t in lineSymbols)
                {
                    if (indexY == maxX)
                    {
                        indexY = 0;
                        indexX++;
                    }
                    if (t == '*')
                    {
                        mapArray[indexX, indexY] = 1;
                        indexY++;
                    }
                    else if (t == ' ')
                    {
                        mapArray[indexX, indexY] = 0;
                        indexY++;
                    }
                }
            }
            return mapArray;
        }
    }
}