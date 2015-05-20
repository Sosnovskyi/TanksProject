using System.Collections.Generic;
using System.Timers;

namespace TanksGame.TanksEngine
{
    public delegate void GameEngineDelegate<T>(T element);
    #region PublicEnums
    public enum GameControl
    {
        StartGame = 0,
        EndGame = 1,
        PauseGame = 2,
        MoveLeft = 3,
        MoveRight = 4,
        MoveUp = 5,
        MoveDown = 6,
        TankShoot = 7,
        ResumeGame = 8,
        DafaultAction = 9
    }
    public enum MapObject
    {
        FreeSpace = 0,
        Wall = 1,
        Player = 2,
        Enemy = 3,
        Bullet = 4,
        Default = 5
    }
    public enum GameStatus
    {
        InProgress = 0,
        Paused = 1,
        Stopped = 2,
        GameEnd = 3
    }

    public enum Platform
    {
        Console,
        WinForms
    }

    public enum ExitStatus
    {
        WinGame = 0,
        LoseGame = 1,
        StopGame = 2,
        DefaultStatus = 3
    }
    #endregion
    public class Game
    {
        #region Fields

        private readonly EnemyTank _enemy;

        private readonly Tank _player;

        private readonly Control _control;

        private readonly Map _map;

        private readonly Bullet _bullet;

        private GameStatus _gameStatus;

        private ExitStatus _exitStatus;

        #endregion

        #region Events

        public delegate void GameProcess();

        public event GameEngineDelegate<ExitStatus> ExitGame;

        public event GameProcess StopGame;

        public event GameProcess StartGame;

        public event GameProcess Process;


        #endregion

        #region Constructor
        public Game(Tank tank, EnemyTank enemy, Control control, Map map, Bullet bullet)
        {
            _player = tank;
            _control = control;
            _enemy = enemy;
            _map = map;
            _bullet = bullet;
        }

        #endregion

        #region Game process handlers

        public void Start(Platform platform)
        {
            _map.OnDrawMap();
            _map.OnDrawBorders();
            InitObjects();
            OnStartGame();
            _gameStatus = GameStatus.InProgress;
            PlayingProcess(platform);
        }

        private void PlayingProcess(Platform platform)
        {
            switch (platform)
            {
                case Platform.Console:
                    PlayerControl();
                    break;
            }
        }

        private void PlayerControl()
        {
            while (_gameStatus != GameStatus.GameEnd)
            {
                if (_gameStatus == GameStatus.Paused)
                {
                    var action = _control.OnGetAction();
                    while (action != GameControl.ResumeGame && action != GameControl.EndGame)
                    {
                        action = _control.OnGetAction();
                    }
                }
                if (_gameStatus == GameStatus.InProgress)
                {
                    PlayerMove();
                }
            }
        }

        private void InitObjects()
        {
            _player.TankInit();
            _player.OnTankDraw();
            _player.OnHealthDraw(_player.Health);
            _enemy.TankInit();
            _enemy.OnEnemyTankDraw();
            _enemy.OnEnemyHealthDraw(_enemy.Health);
        }

        #endregion

        #region Timer Handlers

        public void PlayerCanMove()
        {
            _player.CanMove = true;
        }

        public void TankCanShoot()
        {
            _player.CanShoot = true;
         }

        public void EnemyCanShoot()
        {
            _enemy.CanShoot = true;
        }

        #endregion

        #region Objects move
        public void PlayerMove()
        {
            var action = _control.OnGetAction();
            switch (action)
            {
                case GameControl.MoveUp:
                    if (_gameStatus == GameStatus.InProgress)
                    {
                        _player.Direction = GameControl.MoveUp;
                        _player.TankMove(_map.GameSpace, _map.GameMap);
                    }
                    break;
                case GameControl.MoveDown:
                    if (_gameStatus == GameStatus.InProgress)
                    {
                        _player.Direction = GameControl.MoveDown;
                        _player.TankMove(_map.GameSpace, _map.GameMap);
                    }
                    break;
                case GameControl.MoveLeft:
                    if (_gameStatus == GameStatus.InProgress)
                    {
                        _player.Direction = GameControl.MoveLeft;
                        _player.TankMove(_map.GameSpace, _map.GameMap);
                    }
                    break;
                case GameControl.MoveRight:
                    if (_gameStatus == GameStatus.InProgress)
                    {
                        _player.Direction = GameControl.MoveRight;
                        _player.TankMove(_map.GameSpace, _map.GameMap);
                    }
                    break;
                case GameControl.TankShoot:
                    if (_gameStatus == GameStatus.InProgress)
                    {
                        _player.Shoot();
                    }
                    break;
                case GameControl.PauseGame:
                    _gameStatus = GameStatus.Paused;
                    OnStopGame();
                    break;
                case GameControl.ResumeGame:
                    _gameStatus = GameStatus.InProgress;
                    OnStartGame();
                    break;
                case GameControl.EndGame:
                    _gameStatus = GameStatus.GameEnd;
                    OnStopGame();
                    OnExitGame(ExitStatus.StopGame);
                    break;
            }
        }

        public void EnemyMove()
        {
            _enemy.ChooseAction(_player.TankModel);
            _enemy.TankMove(_map.GameSpace, _map.GameMap);
        }

