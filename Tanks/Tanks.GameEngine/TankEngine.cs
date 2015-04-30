using System.Collections.Generic;

namespace Tanks.GameEngine
{
    public class TankEngine
    {
        private int _shootCounter;
        public List<BulletEngine> Bullets = new List<BulletEngine>();

        public TankEngine()
        {
            ShootEnable = true;
        }

        public bool ShootEnable { get; set; }

        public int ShootCounter
        {
            get { return _shootCounter; }
            set
            {
                if (_shootCounter >= 3)
                {
                    _shootCounter = 0;
                    ShootEnable = true;
                }
                else
                {
                    if (value <= 3)
                    {
                        _shootCounter = value;
                    }
                    else
                    {
                        _shootCounter = 0;
                    }
                }
            }
        }

        public event GameEngineDelegate<List<TankFragment>> TankDraw;
        public event GameEngineDelegate<List<TankFragment>> TankErase;

        //private
        public void OnTankDraw(List<TankFragment> list)
        {
            if (TankDraw != null)
            {
                TankDraw(list);
            }
        }

        //private
        public void OnTankErase(List<TankFragment> list)
        {
            if (TankErase != null)
            {
                TankErase(list);
            }
        }

        public void TankInit(out List<TankFragment> tank, int x, int y, MoveDirection direction, int[,] map)
        {
            tank = TankCreate(x, y, direction);
            foreach (var item in tank)
            {
                map[item.Y, item.X] = 2;
            }
            OnTankDraw(tank);
        }

        public List<TankFragment> TankCreate(int x, int y, MoveDirection direction)
        {
            var tank = new List<TankFragment>();
            for (var i = 0; i < 9; i++)
            {
                var fragment = new TankFragment(0, 0);
                tank.Add(fragment);
            }
            switch (direction)
            {
                case MoveDirection.Up:
                    tank[0].X = x;
                    tank[0].Y = y;
                    tank[1].X = tank[0].X + 1;
                    tank[1].Y = tank[0].Y;
                    tank[2].X = tank[0].X + 2;
                    tank[2].Y = tank[0].Y;
                    tank[3].X = tank[0].X;
                    tank[3].Y = tank[0].Y + 1;
                    tank[4].X = tank[0].X + 1;
                    tank[4].Y = tank[0].Y + 1;
                    tank[5].X = tank[0].X + 2;
                    tank[5].Y = tank[0].Y + 1;
                    tank[6].X = tank[0].X;
                    tank[6].Y = tank[0].Y + 2;
                    tank[7].X = tank[0].X + 1;
                    tank[7].Y = tank[0].Y + 2;
                    tank[8].X = tank[0].X + 2;
                    tank[8].Y = tank[0].Y + 2;
                    break;
                case MoveDirection.Down:
                    tank[0].X = x;
                    tank[0].Y = y;
                    tank[1].X = tank[0].X - 1;
                    tank[1].Y = tank[0].Y;
                    tank[2].X = tank[0].X - 2;
                    tank[2].Y = tank[0].Y;
                    tank[3].X = tank[0].X;
                    tank[3].Y = tank[0].Y - 1;
                    tank[4].X = tank[0].X - 1;
                    tank[4].Y = tank[0].Y - 1;
                    tank[5].X = tank[0].X - 2;
                    tank[5].Y = tank[0].Y - 1;
                    tank[6].X = tank[0].X;
                    tank[6].Y = tank[0].Y - 2;
                    tank[7].X = tank[0].X - 1;
                    tank[7].Y = tank[0].Y - 2;
                    tank[8].X = tank[0].X - 2;
                    tank[8].Y = tank[0].Y - 2;
                    break;
                case MoveDirection.Left:
                    tank[0].X = x;
                    tank[0].Y = y;
                    tank[1].X = tank[0].X;
                    tank[1].Y = tank[0].Y - 1;
                    tank[2].X = tank[0].X;
                    tank[2].Y = tank[0].Y - 2;
                    tank[3].X = tank[0].X + 1;
                    tank[3].Y = tank[0].Y;
                    tank[4].X = tank[0].X + 1;
                    tank[4].Y = tank[0].Y - 1;
                    tank[5].X = tank[0].X + 1;
                    tank[5].Y = tank[0].Y - 2;
                    tank[6].X = tank[0].X + 2;
                    tank[6].Y = tank[0].Y;
                    tank[7].X = tank[0].X + 2;
                    tank[7].Y = tank[0].Y - 1;
                    tank[8].X = tank[0].X + 2;
                    tank[8].Y = tank[0].Y - 2;
                    break;
                case MoveDirection.Right:
                    tank[0].X = x;
                    tank[0].Y = y;
                    tank[1].X = tank[0].X;
                    tank[1].Y = tank[0].Y + 1;
                    tank[2].X = tank[0].X;
                    tank[2].Y = tank[0].Y + 2;
                    tank[3].X = tank[0].X - 1;
                    tank[3].Y = tank[0].Y;
                    tank[4].X = tank[0].X - 1;
                    tank[4].Y = tank[0].Y + 1;
                    tank[5].X = tank[0].X - 1;
                    tank[5].Y = tank[0].Y + 2;
                    tank[6].X = tank[0].X - 2;
                    tank[6].Y = tank[0].Y;
                    tank[7].X = tank[0].X - 2;
                    tank[7].Y = tank[0].Y + 1;
                    tank[8].X = tank[0].X - 2;
                    tank[8].Y = tank[0].Y + 2;
                    break;
            }
            return tank;
        }

