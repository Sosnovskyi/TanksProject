using System;
using System.Windows.Forms;
using TanksGame.TanksEngine;
using Control = TanksGame.TanksEngine.Control;

namespace TanksGame.DesktopUI
{
    public class InitObjects
    {
        #region Fields
        private Timer _playerTimer;

        private Timer _playerCanMove;

        private Timer _enemyTimer;

        private Timer _tankCanShoot;

        private Timer _enemyCanShoot;

        private Timer _bulletTimer;
        #endregion

        #region Properties
        private Label EnemyHealth { get; set; }

        private Label PlayerHealth { get; set; }

        public Game Game { get; set; }

        public Tank Player { get; set; }

        public EnemyTank Enemy { get; set; }

        private DesktopInfo Info { get; set; }
        
        #endregion

        public void Init(Label lblPlayerHealthValue, Label lblEnemyHealthValue)
        {
            PlayerHealth = lblPlayerHealthValue;
            EnemyHealth = lblEnemyHealthValue;
            Info = new DesktopInfo();

            Player = new Tank(Info.TankInitPointX, Info.TankInitPointY, GameControl.MoveUp, Info.PlayerHealth);
            Player.TankDraw += Info.OnTankDraw;
            Player.TankErase += Info.OnTankErase;
            Player.HealthDraw += DrawHealth;

            Enemy = new EnemyTank(Info.EnemyInitPointX, Info.EnemyInitPointY, GameControl.MoveDown, Info.EnemyHealth);
            Enemy.EnemyTankDraw += Info.OnEnemyTankDraw;
            Enemy.EnemyTankErase += Info.OnEnemyTankErase;
            Enemy.EnemyHealthDraw += DrawEnemyHealth;

            var control = new Control();
            control.GetAction += Info.OnGetAction;

            var map = new Map(frmDesktopGame.GetMap(), Info.SetGameSpaceBorders());
            map.MapPointDelete += Info.OnMapPointDelete;

            var bullet = new Bullet();
            bullet.BulletDraw += Info.OnBulletDraw;
            bullet.BulletErase += Info.OnBulletErase;

            Game = new Game(Player, Enemy, control, map, bullet);
            Game.StopGame += StopGame;
            Game.StartGame += StartGame;
            Game.ExitGame += ExitGame;
            InitTimers();
            StartTimers();
            Game.Start(Platform.WinForms);
        }

        private void ExitGame(ExitStatus element)
        {
            StopTimers();
            _playerTimer.Stop();
            frmDesktopGame.Status = element;
            SetResult();
            frmDesktopGame.Exit();
        }

        private void SetResult()
        {
            switch (frmDesktopGame.Status)
            {
                case ExitStatus.WinGame:
                    frmDesktopGame.Result = "You Win";
                    break;
                case ExitStatus.LoseGame:
                    frmDesktopGame.Result = "You Lose";
                    break;
                case ExitStatus.StopGame:
                    frmDesktopGame.Result = "You Stop Game";
                    break;
            }
        }

        private void DrawHealth(int health)
        {
            PlayerHealth.Text = String.Empty;
            char healthPoint = Convert.ToChar(9829);
            string healthPart = null;
            for (int i = 0; i < health; i++)
            {
                healthPart = string.Concat(healthPart, healthPoint.ToString());
            }
            PlayerHealth.Text = healthPart;
        }

        private void DrawEnemyHealth(int health)
        {
            EnemyHealth.Text = String.Empty;
            char healthPoint = Convert.ToChar(9829);
            string healthPart = null;
            for (int i = 0; i < health; i++)
            {
                healthPart = string.Concat(healthPart, healthPoint.ToString());
            }
            EnemyHealth.Text = healthPart;
        }
        
        private void InitTimers()
        {
            _playerTimer = new Timer { Interval = 50 };
            _playerTimer.Tick += PlayerMove;

            _playerCanMove = new Timer{Interval = 10};
            _playerCanMove.Tick += PlayerCanMove;

            _enemyTimer = new Timer { Interval = 150 };
            _enemyTimer.Tick += EnemyMove;

            _tankCanShoot = new Timer { Interval = 450 };
            _tankCanShoot.Tick += OnTankCanShoot;

            _enemyCanShoot = new Timer { Interval = 650 };
            _enemyCanShoot.Tick += OnEnemyCanShoot;

            _bulletTimer = new Timer { Interval = 20 };
            _bulletTimer.Tick += BulletMove;

        }

        #region HandlerMethods
        private void StartTimers()
        {
            _playerTimer.Start();
            _playerCanMove.Start();
            _enemyTimer.Start();
            _tankCanShoot.Start();
            _enemyCanShoot.Start();
            _bulletTimer.Start();
        }

        private void StopTimers()
        {
            _playerCanMove.Stop();
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

        #endregion

        #region Timers Tick methods
        private void BulletMove(object sender, EventArgs e)
        {
            Game.BulletsMove();
        }

        private void OnEnemyCanShoot(object sender, EventArgs e)
        {
            Game.EnemyCanShoot();
        }

        private void OnTankCanShoot(object sender, EventArgs e)
        {
            Game.TankCanShoot();
        }

        private void EnemyMove(object sender, EventArgs e)
        {
            Game.EnemyMove();
        }

        private void PlayerCanMove(object sender, EventArgs e)
        {
            Game.PlayerCanMove();
        }

        private void PlayerMove(object sender, EventArgs e)
        {
            Game.PlayerMove();
        }

        #endregion
    }
}