        public void BulletsMove()
        {
            BulletsColision();
            TankHit(false);
            if (_gameStatus == GameStatus.InProgress)
            {
                _bullet.BulletsMove(_player.Bullets, _map);
            }
            BulletsColision();
            TankHit(true);
            if (_gameStatus == GameStatus.InProgress)
            {
                _bullet.BulletsMove(_enemy.Bullets, _map);
            }
        }

        #endregion

        #region Objects collision

        private void BulletsColision()
        {
            for (int i = 0; i < _player.Bullets.Count; i++)
            {
                var bullet = _player.Bullets[i];
                for (int j = 0; j < _enemy.Bullets.Count; j++)
                {
                    var enemyBullet = _enemy.Bullets[j];
                    switch (bullet.Direction)
                    {
                            case GameControl.MoveUp:
                            if (bullet.PointY + 1 == enemyBullet.PointY)
                            {
                                BulletsCrash(bullet, enemyBullet);
                            }
                            break;
                            case GameControl.MoveDown:
                            if (bullet.PointY - 1 == enemyBullet.PointY)
                            {
                                BulletsCrash(bullet, enemyBullet);
                            }
                            break;
                            case GameControl.MoveLeft:
                            if (bullet.PointX - 1 == enemyBullet.PointX)
                            {
                                BulletsCrash(bullet, enemyBullet);
                            }
                            break;
                            case GameControl.MoveRight:
                            if (bullet.PointX + 1 == enemyBullet.PointX)
                            {
                                BulletsCrash(bullet, enemyBullet);
                            }
                            break;
                    }
                }
            }
        }

        private void BulletsCrash(Bullet bullet, Bullet enemyBullet)
        {
            _bullet.OnBulletErase(new Coordinate(bullet.PointX, bullet.PointY));
            _player.Bullets.Remove(bullet);
            _bullet.OnBulletErase(new Coordinate(enemyBullet.PointX, enemyBullet.PointY));
            _enemy.Bullets.Remove(enemyBullet);
        }

        private void TankHit(bool enemy)
        {
            var bullets = new List<Bullet>();
            var tank = new List<TankFragment>();
            if (enemy)
            {
                bullets = _player.Bullets;
                tank = _enemy.TankModel;
            }
            else
            {
                bullets = _enemy.Bullets;
                tank = _player.TankModel;
            }
            if (bullets.Count > 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    var bullet = bullets[i];
                    switch (bullet.Direction)
                    {
                        case GameControl.MoveUp:
                            if (tank.Exists(t => t.PointX == bullet.PointX && t.PointY == bullet.PointY - 1))
                            {
                                TankCrash(bullet, bullets, enemy);
                                TankReInit(enemy);
                            }
                            break;
                        case GameControl.MoveDown:
                            if (tank.Exists(t => t.PointX == bullet.PointX && t.PointY == bullet.PointY + 1))
                            {
                                TankCrash(bullet, bullets, enemy);
                                TankReInit(enemy);
                            }
                            break;
                        case GameControl.MoveLeft:
                            if (tank.Exists(t => t.PointX == bullet.PointX - 1 && t.PointY == bullet.PointY))
                            {
                                TankCrash(bullet, bullets, enemy);
                                TankReInit(enemy);
                            }
                            break;
                        case GameControl.MoveRight:
                            if (tank.Exists(t => t.PointX == bullet.PointX + 1 && t.PointY == bullet.PointY))
                            {
                                TankCrash(bullet, bullets, enemy);
                                TankReInit(enemy);
                            }
                            break;
                    }
                }
            }
        }

        private void TankReInit(bool enemy)
        {
            if (_gameStatus == GameStatus.InProgress)
            {
                if (enemy)
                {
                    _enemy.OnEnemyTankErase();
                    _enemy.TankModel.Clear();
                    _enemy.TankInit();
                }
                else
                {
                    _player.OnTankErase();
                    _player.TankModel.Clear();
                    _player.TankInit();
                }
            }
        }

        private void TankCrash(Bullet bullet, List<Bullet> bullets, bool enemy)
        {
            _bullet.OnBulletErase(new Coordinate(bullet.PointX, bullet.PointY));
            bullets.Remove(bullet);
            if (enemy)
            {
                if (_enemy.Health > 1)
                {
                    _enemy.Health--;
                    _enemy.OnEnemyHealthDraw(_enemy.Health);
                }
                else if (_enemy.Health == 1)
                {
                    _enemy.Health--;
                    _enemy.OnEnemyHealthDraw(_enemy.Health);
                    OnStopGame();
                    OnExitGame(ExitStatus.WinGame);
                }
            }
            else
            {
                if (_player.Health > 1)
                {
                    _player.Health--;
                    _player.OnHealthDraw(_player.Health);
                }
                else if (_player.Health == 1)
                {
                    _player.Health--;
                    _player.OnHealthDraw(_player.Health);
                    OnStopGame();
                    OnExitGame(ExitStatus.LoseGame);
                }
            }
        }
        #endregion

        #region Event handlers

        public void OnExitGame(ExitStatus status)
        {
            _gameStatus = GameStatus.GameEnd;
            if (ExitGame != null)
            {
                ExitGame(status);
            }
        }

        public void OnStopGame()
        {
            if (StopGame != null)
            {
                StopGame();
            }
        }

        public void OnStartGame()
        {
            if (StartGame != null)
            {
                StartGame();
            }
        }

        public void OnProcess()
        {
            if (Process != null)
            {
                Process();
            }
        }

        #endregion
    }
}