        public void TankMove(List<TankFragment> tank, MoveDirection direction, MapBase map)
        {
            OnTankErase(tank);
            TankDeleteFromMap(tank, map);
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
            TankCreateOnMap(tank, map);
            OnTankDraw(tank);
        }

        //private
        public void TankCreateOnMap(List<TankFragment> tank, MapBase map)
        {
            foreach (var t in tank)
            {
                map.GameMap[t.Y, t.X] = 2;
            }
        }

        //private
        public void TankDeleteFromMap(List<TankFragment> tank, MapBase map)
        {
            foreach (var t in tank)
            {
                map.GameMap[t.Y, t.X] = 0;
            }
        }

        public bool DirectionIsBlocked(List<TankFragment> tank, MoveDirection direction, MapBase map)
        {
            var directionIsBlocked = false;
            switch (direction)
            {
                case MoveDirection.Up:
                    if (map.GameMap[tank[0].Y - 1, tank[0].X] == 1 ||
                        map.GameMap[tank[1].Y - 1, tank[1].X] == 1 ||
                        map.GameMap[tank[2].Y - 1, tank[2].X] == 1)
                    {
                        directionIsBlocked = true;
                    }
                    break;
                case MoveDirection.Down:
                    if (map.GameMap[tank[0].Y + 1, tank[0].X] == 1 ||
                        map.GameMap[tank[1].Y + 1, tank[1].X] == 1 ||
                        map.GameMap[tank[2].Y + 1, tank[2].X] == 1)
                    {
                        directionIsBlocked = true;
                    }
                    break;
                case MoveDirection.Left:
                    if (map.GameMap[tank[0].Y, tank[0].X - 1] == 1 ||
                        map.GameMap[tank[1].Y, tank[1].X - 1] == 1 ||
                        map.GameMap[tank[2].Y, tank[2].X - 1] == 1)
                    {
                        directionIsBlocked = true;
                    }
                    break;
                case MoveDirection.Right:
                    if (map.GameMap[tank[0].Y, tank[0].X + 1] == 1 ||
                        map.GameMap[tank[1].Y, tank[1].X + 1] == 1 ||
                        map.GameMap[tank[2].Y, tank[2].X + 1] == 1)
                    {
                        directionIsBlocked = true;
                    }
                    break;
            }
            return directionIsBlocked;
        }

