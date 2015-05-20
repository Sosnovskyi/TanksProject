using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TanksGame.TanksEngine;

namespace TanksGame.ConsoleUI
{
    public class ConsoleGameInfo
    {
        #region Fields

        static string _filePath = ConfigurationManager.AppSettings["FilePath"];
        static string _projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        private readonly string _mapPath = string.Concat(_projectPath, _filePath);

        #endregion

        #region Properties
        public int ScorePointX { get; set; }
        public int ScorePointY { get; set; }
        public int MinPointX { get; set; }
        public int MinPointY { get; set; }
        public int MaxPointX { get; set; }
        public int MaxPointY { get; set; }
        public int GameSpaceYDiff { get; set; }
        public int GameSpaceXDiff { get; set; }
        public int ResultPointX { get; set; }
        public int ResultPointY { get; set; }
        public int PlayerHealthPointX { get; set; }
        public int PlayerHealthPointY { get; set; }
        public int EnemyHealthPointX { get; set; }
        public int EnemyHealthPointY { get; set; }
        public int EnemyInitPointY { get; set; }
        public int EnemyInitPointX { get; set; }
        public int PlayerInitPointY { get; set; }
        public int PlayerInitPointX{ get; set; }
        public int PlayerHealth { get; set; }
        public int EnemyHealth { get; set; }
        public GameControl PlayerStartDirection { get; set; }
        public GameControl EnemyStartDirection { get; set; }
        public GameStatus GameStatus { get; set; }

        #endregion

        #region Constructor
        public ConsoleGameInfo()
        {
            ScorePointX = Int32.Parse(ConfigurationManager.AppSettings["ScorePointX"]);
            ScorePointY = Int32.Parse(ConfigurationManager.AppSettings["ScorePointY"]);
            MinPointX = Int32.Parse(ConfigurationManager.AppSettings["MinPointX"]);
            MinPointY = Int32.Parse(ConfigurationManager.AppSettings["MinPointY"]);
            MaxPointX = Int32.Parse(ConfigurationManager.AppSettings["MaxPointX"]);
            MaxPointY = Int32.Parse(ConfigurationManager.AppSettings["MaxPointY"]);
            GameSpaceYDiff = Int32.Parse(ConfigurationManager.AppSettings["GameSpaceYDiff"]);
            GameSpaceXDiff = Int32.Parse(ConfigurationManager.AppSettings["GameSpaceXDiff"]);
            ResultPointX = Int32.Parse(ConfigurationManager.AppSettings["ResultPointX"]);
            ResultPointY = Int32.Parse(ConfigurationManager.AppSettings["ResultPointY"]);
            PlayerHealthPointX = Int32.Parse(ConfigurationManager.AppSettings["PlayerHealthPointX"]);
            PlayerHealthPointY = Int32.Parse(ConfigurationManager.AppSettings["PlayerHealthPointY"]);
            EnemyHealthPointX = Int32.Parse(ConfigurationManager.AppSettings["EnemyHealthPointX"]);
            EnemyHealthPointY = Int32.Parse(ConfigurationManager.AppSettings["EnemyHealthPointY"]);
            PlayerInitPointX = Int32.Parse(ConfigurationManager.AppSettings["PlayerInitPointX"]);
            PlayerInitPointY = Int32.Parse(ConfigurationManager.AppSettings["PlayerInitPointY"]);
            EnemyInitPointX = Int32.Parse(ConfigurationManager.AppSettings["EnemyInitPointX"]);
            EnemyInitPointY = Int32.Parse(ConfigurationManager.AppSettings["EnemyInitPointY"]);
            PlayerHealth = Int32.Parse(ConfigurationManager.AppSettings["PlayerHealth"]);
            EnemyHealth = Int32.Parse(ConfigurationManager.AppSettings["EnemyHealth"]);
            PlayerStartDirection = GameControl.MoveUp;
            EnemyStartDirection = GameControl.MoveRight;
        }

        #endregion

        #region Tank Methods
        public void OnTankErase(List<TankFragment> tank)
        {
            foreach (var t in tank)
            {
                Drawing.Draw(t.PointX, t.PointY, " ");
            }
        }
        public void OnTankDraw(List<TankFragment> tank)
        {
            for (int index = 0; index < tank.Count; index++)
            {
                if (index == 0 || index == 2)
                {
                    Drawing.Draw(tank[index].PointX, tank[index].PointY, " ");
                }
                else
                {
                    Drawing.Draw(tank[index].PointX, tank[index].PointY, "x");
                }
            }
        }

        public void OnDrawHealth(int health)
        {
            char healthPoint = Convert.ToChar(9829);
            for (int i = 0; i < health + 1; i++)
            {
                Drawing.Draw(PlayerHealthPointX + i, PlayerHealthPointY, " ");
            }
            for (int i = 0; i < health; i++)
            {
                Drawing.Draw(PlayerHealthPointX + i, PlayerHealthPointY, healthPoint.ToString());
            }
        }

        #endregion 

        #region Enemy tank methods
        public void OnEnemyTankErase(List<TankFragment> tank)
        {
            foreach (var t in tank)
            {
                Drawing.Draw(t.PointX, t.PointY, " ");
            }
        }

        public void OnEnemyTankDraw(List<TankFragment> tank)
        {
            for (int index = 0; index < tank.Count; index++)
            {
                if (index == 0 || index == 2)
                {
                    Drawing.Draw(tank[index].PointX, tank[index].PointY, " ");
                }
                else
                {
                    Drawing.Draw(tank[index].PointX, tank[index].PointY, "*");
                }
            }
        }

