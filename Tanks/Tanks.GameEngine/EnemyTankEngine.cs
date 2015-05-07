using System.Collections.Generic;

namespace Tanks.GameEngine
{
    public class EnemyTankEngine : TankEngine
    {
        /*			
            Review VV:
                для типів подій слід використовувати EventHandler<>
        */
        public event GameEngineDelegate<List<TankFragment>> EnemyTankDraw;
        public event GameEngineDelegate<List<TankFragment>> EnemyTankErase;

        public void OnEnemyTankDraw(List<TankFragment> tank)
        {
            if (EnemyTankDraw != null)
            {
                EnemyTankDraw(tank);
            }
        }

        public void OnEnemyTankErase(List<TankFragment> tank)
        {
            if (EnemyTankErase != null)
            {
                EnemyTankErase(tank);
            }
        }

        public void EnemyTankInit(out List<TankFragment> tank, int x, int y, MoveDirection direction, int[,] map)
        {
            tank = TankCreate(x, y, direction);
            foreach (var item in tank)
            {
                map[item.Y, item.X] = 3;
            }
            OnEnemyTankDraw(tank);
        }

        public void EnemyTankChoseActions(out ControlActions enemyTankNextAction, List<TankFragment> enemyTank,
            List<TankFragment> tank, ControlActions enemyTankAction)
        {
            enemyTankNextAction = 0;
            if (enemyTank[4].X > tank[4].X && enemyTank[4].Y > tank[4].Y)
            {
                switch (enemyTankAction)
                {
                    case ControlActions.MoveRight:
                        enemyTankNextAction = ControlActions.MoveUp;
                        break;
                    case ControlActions.MoveLeft:
                        enemyTankNextAction = ControlActions.MoveUp;
                        break;
                    case ControlActions.MoveUp:
                        enemyTankNextAction = ControlActions.MoveLeft;
                        break;
                    case ControlActions.MoveDown:
                        enemyTankNextAction = ControlActions.MoveLeft;
                        break;
                }
            }
            else if (enemyTank[4].X > tank[4].X && enemyTank[4].Y < tank[4].Y)
            {
                switch (enemyTankAction)
                {
                    case ControlActions.MoveRight:
                        enemyTankNextAction = ControlActions.MoveDown;
                        break;
                    case ControlActions.MoveLeft:
                        enemyTankNextAction = ControlActions.MoveDown;
                        break;
                    case ControlActions.MoveUp:
                        enemyTankNextAction = ControlActions.MoveLeft;
                        break;
                    case ControlActions.MoveDown:
                        enemyTankNextAction = ControlActions.MoveLeft;
                        break;
                }
            }
            else if (enemyTank[4].X < tank[4].X && enemyTank[4].Y > tank[4].Y)
            {
                switch (enemyTankAction)
                {
                    case ControlActions.MoveRight:
                        enemyTankNextAction = ControlActions.MoveUp;
                        break;
                    case ControlActions.MoveLeft:
                        enemyTankNextAction = ControlActions.MoveUp;
                        break;
                    case ControlActions.MoveUp:
                        enemyTankNextAction = ControlActions.MoveRight;
                        break;
                    case ControlActions.MoveDown:
                        enemyTankNextAction = ControlActions.MoveRight;
                        break;
                }
            }
            else if (enemyTank[4].X < tank[4].X && enemyTank[4].Y < tank[4].Y)
            {
                switch (enemyTankAction)
                {
                    case ControlActions.MoveRight:
                        enemyTankNextAction = ControlActions.MoveDown;
                        break;
                    case ControlActions.MoveLeft:
                        enemyTankNextAction = ControlActions.MoveDown;
                        break;
                    case ControlActions.MoveUp:
                        enemyTankNextAction = ControlActions.MoveRight;
                        break;
                    case ControlActions.MoveDown:
                        enemyTankNextAction = ControlActions.MoveRight;
                        break;
                }
            }
            else if (enemyTank[4].X == tank[4].X && enemyTank[4].Y > tank[4].Y)
            {
                switch (enemyTankAction)
                {
                    case ControlActions.MoveRight:
                        enemyTankNextAction = ControlActions.MoveUp;
                        break;
                    case ControlActions.MoveLeft:
                        enemyTankNextAction = ControlActions.MoveUp;
                        break;
                    case ControlActions.MoveUp:
                        enemyTankNextAction = ControlActions.Shoot;
                        break;
                    case ControlActions.MoveDown:
                        enemyTankNextAction = ControlActions.MoveLeft;
                        break;
                }
            }
            else if (enemyTank[4].X > tank[4].X && enemyTank[4].Y == tank[4].Y)
            {
                switch (enemyTankAction)
                {
                    case ControlActions.MoveRight:
                        enemyTankNextAction = ControlActions.MoveUp;
                        break;
                    case ControlActions.MoveLeft:
                        enemyTankNextAction = ControlActions.Shoot;
                        break;
                    case ControlActions.MoveUp:
                        enemyTankNextAction = ControlActions.MoveLeft;
                        break;
                    case ControlActions.MoveDown:
                        enemyTankNextAction = ControlActions.MoveLeft;
                        break;
                }
            }
            else if (enemyTank[4].X == tank[4].X && enemyTank[4].Y < tank[4].Y)
            {
                switch (enemyTankAction)
                {
                    case ControlActions.MoveRight:
                        enemyTankNextAction = ControlActions.MoveDown;
                        break;
                    case ControlActions.MoveLeft:
                        enemyTankNextAction = ControlActions.MoveDown;
                        break;
                    case ControlActions.MoveUp:
                        enemyTankNextAction = ControlActions.MoveLeft;
                        break;
                    case ControlActions.MoveDown:
                        enemyTankNextAction = ControlActions.Shoot;
                        break;
                }
            }
            else if (enemyTank[4].X < tank[4].X && enemyTank[4].Y == tank[4].Y)
            {
                switch (enemyTankAction)
                {
                    case ControlActions.MoveRight:
                        enemyTankNextAction = ControlActions.Shoot;
                        break;
                    case ControlActions.MoveLeft:
                        enemyTankNextAction = ControlActions.MoveUp;
                        break;
                    case ControlActions.MoveUp:
                        enemyTankNextAction = ControlActions.MoveRight;
                        break;
                    case ControlActions.MoveDown:
                        enemyTankNextAction = ControlActions.MoveRight;
                        break;
                }
            }
        }