        public bool TankNearBorder(List<TankFragment> tank, MoveDirection direction, List<int> borders)
        {
            var tankNearBorder = false;
            switch (direction)
            {
                case MoveDirection.Up:
                    if (tank[1].Y - 1 == borders[0])
                    {
                        tankNearBorder = true;
                    }
                    break;
                case MoveDirection.Down:
                    if (tank[1].Y + 2 == borders[2])
                    {
                        tankNearBorder = true;
                    }
                    break;
                case MoveDirection.Left:
                    if (tank[1].X - 1 == borders[0])
                    {
                        tankNearBorder = true;
                    }
                    break;
                case MoveDirection.Right:
                    if (tank[1].X + 2 == borders[3])
                    {
                        tankNearBorder = true;
                    }
                    break;
            }
            return tankNearBorder;
        }

        public void TankMoveRight(List<TankFragment> tank)
        {
            tank[0].X = tank[0].X + 1;
            tank[1].X = tank[1].X + 1;
            tank[2].X = tank[2].X + 1;
            tank[3].X = tank[3].X + 1;
            tank[4].X = tank[4].X + 1;
            tank[5].X = tank[5].X + 1;
            tank[6].X = tank[6].X + 1;
            tank[7].X = tank[7].X + 1;
            tank[8].X = tank[8].X + 1;
        }

        public void TankMoveLeft(List<TankFragment> tank)
        {
            tank[0].X = tank[0].X - 1;
            tank[1].X = tank[1].X - 1;
            tank[2].X = tank[2].X - 1;
            tank[3].X = tank[3].X - 1;
            tank[4].X = tank[4].X - 1;
            tank[5].X = tank[5].X - 1;
            tank[6].X = tank[6].X - 1;
            tank[7].X = tank[7].X - 1;
            tank[8].X = tank[8].X - 1;
        }

        public void TankMoveDown(List<TankFragment> tank)
        {
            tank[0].Y = tank[0].Y + 1;
            tank[1].Y = tank[1].Y + 1;
            tank[2].Y = tank[2].Y + 1;
            tank[3].Y = tank[3].Y + 1;
            tank[4].Y = tank[4].Y + 1;
            tank[5].Y = tank[5].Y + 1;
            tank[6].Y = tank[6].Y + 1;
            tank[7].Y = tank[7].Y + 1;
            tank[8].Y = tank[8].Y + 1;
        }

        public void TankMoveUp(List<TankFragment> tank)
        {
            tank[0].Y = tank[0].Y - 1;
            tank[1].Y = tank[1].Y - 1;
            tank[2].Y = tank[2].Y - 1;
            tank[3].Y = tank[3].Y - 1;
            tank[4].Y = tank[4].Y - 1;
            tank[5].Y = tank[5].Y - 1;
            tank[6].Y = tank[6].Y - 1;
            tank[7].Y = tank[7].Y - 1;
            tank[8].Y = tank[8].Y - 1;
        }

        public void Shoot(int x, int y, MoveDirection direction)
        {
            if (ShootEnable)
            {
                MakeShoot(x, y, direction);
                ShootEnable = false;
            }
        }

        //private
        public void MakeShoot(int x, int y, MoveDirection direction)
        {
            var lBulletEngine = new BulletEngine(x, y, direction);
            Bullets.Add(lBulletEngine);
        }

