using System.Collections.Generic;

namespace TanksGame.TanksEngine
{
    public class EnemyTank: Tank
    {
        #region Fields

        private GameControl _enemyStartDirection;

        #endregion

        #region Public events
        public event GameEngineDelegate<List<TankFragment>> EnemyTankDraw;
        public event GameEngineDelegate<List<TankFragment>> EnemyTankErase;
        public event GameEngineDelegate<int> EnemyHealthDraw;
        #endregion

        #region Constructor

        public EnemyTank(int initPointX, int initPointY, GameControl enemyStartDirection, int health)
        {
            InitPointX = initPointX;
            InitPointY = initPointY;
            _enemyStartDirection = enemyStartDirection;
            Direction = enemyStartDirection;
            LastDirection = enemyStartDirection;
            Health = health;
            TankInit();
            Bullets = new List<Bullet>();
            CanMove = true;
        }
        #endregion

        #region Enemy tank model move

        public new void TankMove(List<int> borders, Dictionary<Coordinate, MapObject> map)
        {
            OnEnemyTankErase();
            NeedTurn();
            if (NearWall(map))
            {
                Shoot();
            }
            if (!DirectionBlocked(borders, map))
            {
                switch (Direction)
                {
                    case GameControl.MoveUp:
                        TankMoveUp();
                        break;
                    case GameControl.MoveDown:
                        TankMoveDown();
                        break;
                    case GameControl.MoveLeft:
                        TankMoveLeft();
                        break;
                    case GameControl.MoveRight:
                        TankMoveRight();
                        break;
                }
            }
            OnEnemyTankDraw();
        }

        #endregion

        #region Enemy choose action

        public void ChooseAction(List<TankFragment> player)
        {
            int pointXDifference;
            int pointYDifference;
            if (TankModel[4].PointX > player[4].PointX && TankModel[4].PointY > player[4].PointY)
            {
                pointXDifference = TankModel[4].PointX - player[4].PointX;
                pointYDifference = TankModel[4].PointY - player[4].PointY;
                LastDirection = Direction;
                Direction = pointXDifference > pointYDifference ? GameControl.MoveUp : GameControl.MoveLeft;
            }
            if (TankModel[4].PointX > player[4].PointX && TankModel[4].PointY < player[4].PointY)
            {
                pointXDifference = TankModel[4].PointX - player[4].PointX;
                pointYDifference = player[4].PointY - TankModel[4].PointY;
                LastDirection = Direction;
                Direction = pointXDifference > pointYDifference ? GameControl.MoveDown : GameControl.MoveLeft;
            }
            if (TankModel[4].PointX < player[4].PointX && TankModel[4].PointY < player[4].PointY)
            {
                pointXDifference = player[4].PointX - TankModel[4].PointX;
                pointYDifference = player[4].PointY - TankModel[4].PointY;
                LastDirection = Direction;
                Direction = pointXDifference > pointYDifference ? GameControl.MoveDown : GameControl.MoveRight;
            }
            if (TankModel[4].PointX < player[4].PointX && TankModel[4].PointY > player[4].PointY)
            {
                pointXDifference = player[4].PointX - TankModel[4].PointX;
                pointYDifference = TankModel[4].PointY - player[4].PointY;
                LastDirection = Direction;
                Direction = pointXDifference > pointYDifference ? GameControl.MoveUp : GameControl.MoveRight;
            }
            if (TankModel[4].PointX < player[4].PointX && TankModel[4].PointY == player[4].PointY)
            {
                LastDirection = Direction;
                Direction = GameControl.MoveRight;
                NeedTurn();
                Shoot();
            }
            if (TankModel[4].PointX > player[4].PointX && TankModel[4].PointY == player[4].PointY)
            {
                LastDirection = Direction;
                Direction = GameControl.MoveLeft;
                NeedTurn();
                Shoot();
            }
            if (TankModel[4].PointX == player[4].PointX && TankModel[4].PointY < player[4].PointY)
            {
                LastDirection = Direction;
                Direction = GameControl.MoveDown;
                NeedTurn();
                Shoot();
            }
            if (TankModel[4].PointX == player[4].PointX && TankModel[4].PointY > player[4].PointY)
            {
                LastDirection = Direction;
                Direction = GameControl.MoveUp;
                NeedTurn();
                Shoot();
            }
        }


        #endregion

        #region Handlers
        public void OnEnemyTankDraw()
        {
            if (EnemyTankDraw != null)
            {
                EnemyTankDraw(TankModel);
            }
        }

        public void OnEnemyTankErase()
        {
            if (EnemyTankErase != null)
            {
                EnemyTankErase(TankModel);
            }
        }

        public void OnEnemyHealthDraw(int health)
        {
            if (EnemyHealthDraw != null)
            {
                EnemyHealthDraw(health);
            }
        }
        #endregion
    }
}