        public new void TankMove(List<TankFragment> tank, MoveDirection direction,
            MapBase map)
        {
            OnEnemyTankErase(tank);
            EnemyTankDeleteFromMap(tank, map);
            if (DirectionIsBlocked(tank, direction, map))
            {
                Shoot(tank[1].X, tank[1].Y, direction);
            }
            if (!DirectionIsBlocked(tank, direction, map) && !TankNearBorder(tank, direction, map.Borders))
            {
                switch (direction)
                {
                    case MoveDirection.Up:
                        TankMoveUp(tank);
                        break;
                    case MoveDirection.Down:
                        TankMoveDown(tank);
                        break;
                    case MoveDirection.Left:
                        TankMoveLeft(tank);
                        break;
                    case MoveDirection.Right:
                        TankMoveRight(tank);
                        break;
                }
            }
            OnEnemyTankDraw(tank);
            EnemyTankCreateOnMap(tank, map);
        }

        public void EnemyTankCreateOnMap(List<TankFragment> tank, MapBase map)
        {
            foreach (var t in tank)
            {
                map.GameMap[t.Y, t.X] = 3;
            }
        }

        public void EnemyTankDeleteFromMap(List<TankFragment> tank, MapBase map)
        {
            foreach (var t in tank)
            {
                map.GameMap[t.Y, t.X] = 0;
            }
        }

        public MoveDirection Control(out ControlActions enemyAction, List<TankFragment> enemyTank,
            List<TankFragment> myTank, EnemyTankEngine tank, ControlActions enemyTankLastAction,
            MoveDirection direction, BulletEngine bullet, MapBase map)
        {
            ControlActions nextEnemyAction;
            MoveDirection nextEnemyDirection = 0;
            EnemyTankChoseActions(out nextEnemyAction, enemyTank, myTank, enemyTankLastAction);
            if (nextEnemyAction == ControlActions.Shoot)
            {
                tank.Shoot(enemyTank[1].X, enemyTank[1].Y, direction);
                enemyAction = enemyTankLastAction;
            }
            else
            {
                enemyAction = nextEnemyAction;
            }
            switch (enemyAction)
            {
                case ControlActions.MoveUp:
                    nextEnemyDirection = MoveDirection.Up;
                    break;
                case ControlActions.MoveDown:
                    nextEnemyDirection = MoveDirection.Down;
                    break;
                case ControlActions.MoveLeft:
                    nextEnemyDirection = MoveDirection.Left;
                    break;
                case ControlActions.MoveRight:
                    nextEnemyDirection = MoveDirection.Right;
                    break;
            }
            return nextEnemyDirection;
        }
    }
}