        public void OnEnemyDrawHealth(int health)
        {
            char healthPoint = Convert.ToChar(9829);
            for (int i = 0; i < health + 1; i++)
            {
                Drawing.Draw(EnemyHealthPointX + i, EnemyHealthPointY, " ");
            }
            for (int i = 0; i < health; i++)
            {
                Drawing.Draw(EnemyHealthPointX + i, EnemyHealthPointY, healthPoint.ToString());
            }
        }

        #endregion

        #region Control methods
        public GameControl OnGetAction()
        {
            var action = GameControl.DafaultAction;
            var keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    action = GameControl.MoveUp;
                    break;
                case ConsoleKey.DownArrow:
                    action = GameControl.MoveDown;
                    break;
                case ConsoleKey.LeftArrow:
                    action = GameControl.MoveLeft;
                    break;
                case ConsoleKey.RightArrow:
                    action = GameControl.MoveRight;
                    break;
                case ConsoleKey.Enter:
                    action = GameControl.StartGame;
                    break;
                case ConsoleKey.Escape:
                    action = GameControl.EndGame;
                    break;
                case ConsoleKey.Spacebar:
                    action = GameControl.TankShoot;
                    break;
                case ConsoleKey.P:
                    action = GameControl.PauseGame;
                    break;
                case ConsoleKey.R:
                    action = GameControl.ResumeGame;
                    break;
            }
            return action;
        }

        #endregion

        #region Info methods

        public void OnExitGame(ExitStatus status)
        {
            GameStatus = GameStatus.GameEnd;
            Console.SetCursorPosition(ResultPointX + 1, ResultPointY + 1);
            switch (status)
            {
                    case ExitStatus.LoseGame:
                    Console.Write("You Lose. Press Any Key.");
                    break;
                    case ExitStatus.WinGame:
                    Console.Write("You Win. Press Any Key.");
                    break;
                    case ExitStatus.StopGame:
                    Console.Write("You Stop The Game. Press Any Key.");
                    break;
            }
        }

        public void ClearInfoBlock()
        {
            Console.SetCursorPosition(ResultPointX + 1, ResultPointY + 1);
            Console.Write(new string(' ', MaxPointX));
        }

        public void OnConfimExit()
        {
            Console.SetCursorPosition(ResultPointX + 1, ResultPointY + 1);
            Console.Write("For Exit Press Escape. For Restart Press Enter");
        }

        #endregion

        #region Map methods

        public void OnDrawMap()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var map = GetMap();
            foreach (var m in map)
            {
                Console.SetCursorPosition(m.Key.PointX + 1, m.Key.PointY + 1);
                Console.Write("+");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void OnDrawBorders()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(MinPointX + 1, MinPointY + 3);
            Console.Write(new string('*', MaxPointX));
            Console.SetCursorPosition(MaxPointX - 10, MinPointY);
            Console.Write("Player");
            Console.SetCursorPosition(MinPointX + 5, MinPointY);
            Console.Write("Enemy");
            Console.SetCursorPosition(MinPointX + 1, MaxPointY - GameSpaceYDiff + 1);
            Console.Write(new string('*', MaxPointX));
            Console.SetCursorPosition(MinPointX, MaxPointY + 1);
            Console.Write(new string('*', MaxPointX + 1));
            for (int i = MinPointY + 3; i < MaxPointY - 1; i++)
            {
                Console.SetCursorPosition(MinPointX + 1, i);
                Console.Write("*");
            }
            for (int i = MinPointY + 3; i < MaxPointY - 1; i++)
            {
                Console.SetCursorPosition(MaxPointX, i);
                Console.Write("*");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public List<int> SetGameSpaceBorders()
        {
            var borsers = new List<int>
            {
                MinPointX,
                MinPointY + 4,
                MaxPointX - 1,
                MaxPointY - 3
            };
            return borsers;
        }

        public Dictionary<Coordinate, MapObject> GetMap()
        {
            Dictionary<Coordinate, MapObject> map = new Dictionary<Coordinate, MapObject>(); 
            for (int i = MinPointX + 2; i < MaxPointX; i++)
            {
                for (int j = MinPointY + 3; j < MaxPointY - 3; j++)
                {
                    if ((j + 1) % 10 == 0)
                    {
                        map.Add(new Coordinate(i, j), MapObject.Wall);
                        if (!map.Keys.ToList().Exists(k => k.PointX == i && k.PointY == j + 1))
                        {
                            map.Add(new Coordinate(i, j + 1), MapObject.Wall);
                        }
                        if (!map.Keys.ToList().Exists(k => k.PointX == i && k.PointY == j + 2))
                        {
                            map.Add(new Coordinate(i, j + 2), MapObject.Wall);
                        }
                    }
                    else if ((i + 1) % 10 == 0)
                    {
                        map.Add(new Coordinate(i, j), MapObject.Wall);
                        map.Add(new Coordinate(i + 1, j), MapObject.Wall);
                        map.Add(new Coordinate(i + 2, j), MapObject.Wall);
                    }
                }
            }
            return map;
        }

        public void OnMapPointDelete(Coordinate point)
        {
            Drawing.Draw(point.PointX, point.PointY, " ");
        }

        #endregion

        #region Bullet methods
        public void OnBulletDraw(Coordinate coordinate)
        {
            Drawing.Draw(coordinate.PointX, coordinate.PointY, "*");
        }

        public void OnBulletErase(Coordinate coordinate)
        {
            Drawing.Draw(coordinate.PointX, coordinate.PointY, " ");
        }
        #endregion
    }
}