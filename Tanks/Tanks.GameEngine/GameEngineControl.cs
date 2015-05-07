using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Tanks.GameEngine
{
    public delegate void GameEngineDelegate<T>(T element);

    public enum MoveDirection
    {
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }

    public enum ControlActions
    {
        MoveLeft = 1,
        MoveRight = 2,
        MoveUp = 3,
        MoveDown = 4,
        Shoot = 5
    }

    public enum DirectionBlocked
    {
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }

    public class GameEngineControl
    {
        /*			
			Review VV:
			    ці поля повинні бути нестатичними і приватними
		*/
        public List<TankFragment> MyTank = new List<TankFragment>();
        public List<TankFragment> EnemyTank = new List<TankFragment>();
        private ControlActions _action;
        private MoveDirection _direction;
        private ControlActions _enemyAction;
        private MoveDirection _enemyDirection;
        private MoveDirection _lastDirection;
        private MoveDirection _lastEnemyDirection;
        public string GameOverMessage;
        private readonly BulletEngine _bullet;
        private readonly Timer _bulletsTimer;
        private readonly ControlBase _controlBase;
        private readonly EnemyTankEngine _enemyTank;
        private readonly MapBase _map;
        private readonly TankEngine _tank;
        private readonly Timer _tanksTimer;
        private readonly int _bullesSpeed = 200;
        private int _tanksSpeed = 500;

        public GameEngineControl(TankEngine tank, EnemyTankEngine enemyTank, BulletEngine bullet,
            ControlBase controlBase,
            MapBase map, MoveDirection direction, MoveDirection enemyDirection)
        {
            PlayerWin = false;
            GameOver = false;
            _tank = tank;
            _enemyTank = enemyTank;
            _bullet = bullet;
            _controlBase = controlBase;
            _map = map;
            _lastDirection = direction;
            _direction = direction;
            _lastEnemyDirection = enemyDirection;
            _enemyDirection = enemyDirection;

            _bulletsTimer = new Timer(10000);
            _bulletsTimer.Elapsed += BulletsTimerOnElapsed;
            _bulletsTimer.Interval = _bullesSpeed;

            _tanksTimer = new Timer(10000);
            _tanksTimer.Elapsed += TanksTimerOnElapsed;
            _tanksTimer.Interval = _tanksSpeed;
            switch (direction)
            {
                case MoveDirection.Up:
                    _action = ControlActions.MoveUp;
                    break;
                case MoveDirection.Down:
                    _action = ControlActions.MoveDown;
                    break;
                case MoveDirection.Right:
                    _action = ControlActions.MoveRight;
                    break;
                case MoveDirection.Left:
                    _action = ControlActions.MoveLeft;
                    break;
            }
            switch (enemyDirection)
            {
                case MoveDirection.Up:
                    _enemyAction = ControlActions.MoveUp;
                    break;
                case MoveDirection.Down:
                    _enemyAction = ControlActions.MoveDown;
                    break;
                case MoveDirection.Right:
                    _enemyAction = ControlActions.MoveRight;
                    break;
                case MoveDirection.Left:
                    _enemyAction = ControlActions.MoveLeft;
                    break;
            }
        }

        public bool GameOver { get; set; }
        public bool PlayerWin { get; set; }

        private void TanksTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _tank.TankTurn(MyTank, _direction, _lastDirection);
            _tank.TankMove(MyTank, _direction, _map);
            _tank.ShootCounter++;
            _enemyDirection = _enemyTank.Control(out _enemyAction, EnemyTank, MyTank, _enemyTank, _enemyAction,
                _enemyDirection, _bullet, _map);
            _enemyTank.TankTurn(EnemyTank, _enemyDirection, _lastEnemyDirection);
            _enemyTank.TankMove(EnemyTank, _enemyDirection, _map);
            _enemyTank.ShootCounter++;
            _lastDirection = _direction;
            _lastEnemyDirection = _enemyDirection;
            _tanksTimer.Enabled = !GameOver;
        }

        public event GameEngineDelegate<string> GameIsOver;

        public void OnGameIsOver(string message)
        {
            if (GameIsOver != null)
            {
                GameIsOver(message);
            }
        }

        public void GameProcess(int initTankX, int initTankY, int initEnemyTankX, int initEnemyTankY,
            int topLeftBorder, int topRightBorder, int bottomLeftBorder, int bottomRightBorder)
        {
            _map.OnDrawMap(_map.GameMap);
            _map.SetBorders(topLeftBorder, topRightBorder, bottomLeftBorder, bottomRightBorder);
            _map.OnDrawBorders(_map.Borders);
            _tank.TankInit(out MyTank, initTankX, initTankY, _direction, _map.GameMap);
            _enemyTank.EnemyTankInit(out EnemyTank, initEnemyTankX, initEnemyTankY, _enemyDirection, _map.GameMap);
            GameProcess();
            GameOverMessage = PlayerWin ? "Game is over. You win" : "Game is over. You lose";
            OnGameIsOver(GameOverMessage);
        }

        public void GameControl()
        {
            while (!GameOver)
            {
                if (GameOver)
                {
                    break;
                }
                _action = _controlBase.GetAction();
                switch (_action)
                {
                    case ControlActions.MoveUp:
                        _direction = MoveDirection.Up;
                        break;
                    case ControlActions.MoveDown:
                        _direction = MoveDirection.Down;
                        break;
                    case ControlActions.MoveLeft:
                        _direction = MoveDirection.Left;
                        break;
                    case ControlActions.MoveRight:
                        _direction = MoveDirection.Right;
                        break;
                    case ControlActions.Shoot:
                        _tank.Shoot(MyTank[1].X, MyTank[1].Y, _direction);
                        break;
                }
            }
        }

        public void GameProcess()
        {
            while (!GameOver)
            {
                _tanksTimer.Enabled = true;
                _bulletsTimer.Enabled = true;
                GameControl();
            }
            if (GameOver)
            {
                OnGameIsOver(GameOverMessage);
            }
        }

        public void BulletsTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            BulletsCollisionHandler(_tank.Bullets, out _enemyTank.Bullets, _map, false);
            BulletAndTankColisionHandler(_tank.Bullets, true, _map);
            _bullet.BulletsMove(_tank.Bullets, _map);
            BulletsCollisionHandler(_enemyTank.Bullets, out _tank.Bullets, _map, true);
            BulletAndTankColisionHandler(_enemyTank.Bullets, false, _map);
            _bullet.BulletsMove(_enemyTank.Bullets, _map);
            _bulletsTimer.Enabled = !GameOver;
        }
         
        public void BulletsCollisionHandler(List<BulletEngine> bullets, out List<BulletEngine> enemyBullets,
            MapBase map, bool enemy)
        {
            var lEnemyBullets = enemy ? _tank.Bullets : _enemyTank.Bullets;
            for (var i = 0; i < bullets.Count; i++)
            {
                switch (bullets[i].Direction)
                {
                    case MoveDirection.Up:
                        if (map.GameMap[bullets[i].Y - 1, bullets[i].X] == 4)
                        {
                            var bullet = lEnemyBullets.SingleOrDefault(bull => bull.X == bullets[i].X &
                                                                              bull.Y == bullets[i].Y - 1);
                            if (bullet != null)
                            {
                                _bullet.OnBulletErase(bullet);
                                _bullet.BulletDeleteFromMap(bullet, map);
                                lEnemyBullets.Remove(bullet);
                            }
                            _bullet.OnBulletErase(bullets[i]);
                            _bullet.BulletDeleteFromMap(bullets[i], map);
                            bullets.Remove(bullets[i]);
                        }
                        break;
                    case MoveDirection.Down:
                        if (map.GameMap[bullets[i].Y + 1, bullets[i].X] == 4)
                        {
                            var bullet = lEnemyBullets.SingleOrDefault(bull => bull.X == bullets[i].X &
                                                                              bull.Y == bullets[i].Y + 1);
                            if (bullet != null)
                            {
                                _bullet.OnBulletErase(bullet);
                                _bullet.BulletDeleteFromMap(bullet, map);
                                lEnemyBullets.Remove(bullet);
                            }
                            _bullet.OnBulletErase(bullets[i]);
                            _bullet.BulletDeleteFromMap(bullets[i], map);
                            bullets.Remove(bullets[i]);
                        }
                        break;
                    case MoveDirection.Left:
                        if (map.GameMap[bullets[i].Y, bullets[i].X - 1] == 4)
                        {
                            var bullet = lEnemyBullets.SingleOrDefault(bull => bull.X == bullets[i].X - 1 &
                                                                              bull.Y == bullets[i].Y);
                            if (bullet != null)
                            {
                                _bullet.OnBulletErase(bullet);
                                _bullet.BulletDeleteFromMap(bullet, map);
                                lEnemyBullets.Remove(bullet);
                            }
                            _bullet.OnBulletErase(bullets[i]);
                            _bullet.BulletDeleteFromMap(bullets[i], map);
                            bullets.Remove(bullets[i]);
                        }
                        break;
                    case MoveDirection.Right:
                        if (map.GameMap[bullets[i].Y, bullets[i].X + 1] == 4)
                        {
                            var bullet = lEnemyBullets.SingleOrDefault(bull => bull.X == bullets[i].X + 1 &
                                                                              bull.Y == bullets[i].Y);
                            if (bullet != null)
                            {
                                _bullet.OnBulletErase(bullet);
                                _bullet.BulletDeleteFromMap(bullet, map);
                                lEnemyBullets.Remove(bullet);
                            }
                            _bullet.OnBulletErase(bullets[i]);
                            _bullet.BulletDeleteFromMap(bullets[i], map);
                            bullets.Remove(bullets[i]);
                        }
                        break;
                }
            }
            enemyBullets = lEnemyBullets;
        }

        public void BulletAndTankColisionHandler(List<BulletEngine> bullets, bool enemy, MapBase map)
        {
            var tankId = enemy ? 3 : 2;
            foreach (var bul in bullets)
            {
                switch (bul.Direction)
                {
                    case MoveDirection.Up:
                        if (map.GameMap[bul.Y - 1, bul.X] == tankId)
                        {
                            GameOver = true;
                            if (enemy)
                            {
                                PlayerWin = true;
                            }
                        }
                        break;
                    case MoveDirection.Down:
                        if (map.GameMap[bul.Y + 1, bul.X] == tankId)
                        {
                            GameOver = true;
                            if (enemy)
                            {
                                PlayerWin = true;
                            }
                        }
                        break;
                    case MoveDirection.Left:
                        if (map.GameMap[bul.Y, bul.X - 1] == tankId)
                        {
                            GameOver = true;
                            if (enemy)
                            {
                                PlayerWin = true;
                            }
                        }
                        break;
                    case MoveDirection.Right:
                        if (map.GameMap[bul.Y, bul.X + 1] == tankId)
                        {
                            GameOver = true;
                            if (enemy)
                            {
                                PlayerWin = true;
                            }
                        }
                        break;
                }
            }
        }
    }
}