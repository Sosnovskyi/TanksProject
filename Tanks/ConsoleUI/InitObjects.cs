using System;
using System.Timers;
using TanksGame.TanksEngine;

namespace TanksGame.ConsoleUI
{
    public class InitObjects
    {
        private Timer _playerTimer;

        private Timer _enemyTimer;

        private Timer _tankCanShoot;

        private Timer _enemyCanShoot;

        private Timer _bulletTimer;

        public Game Game { get; set; }

        private ConsoleGameInfo Info { get; set; }

        public void Init()
        {
            Info = new ConsoleGameInfo();

            var tank = new Tank(Info.PlayerInitPointX, Info.PlayerInitPointY, Info.PlayerStartDirection, Info.PlayerHealth);
            tank.TankDraw += Info.OnTankDraw;
            tank.TankErase += Info.OnTankErase;
            tank.HealthDraw += Info.OnDrawHealth;

            var enemy = new EnemyTank(Info.EnemyInitPointX, Info.EnemyInitPointY, Info.EnemyStartDirection, Info.EnemyHealth);
            enemy.EnemyTankDraw += Info.OnEnemyTankDraw;
            enemy.EnemyTankErase += Info.OnEnemyTankErase;
            enemy.EnemyHealthDraw += Info.OnEnemyDrawHealth;

            var control = new Control();
            control.GetAction += Info.OnGetAction;

            var map = new Map(Info.GetMap(), Info.SetGameSpaceBorders());
            map.DrawBorders += Info.OnDrawBorders;
            map.DrawMap += Info.OnDrawMap;
            map.MapPointDelete += Info.OnMapPointDelete;

            var bullet = new Bullet();
            bullet.BulletDraw += Info.OnBulletDraw;
            bullet.BulletErase += Info.OnBulletErase;

            Game = new Game(tank, enemy, control, map, bullet);
            Console.SetWindowSize(Info.MaxPointX + 2, Info.MaxPointY + 2);
            Game.StopGame += StopGame;
            Game.StartGame += StartGame;
            Game.ExitGame += Info.OnExitGame;
            InitTimers();
            Info.GameStatus = GameStatus.InProgress;
            Game.Start(Platform.Console);
            ConfirmExit();
        }

        private void ConfirmExit()
        {
            Info.ClearInfoBlock();
            Info.OnConfimExit();
            var action = Info.OnGetAction();
            while (action != GameControl.StartGame && action != GameControl.EndGame)
            {
                action = Info.OnGetAction();
            }
            switch (action)
            {
                case GameControl.StartGame:
                    Console.Clear();
                    Init();
                    break;
                case GameControl.EndGame:
                    Exit();
                    break;
            }
        }

        private void Exit()
        {
            Info.ClearInfoBlock();
            Console.SetCursorPosition(Info.ResultPointX + 1, Info.ResultPointY + 1);
            Console.Write("Thanks For Playing. Press Any Key For Exit.");
            Info.OnGetAction();
        }

        private void InitTimers()
        {
            _playerTimer = new Timer { Interval = 50 };
            _playerTimer.Elapsed += PlayerMove;

            _enemyTimer = new Timer { Interval = 150 };
            _enemyTimer.Elapsed += EnemyMove;

            _tankCanShoot = new Timer { Interval = 450 };
            _tankCanShoot.Elapsed += TankCanShoot;

            _enemyCanShoot = new Timer { Interval = 650 };
            _enemyCanShoot.Elapsed += EnemyCanShoot;

            _bulletTimer = new Timer { Interval = 20 };
            _bulletTimer.Elapsed += BulletsMove;

        }

        private void BulletsMove(object sender, ElapsedEventArgs e)
        {
            Game.BulletsMove();
        }

        private void EnemyCanShoot(object sender, ElapsedEventArgs e)
        {
            Game.EnemyCanShoot();
        }

        private void TankCanShoot(object sender, ElapsedEventArgs e)
        {
            Game.TankCanShoot();
        }

        private void EnemyMove(object sender, ElapsedEventArgs e)
        {
            Game.EnemyMove();
        }

        private void PlayerMove(object sender, ElapsedEventArgs e)
        {
            Game.PlayerCanMove();
        }

        private void StartTimers()
        {
            _playerTimer.Start();
            _enemyTimer.Start();
            _tankCanShoot.Start();
            _enemyCanShoot.Start();
            _bulletTimer.Start();
        }

        private void StopTimers()
        {
            _playerTimer.Stop();
            _enemyTimer.Stop();
            _tankCanShoot.Stop();
            _enemyCanShoot.Stop();
            _bulletTimer.Stop();
        }

        public void StartGame()
        {
            StartTimers();
        }

        public void StopGame()
        {
            StopTimers();
        }
    }
}