        public void TankTurn(List<TankFragment> tank, MoveDirection direction, MoveDirection lastDirection)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    switch (lastDirection)
                    {
                        case MoveDirection.Up:

                            #region turn_up->left

                            tank[0].X = tank[6].X;
                            tank[0].Y = tank[6].Y;
                            tank[1].X = tank[0].X;
                            tank[1].Y = tank[0].Y - 1;
                            tank[2].X = tank[0].X;
                            tank[2].Y = tank[0].Y - 2;
                            tank[3].X = tank[0].X + 1;
                            tank[3].Y = tank[0].Y;
                            tank[4].X = tank[0].X + 1;
                            tank[4].Y = tank[0].Y - 1;
                            tank[5].X = tank[0].X + 1;
                            tank[5].Y = tank[0].Y - 2;
                            tank[6].X = tank[0].X + 2;
                            tank[6].Y = tank[0].Y;
                            tank[7].X = tank[0].X + 2;
                            tank[7].Y = tank[0].Y - 1;
                            tank[8].X = tank[0].X + 2;
                            tank[8].Y = tank[0].Y - 2;

                            #endregion

                            break;
                        case MoveDirection.Right:

                            #region turn_right->left

                            tank[0].X = tank[8].X;
                            tank[0].Y = tank[8].Y;
                            tank[1].X = tank[0].X;
                            tank[1].Y = tank[0].Y - 1;
                            tank[2].X = tank[0].X;
                            tank[2].Y = tank[0].Y - 2;
                            tank[3].X = tank[0].X + 1;
                            tank[3].Y = tank[0].Y;
                            tank[4].X = tank[0].X + 1;
                            tank[4].Y = tank[0].Y - 1;
                            tank[5].X = tank[0].X + 1;
                            tank[5].Y = tank[0].Y - 2;
                            tank[6].X = tank[0].X + 2;
                            tank[6].Y = tank[0].Y;
                            tank[7].X = tank[0].X + 2;
                            tank[7].Y = tank[0].Y - 1;
                            tank[8].X = tank[0].X + 2;
                            tank[8].Y = tank[0].Y - 2;

                            #endregion

                            break;
                        case MoveDirection.Down:

                            #region turn_down->left

                            tank[0].X = tank[2].X;
                            tank[0].Y = tank[2].Y;
                            tank[1].X = tank[0].X;
                            tank[1].Y = tank[0].Y - 1;
                            tank[2].X = tank[0].X;
                            tank[2].Y = tank[0].Y - 2;
                            tank[3].X = tank[0].X + 1;
                            tank[3].Y = tank[0].Y;
                            tank[4].X = tank[0].X + 1;
                            tank[4].Y = tank[0].Y - 1;
                            tank[5].X = tank[0].X + 1;
                            tank[5].Y = tank[0].Y - 2;
                            tank[6].X = tank[0].X + 2;
                            tank[6].Y = tank[0].Y;
                            tank[7].X = tank[0].X + 2;
                            tank[7].Y = tank[0].Y - 1;
                            tank[8].X = tank[0].X + 2;
                            tank[8].Y = tank[0].Y - 2;

                            #endregion

                            break;
                    }
                    break;
                case MoveDirection.Right:
                    switch (lastDirection)
                    {
                        case MoveDirection.Left:

                            #region turn_left->right

                            tank[0].X = tank[8].X;
                            tank[0].Y = tank[8].Y;
                            tank[1].X = tank[0].X;
                            tank[1].Y = tank[0].Y + 1;
                            tank[2].X = tank[0].X;
                            tank[2].Y = tank[0].Y + 2;
                            tank[3].X = tank[0].X - 1;
                            tank[3].Y = tank[0].Y;
                            tank[4].X = tank[0].X - 1;
                            tank[4].Y = tank[0].Y + 1;
                            tank[5].X = tank[0].X - 1;
                            tank[5].Y = tank[0].Y + 2;
                            tank[6].X = tank[0].X - 2;
                            tank[6].Y = tank[0].Y;
                            tank[7].X = tank[0].X - 2;
                            tank[7].Y = tank[0].Y + 1;
                            tank[8].X = tank[0].X - 2;
                            tank[8].Y = tank[0].Y + 2;

                            #endregion

                            break;
                        case MoveDirection.Up:

                            #region turn_up->right

                            tank[0].X = tank[2].X;
                            tank[0].Y = tank[2].Y;
                            tank[1].X = tank[0].X;
                            tank[1].Y = tank[0].Y + 1;
                            tank[2].X = tank[0].X;
                            tank[2].Y = tank[0].Y + 2;
                            tank[3].X = tank[0].X - 1;
                            tank[3].Y = tank[0].Y;
                            tank[4].X = tank[0].X - 1;
                            tank[4].Y = tank[0].Y + 1;
                            tank[5].X = tank[0].X - 1;
                            tank[5].Y = tank[0].Y + 2;
                            tank[6].X = tank[0].X - 2;
                            tank[6].Y = tank[0].Y;
                            tank[7].X = tank[0].X - 2;
                            tank[7].Y = tank[0].Y + 1;
                            tank[8].X = tank[0].X - 2;
                            tank[8].Y = tank[0].Y + 2;

                            #endregion

                            break;
                        case MoveDirection.Down:

                            #region turn_down->right

                            tank[0].X = tank[6].X;
                            tank[0].Y = tank[6].Y;
                            tank[1].X = tank[0].X;
                            tank[1].Y = tank[0].Y + 1;
                            tank[2].X = tank[0].X;
                            tank[2].Y = tank[0].Y + 2;
                            tank[3].X = tank[0].X - 1;
                            tank[3].Y = tank[0].Y;
                            tank[4].X = tank[0].X - 1;
                            tank[4].Y = tank[0].Y + 1;
                            tank[5].X = tank[0].X - 1;
                            tank[5].Y = tank[0].Y + 2;
                            tank[6].X = tank[0].X - 2;
                            tank[6].Y = tank[0].Y;
                            tank[7].X = tank[0].X - 2;
                            tank[7].Y = tank[0].Y + 1;
                            tank[8].X = tank[0].X - 2;
                            tank[8].Y = tank[0].Y + 2;

                            #endregion

                            break;
                    }
                    break;
                case MoveDirection.Up:
                    switch (lastDirection)
                    {
                        case MoveDirection.Left:

                            #region turn_left->up

                            tank[0].X = tank[2].X;
                            tank[0].Y = tank[2].Y;
                            tank[1].X = tank[0].X + 1;
                            tank[1].Y = tank[0].Y;
                            tank[2].X = tank[0].X + 2;
                            tank[2].Y = tank[0].Y;
                            tank[3].X = tank[0].X;
                            tank[3].Y = tank[0].Y + 1;
                            tank[4].X = tank[0].X + 1;
                            tank[4].Y = tank[0].Y + 1;
                            tank[5].X = tank[0].X + 2;
                            tank[5].Y = tank[0].Y + 1;
                            tank[6].X = tank[0].X;
                            tank[6].Y = tank[0].Y + 2;
                            tank[7].X = tank[0].X + 1;
                            tank[7].Y = tank[0].Y + 2;
                            tank[8].X = tank[0].X + 2;
                            tank[8].Y = tank[0].Y + 2;

                            #endregion

                            break;
                        case MoveDirection.Right:

                            #region turn_right->up

                            tank[0].X = tank[6].X;
                            tank[0].Y = tank[6].Y;
                            tank[1].X = tank[0].X + 1;
                            tank[1].Y = tank[0].Y;
                            tank[2].X = tank[0].X + 2;
                            tank[2].Y = tank[0].Y;
                            tank[3].X = tank[0].X;
                            tank[3].Y = tank[0].Y + 1;
                            tank[4].X = tank[0].X + 1;
                            tank[4].Y = tank[0].Y + 1;
                            tank[5].X = tank[0].X + 2;
                            tank[5].Y = tank[0].Y + 1;
                            tank[6].X = tank[0].X;
                            tank[6].Y = tank[0].Y + 2;
                            tank[7].X = tank[0].X + 1;
                            tank[7].Y = tank[0].Y + 2;
                            tank[8].X = tank[0].X + 2;
                            tank[8].Y = tank[0].Y + 2;

                            #endregion

                            break;
                        case MoveDirection.Down:

                            #region turn_down->up

                            tank[0].X = tank[8].X;
                            tank[0].Y = tank[8].Y;
                            tank[1].X = tank[0].X + 1;
                            tank[1].Y = tank[0].Y;
                            tank[2].X = tank[0].X + 2;
                            tank[2].Y = tank[0].Y;
                            tank[3].X = tank[0].X;
                            tank[3].Y = tank[0].Y + 1;
                            tank[4].X = tank[0].X + 1;
                            tank[4].Y = tank[0].Y + 1;
                            tank[5].X = tank[0].X + 2;
                            tank[5].Y = tank[0].Y + 1;
                            tank[6].X = tank[0].X;
                            tank[6].Y = tank[0].Y + 2;
                            tank[7].X = tank[0].X + 1;
                            tank[7].Y = tank[0].Y + 2;
                            tank[8].X = tank[0].X + 2;
                            tank[8].Y = tank[0].Y + 2;

                            #endregion

                            break;
                    }
                    break;
                case MoveDirection.Down:
                    switch (lastDirection)
                    {
                        case MoveDirection.Left:

                            #region turn_left->down

                            tank[0].X = tank[6].X;
                            tank[0].Y = tank[6].Y;
                            tank[1].X = tank[0].X - 1;
                            tank[1].Y = tank[0].Y;
                            tank[2].X = tank[0].X - 2;
                            tank[2].Y = tank[0].Y;
                            tank[3].X = tank[0].X;
                            tank[3].Y = tank[0].Y - 1;
                            tank[4].X = tank[0].X - 1;
                            tank[4].Y = tank[0].Y - 1;
                            tank[5].X = tank[0].X - 2;
                            tank[5].Y = tank[0].Y - 1;
                            tank[6].X = tank[0].X;
                            tank[6].Y = tank[0].Y - 2;
                            tank[7].X = tank[0].X - 1;
                            tank[7].Y = tank[0].Y - 2;
                            tank[8].X = tank[0].X - 2;
                            tank[8].Y = tank[0].Y - 2;

                            #endregion

                            break;
                        case MoveDirection.Right:

                            #region turn_right->down

                            tank[0].X = tank[2].X;
                            tank[0].Y = tank[2].Y;
                            tank[1].X = tank[0].X - 1;
                            tank[1].Y = tank[0].Y;
                            tank[2].X = tank[0].X - 2;
                            tank[2].Y = tank[0].Y;
                            tank[3].X = tank[0].X;
                            tank[3].Y = tank[0].Y - 1;
                            tank[4].X = tank[0].X - 1;
                            tank[4].Y = tank[0].Y - 1;
                            tank[5].X = tank[0].X - 2;
                            tank[5].Y = tank[0].Y - 1;
                            tank[6].X = tank[0].X;
                            tank[6].Y = tank[0].Y - 2;
                            tank[7].X = tank[0].X - 1;
                            tank[7].Y = tank[0].Y - 2;
                            tank[8].X = tank[0].X - 2;
                            tank[8].Y = tank[0].Y - 2;

                            #endregion

                            break;
                        case MoveDirection.Up:

                            #region turn_up->down

                            tank[0].X = tank[8].X;
                            tank[0].Y = tank[8].Y;
                            tank[1].X = tank[0].X - 1;
                            tank[1].Y = tank[0].Y;
                            tank[2].X = tank[0].X - 2;
                            tank[2].Y = tank[0].Y;
                            tank[3].X = tank[0].X;
                            tank[3].Y = tank[0].Y - 1;
                            tank[4].X = tank[0].X - 1;
                            tank[4].Y = tank[0].Y - 1;
                            tank[5].X = tank[0].X - 2;
                            tank[5].Y = tank[0].Y - 1;
                            tank[6].X = tank[0].X;
                            tank[6].Y = tank[0].Y - 2;
                            tank[7].X = tank[0].X - 1;
                            tank[7].Y = tank[0].Y - 2;
                            tank[8].X = tank[0].X - 2;
                            tank[8].Y = tank[0].Y - 2;

                            #endregion

                            break;
                    }
                    break;
            }
        }
    }
}