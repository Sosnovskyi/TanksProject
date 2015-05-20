using System.Collections.Generic;
using System.Linq;

namespace TanksGame.TanksEngine
{
    public class Tank
    {
        #region Fields

        private readonly GameControl _tankStartDirection;

        #endregion

        #region Public events

        public event GameEngineDelegate<List<TankFragment>> TankDraw;

        public event GameEngineDelegate<List<TankFragment>> TankErase;

        public event GameEngineDelegate<int> HealthDraw;

        #endregion

        #region Properties
        public List<TankFragment> TankModel { get; set; }

        public GameControl Direction { get; set; }

        public GameControl LastDirection { get; set; }

        public List<Bullet> Bullets { get; set; }

        public int InitPointX { get; set; }

        public int InitPointY { get; set; }

        public bool CanMove { get; set; }

        public bool CanShoot { get; set; }

        public int Health { get; set; }

        #endregion

        #region Constructor

        public Tank(int initPointX, int initPointY, GameControl startDirection, int health)
        {
            InitPointX = initPointX;
            InitPointY = initPointY;
            _tankStartDirection = startDirection;
            Direction = startDirection;
            LastDirection = startDirection;
            Bullets = new List<Bullet>();
            Health = health;
            CanMove = true;
        }

        protected Tank() { }
        
        #endregion

        #region TankModelInit

        public void TankInit()
        {
            TankModel = new List<TankFragment>();
            TankCreate(Direction);
            OnTankDraw();
        }

        protected void TankCreate(GameControl direction)
        {
            for (var i = 0; i < 9; i++)
            {
                var fragment = new TankFragment(0, 0);
                TankModel.Add(fragment);
            }
            switch (direction)
            {
                case GameControl.MoveUp:
                    TankModel[0].PointX = InitPointX;
                    TankModel[0].PointY = InitPointY;
                    TankModel[1].PointX = TankModel[0].PointX + 1;
                    TankModel[1].PointY = TankModel[0].PointY;
                    TankModel[2].PointX = TankModel[0].PointX + 2;
                    TankModel[2].PointY = TankModel[0].PointY;
                    TankModel[3].PointX = TankModel[0].PointX;
                    TankModel[3].PointY = TankModel[0].PointY + 1;
                    TankModel[4].PointX = TankModel[0].PointX + 1;
                    TankModel[4].PointY = TankModel[0].PointY + 1;
                    TankModel[5].PointX = TankModel[0].PointX + 2;
                    TankModel[5].PointY = TankModel[0].PointY + 1;
                    TankModel[6].PointX = TankModel[0].PointX;
                    TankModel[6].PointY = TankModel[0].PointY + 2;
                    TankModel[7].PointX = TankModel[0].PointX + 1;
                    TankModel[7].PointY = TankModel[0].PointY + 2;
                    TankModel[8].PointX = TankModel[0].PointX + 2;
                    TankModel[8].PointY = TankModel[0].PointY + 2;
                    break;
                case GameControl.MoveDown:
                    TankModel[0].PointX = InitPointX;
                    TankModel[0].PointY = InitPointY;
                    TankModel[1].PointX = TankModel[0].PointX - 1;
                    TankModel[1].PointY = TankModel[0].PointY;
                    TankModel[2].PointX = TankModel[0].PointX - 2;
                    TankModel[2].PointY = TankModel[0].PointY;
                    TankModel[3].PointX = TankModel[0].PointX;
                    TankModel[3].PointY = TankModel[0].PointY - 1;
                    TankModel[4].PointX = TankModel[0].PointX - 1;
                    TankModel[4].PointY = TankModel[0].PointY - 1;
                    TankModel[5].PointX = TankModel[0].PointX - 2;
                    TankModel[5].PointY = TankModel[0].PointY - 1;
                    TankModel[6].PointX = TankModel[0].PointX;
                    TankModel[6].PointY = TankModel[0].PointY - 2;
                    TankModel[7].PointX = TankModel[0].PointX - 1;
                    TankModel[7].PointY = TankModel[0].PointY - 2;
                    TankModel[8].PointX = TankModel[0].PointX - 2;
                    TankModel[8].PointY = TankModel[0].PointY - 2;
                    break;
                case GameControl.MoveLeft:
                    TankModel[0].PointX = InitPointX;
                    TankModel[0].PointY = InitPointY;
                    TankModel[1].PointX = TankModel[0].PointX;
                    TankModel[1].PointY = TankModel[0].PointY - 1;
                    TankModel[2].PointX = TankModel[0].PointX;
                    TankModel[2].PointY = TankModel[0].PointY - 2;
                    TankModel[3].PointX = TankModel[0].PointX + 1;
                    TankModel[3].PointY = TankModel[0].PointY;
                    TankModel[4].PointX = TankModel[0].PointX + 1;
                    TankModel[4].PointY = TankModel[0].PointY - 1;
                    TankModel[5].PointX = TankModel[0].PointX + 1;
                    TankModel[5].PointY = TankModel[0].PointY - 2;
                    TankModel[6].PointX = TankModel[0].PointX + 2;
                    TankModel[6].PointY = TankModel[0].PointY;
                    TankModel[7].PointX = TankModel[0].PointX + 2;
                    TankModel[7].PointY = TankModel[0].PointY - 1;
                    TankModel[8].PointX = TankModel[0].PointX + 2;
                    TankModel[8].PointY = TankModel[0].PointY - 2;
                    break;
                case GameControl.MoveRight:
                    TankModel[0].PointX = InitPointX;
                    TankModel[0].PointY = InitPointY;
                    TankModel[1].PointX = TankModel[0].PointX;
                    TankModel[1].PointY = TankModel[0].PointY + 1;
                    TankModel[2].PointX = TankModel[0].PointX;
                    TankModel[2].PointY = TankModel[0].PointY + 2;
                    TankModel[3].PointX = TankModel[0].PointX - 1;
                    TankModel[3].PointY = TankModel[0].PointY;
                    TankModel[4].PointX = TankModel[0].PointX - 1;
                    TankModel[4].PointY = TankModel[0].PointY + 1;
                    TankModel[5].PointX = TankModel[0].PointX - 1;
                    TankModel[5].PointY = TankModel[0].PointY + 2;
                    TankModel[6].PointX = TankModel[0].PointX - 2;
                    TankModel[6].PointY = TankModel[0].PointY;
                    TankModel[7].PointX = TankModel[0].PointX - 2;
                    TankModel[7].PointY = TankModel[0].PointY + 1;
                    TankModel[8].PointX = TankModel[0].PointX - 2;
                    TankModel[8].PointY = TankModel[0].PointY + 2;
                    break;
            }
        }

        #endregion

        #region Move checking

        protected void NeedTurn()
        {
            if (Direction != LastDirection)
            {
                TankTurn();
                LastDirection = Direction;
            }
        }

        protected bool DirectionBlocked(List<int> borders, Dictionary<Coordinate,MapObject> map)
        {
            if (NearBorder(borders))
            {
                return true;
            }
            if (NearWall(map))
            {
                return true;
            }
            return false;
        }

        protected bool NearWall(Dictionary<Coordinate, MapObject> map)
        {
            bool nearWall = false;
            switch (Direction)
            {
                    
                    case GameControl.MoveUp:
                    if (map.Keys.ToList().Exists(key => (key.PointX == TankModel[0].PointX && key.PointY == TankModel[0].PointY - 1)
                        || (key.PointX == TankModel[1].PointX && key.PointY == TankModel[1].PointY - 1)
                        || (key.PointX == TankModel[2].PointX && key.PointY == TankModel[2].PointY - 1)))
                    {
                        nearWall = true;
                    }
                    break;
                    case GameControl.MoveDown:
                    if (map.Keys.ToList().Exists(key => (key.PointX == TankModel[0].PointX && key.PointY == TankModel[0].PointY + 1)
                        || (key.PointX == TankModel[1].PointX && key.PointY == TankModel[1].PointY + 1)
                        || (key.PointX == TankModel[2].PointX && key.PointY == TankModel[2].PointY + 1)))
                    {
                        nearWall = true;
                    }
                    break;
                    case GameControl.MoveLeft:
                    if (map.Keys.ToList().Exists(key => (key.PointX == TankModel[0].PointX - 1 && key.PointY == TankModel[0].PointY)
                        || (key.PointX == TankModel[1].PointX - 1 && key.PointY == TankModel[1].PointY)
                        || (key.PointX == TankModel[2].PointX - 1 && key.PointY == TankModel[2].PointY)))
                    {
                        nearWall = true;
                    }
                    break;
                    case GameControl.MoveRight:
                    if (map.Keys.ToList().Exists(key => (key.PointX == TankModel[0].PointX + 1 && key.PointY == TankModel[0].PointY)
                        || (key.PointX == TankModel[1].PointX + 1 && key.PointY == TankModel[1].PointY)
                        || (key.PointX == TankModel[2].PointX + 1 && key.PointY == TankModel[2].PointY)))
                    {
                        nearWall = true;
                    }
                    break;
            }
            return nearWall;
        }

        protected bool NearBorder(List<int> borders)
        {
            bool nearBorder = false;
            switch (Direction)
            {
                    case GameControl.MoveUp:
                    if (TankModel[0].PointY + 1 == borders[1] ||
                        TankModel[1].PointY + 1 == borders[1] ||
                        TankModel[2].PointY + 1 == borders[1])
                    {
                        nearBorder = true;
                    }
                    break;
                    case GameControl.MoveDown:
                    if (TankModel[0].PointY - 1 == borders[3] ||
                        TankModel[1].PointY - 1 == borders[3] ||
                        TankModel[2].PointY - 1 == borders[3])
                    {
                        nearBorder = true;
                    }
                    break;
                    case GameControl.MoveLeft:
                    if (TankModel[0].PointX - 1 == borders[0] ||
                        TankModel[1].PointX - 1 == borders[0] ||
                        TankModel[2].PointX - 1 == borders[0])
                    {
                        nearBorder = true;
                    }
                    break;
                    case GameControl.MoveRight:
                    if (TankModel[0].PointX + 1 == borders[2] ||
                        TankModel[1].PointX + 1 == borders[2] ||
                        TankModel[2].PointX + 1 == borders[2])
                    {
                        nearBorder = true;
                    }
                    break;
            }
            return nearBorder;
        }

        #endregion

        #region Tank model move

        protected void TankTurn()
        {
            switch (Direction)
            {
                case GameControl.MoveLeft:
                    switch (LastDirection)
                    {
                        case GameControl.MoveUp:

                            #region turn_up->left

                            TankModel[0].PointX = TankModel[6].PointX;
                            TankModel[0].PointY = TankModel[6].PointY;
                            TankModel[1].PointX = TankModel[0].PointX;
                            TankModel[1].PointY = TankModel[0].PointY - 1;
                            TankModel[2].PointX = TankModel[0].PointX;
                            TankModel[2].PointY = TankModel[0].PointY - 2;
                            TankModel[3].PointX = TankModel[0].PointX + 1;
                            TankModel[3].PointY = TankModel[0].PointY;
                            TankModel[4].PointX = TankModel[0].PointX + 1;
                            TankModel[4].PointY = TankModel[0].PointY - 1;
                            TankModel[5].PointX = TankModel[0].PointX + 1;
                            TankModel[5].PointY = TankModel[0].PointY - 2;
                            TankModel[6].PointX = TankModel[0].PointX + 2;
                            TankModel[6].PointY = TankModel[0].PointY;
                            TankModel[7].PointX = TankModel[0].PointX + 2;
                            TankModel[7].PointY = TankModel[0].PointY - 1;
                            TankModel[8].PointX = TankModel[0].PointX + 2;
                            TankModel[8].PointY = TankModel[0].PointY - 2;

                            #endregion

                            break;
                        case GameControl.MoveRight:

                            #region turn_right->left

                            TankModel[0].PointX = TankModel[8].PointX;
                            TankModel[0].PointY = TankModel[8].PointY;
                            TankModel[1].PointX = TankModel[0].PointX;
                            TankModel[1].PointY = TankModel[0].PointY - 1;
                            TankModel[2].PointX = TankModel[0].PointX;
                            TankModel[2].PointY = TankModel[0].PointY - 2;
                            TankModel[3].PointX = TankModel[0].PointX + 1;
                            TankModel[3].PointY = TankModel[0].PointY;
                            TankModel[4].PointX = TankModel[0].PointX + 1;
                            TankModel[4].PointY = TankModel[0].PointY - 1;
                            TankModel[5].PointX = TankModel[0].PointX + 1;
                            TankModel[5].PointY = TankModel[0].PointY - 2;
                            TankModel[6].PointX = TankModel[0].PointX + 2;
                            TankModel[6].PointY = TankModel[0].PointY;
                            TankModel[7].PointX = TankModel[0].PointX + 2;
                            TankModel[7].PointY = TankModel[0].PointY - 1;
                            TankModel[8].PointX = TankModel[0].PointX + 2;
                            TankModel[8].PointY = TankModel[0].PointY - 2;

                            #endregion

                            break;
                        case GameControl.MoveDown:

                            #region turn_down->left

                            TankModel[0].PointX = TankModel[2].PointX;
                            TankModel[0].PointY = TankModel[2].PointY;
                            TankModel[1].PointX = TankModel[0].PointX;
                            TankModel[1].PointY = TankModel[0].PointY - 1;
                            TankModel[2].PointX = TankModel[0].PointX;
                            TankModel[2].PointY = TankModel[0].PointY - 2;
                            TankModel[3].PointX = TankModel[0].PointX + 1;
                            TankModel[3].PointY = TankModel[0].PointY;
                            TankModel[4].PointX = TankModel[0].PointX + 1;
                            TankModel[4].PointY = TankModel[0].PointY - 1;
                            TankModel[5].PointX = TankModel[0].PointX + 1;
                            TankModel[5].PointY = TankModel[0].PointY - 2;
                            TankModel[6].PointX = TankModel[0].PointX + 2;
                            TankModel[6].PointY = TankModel[0].PointY;
                            TankModel[7].PointX = TankModel[0].PointX + 2;
                            TankModel[7].PointY = TankModel[0].PointY - 1;
                            TankModel[8].PointX = TankModel[0].PointX + 2;
                            TankModel[8].PointY = TankModel[0].PointY - 2;

                            #endregion

                            break;
                    }
                    LastDirection = Direction;
                    break;
                case GameControl.MoveRight:
                    switch (LastDirection)
                    {
                        case GameControl.MoveLeft:

                            #region turn_left->right

                            TankModel[0].PointX = TankModel[8].PointX;
                            TankModel[0].PointY = TankModel[8].PointY;
                            TankModel[1].PointX = TankModel[0].PointX;
                            TankModel[1].PointY = TankModel[0].PointY + 1;
                            TankModel[2].PointX = TankModel[0].PointX;
                            TankModel[2].PointY = TankModel[0].PointY + 2;
                            TankModel[3].PointX = TankModel[0].PointX - 1;
                            TankModel[3].PointY = TankModel[0].PointY;
                            TankModel[4].PointX = TankModel[0].PointX - 1;
                            TankModel[4].PointY = TankModel[0].PointY + 1;
                            TankModel[5].PointX = TankModel[0].PointX - 1;
                            TankModel[5].PointY = TankModel[0].PointY + 2;
                            TankModel[6].PointX = TankModel[0].PointX - 2;
                            TankModel[6].PointY = TankModel[0].PointY;
                            TankModel[7].PointX = TankModel[0].PointX - 2;
                            TankModel[7].PointY = TankModel[0].PointY + 1;
                            TankModel[8].PointX = TankModel[0].PointX - 2;
                            TankModel[8].PointY = TankModel[0].PointY + 2;

                            #endregion

                            break;
                        case GameControl.MoveUp:

                            #region turn_up->right

                            TankModel[0].PointX = TankModel[2].PointX;
                            TankModel[0].PointY = TankModel[2].PointY;
                            TankModel[1].PointX = TankModel[0].PointX;
                            TankModel[1].PointY = TankModel[0].PointY + 1;
                            TankModel[2].PointX = TankModel[0].PointX;
                            TankModel[2].PointY = TankModel[0].PointY + 2;
                            TankModel[3].PointX = TankModel[0].PointX - 1;
                            TankModel[3].PointY = TankModel[0].PointY;
                            TankModel[4].PointX = TankModel[0].PointX - 1;
                            TankModel[4].PointY = TankModel[0].PointY + 1;
                            TankModel[5].PointX = TankModel[0].PointX - 1;
                            TankModel[5].PointY = TankModel[0].PointY + 2;
                            TankModel[6].PointX = TankModel[0].PointX - 2;
                            TankModel[6].PointY = TankModel[0].PointY;
                            TankModel[7].PointX = TankModel[0].PointX - 2;
                            TankModel[7].PointY = TankModel[0].PointY + 1;
                            TankModel[8].PointX = TankModel[0].PointX - 2;
                            TankModel[8].PointY = TankModel[0].PointY + 2;

                            #endregion

                            break;
                        case GameControl.MoveDown:

                            #region turn_down->right

                            TankModel[0].PointX = TankModel[6].PointX;
                            TankModel[0].PointY = TankModel[6].PointY;
                            TankModel[1].PointX = TankModel[0].PointX;
                            TankModel[1].PointY = TankModel[0].PointY + 1;
                            TankModel[2].PointX = TankModel[0].PointX;
                            TankModel[2].PointY = TankModel[0].PointY + 2;
                            TankModel[3].PointX = TankModel[0].PointX - 1;
                            TankModel[3].PointY = TankModel[0].PointY;
                            TankModel[4].PointX = TankModel[0].PointX - 1;
                            TankModel[4].PointY = TankModel[0].PointY + 1;
                            TankModel[5].PointX = TankModel[0].PointX - 1;
                            TankModel[5].PointY = TankModel[0].PointY + 2;
                            TankModel[6].PointX = TankModel[0].PointX - 2;
                            TankModel[6].PointY = TankModel[0].PointY;
                            TankModel[7].PointX = TankModel[0].PointX - 2;
                            TankModel[7].PointY = TankModel[0].PointY + 1;
                            TankModel[8].PointX = TankModel[0].PointX - 2;
                            TankModel[8].PointY = TankModel[0].PointY + 2;

                            #endregion

                            break;
                    }
                    LastDirection = Direction;
                    break;
                case GameControl.MoveUp:
                    switch (LastDirection)
                    {
                        case GameControl.MoveLeft:

                            #region turn_left->up

                            TankModel[0].PointX = TankModel[2].PointX;
                            TankModel[0].PointY = TankModel[2].PointY;
                            TankModel[1].PointX = TankModel[0].PointX + 1;
                            TankModel[1].PointY = TankModel[0].PointY;
                            TankModel[2].PointX = TankModel[0].PointX + 2;
                            TankModel[2].PointY = TankModel[0].PointY;
                            TankModel[3].PointX = TankModel[0].PointX;
                            TankModel[3].PointY = TankModel[0].PointY + 1;
                            TankModel[4].PointX = TankModel[0].PointX + 1;
                            TankModel[4].PointY = TankModel[0].PointY + 1;
                            TankModel[5].PointX = TankModel[0].PointX + 2;
                            TankModel[5].PointY = TankModel[0].PointY + 1;
                            TankModel[6].PointX = TankModel[0].PointX;
                            TankModel[6].PointY = TankModel[0].PointY + 2;
                            TankModel[7].PointX = TankModel[0].PointX + 1;
                            TankModel[7].PointY = TankModel[0].PointY + 2;
                            TankModel[8].PointX = TankModel[0].PointX + 2;
                            TankModel[8].PointY = TankModel[0].PointY + 2;

                            #endregion

                            break;
                        case GameControl.MoveRight:

                            #region turn_right->up

                            TankModel[0].PointX = TankModel[6].PointX;
                            TankModel[0].PointY = TankModel[6].PointY;
                            TankModel[1].PointX = TankModel[0].PointX + 1;
                            TankModel[1].PointY = TankModel[0].PointY;
                            TankModel[2].PointX = TankModel[0].PointX + 2;
                            TankModel[2].PointY = TankModel[0].PointY;
                            TankModel[3].PointX = TankModel[0].PointX;
                            TankModel[3].PointY = TankModel[0].PointY + 1;
                            TankModel[4].PointX = TankModel[0].PointX + 1;
                            TankModel[4].PointY = TankModel[0].PointY + 1;
                            TankModel[5].PointX = TankModel[0].PointX + 2;
                            TankModel[5].PointY = TankModel[0].PointY + 1;
                            TankModel[6].PointX = TankModel[0].PointX;
                            TankModel[6].PointY = TankModel[0].PointY + 2;
                            TankModel[7].PointX = TankModel[0].PointX + 1;
                            TankModel[7].PointY = TankModel[0].PointY + 2;
                            TankModel[8].PointX = TankModel[0].PointX + 2;
                            TankModel[8].PointY = TankModel[0].PointY + 2;

                            #endregion

                            break;
                        case GameControl.MoveDown:

                            #region turn_down->up

                            TankModel[0].PointX = TankModel[8].PointX;
                            TankModel[0].PointY = TankModel[8].PointY;
                            TankModel[1].PointX = TankModel[0].PointX + 1;
                            TankModel[1].PointY = TankModel[0].PointY;
                            TankModel[2].PointX = TankModel[0].PointX + 2;
                            TankModel[2].PointY = TankModel[0].PointY;
                            TankModel[3].PointX = TankModel[0].PointX;
                            TankModel[3].PointY = TankModel[0].PointY + 1;
                            TankModel[4].PointX = TankModel[0].PointX + 1;
                            TankModel[4].PointY = TankModel[0].PointY + 1;
                            TankModel[5].PointX = TankModel[0].PointX + 2;
                            TankModel[5].PointY = TankModel[0].PointY + 1;
                            TankModel[6].PointX = TankModel[0].PointX;
                            TankModel[6].PointY = TankModel[0].PointY + 2;
                            TankModel[7].PointX = TankModel[0].PointX + 1;
                            TankModel[7].PointY = TankModel[0].PointY + 2;
                            TankModel[8].PointX = TankModel[0].PointX + 2;
                            TankModel[8].PointY = TankModel[0].PointY + 2;

                            #endregion

                            break;
                    }
                    LastDirection = Direction;
                    break;
                case GameControl.MoveDown:
                    switch (LastDirection)
                    {
                        case GameControl.MoveLeft:

                            #region turn_left->down

                            TankModel[0].PointX = TankModel[6].PointX;
                            TankModel[0].PointY = TankModel[6].PointY;
                            TankModel[1].PointX = TankModel[0].PointX - 1;
                            TankModel[1].PointY = TankModel[0].PointY;
                            TankModel[2].PointX = TankModel[0].PointX - 2;
                            TankModel[2].PointY = TankModel[0].PointY;
                            TankModel[3].PointX = TankModel[0].PointX;
                            TankModel[3].PointY = TankModel[0].PointY - 1;
                            TankModel[4].PointX = TankModel[0].PointX - 1;
                            TankModel[4].PointY = TankModel[0].PointY - 1;
                            TankModel[5].PointX = TankModel[0].PointX - 2;
                            TankModel[5].PointY = TankModel[0].PointY - 1;
                            TankModel[6].PointX = TankModel[0].PointX;
                            TankModel[6].PointY = TankModel[0].PointY - 2;
                            TankModel[7].PointX = TankModel[0].PointX - 1;
                            TankModel[7].PointY = TankModel[0].PointY - 2;
                            TankModel[8].PointX = TankModel[0].PointX - 2;
                            TankModel[8].PointY = TankModel[0].PointY - 2;

                            #endregion

                            break;
                        case GameControl.MoveRight:

                            #region turn_right->down

                            TankModel[0].PointX = TankModel[2].PointX;
                            TankModel[0].PointY = TankModel[2].PointY;
                            TankModel[1].PointX = TankModel[0].PointX - 1;
                            TankModel[1].PointY = TankModel[0].PointY;
                            TankModel[2].PointX = TankModel[0].PointX - 2;
                            TankModel[2].PointY = TankModel[0].PointY;
                            TankModel[3].PointX = TankModel[0].PointX;
                            TankModel[3].PointY = TankModel[0].PointY - 1;
                            TankModel[4].PointX = TankModel[0].PointX - 1;
                            TankModel[4].PointY = TankModel[0].PointY - 1;
                            TankModel[5].PointX = TankModel[0].PointX - 2;
                            TankModel[5].PointY = TankModel[0].PointY - 1;
                            TankModel[6].PointX = TankModel[0].PointX;
                            TankModel[6].PointY = TankModel[0].PointY - 2;
                            TankModel[7].PointX = TankModel[0].PointX - 1;
                            TankModel[7].PointY = TankModel[0].PointY - 2;
                            TankModel[8].PointX = TankModel[0].PointX - 2;
                            TankModel[8].PointY = TankModel[0].PointY - 2;

                            #endregion

                            break;
                        case GameControl.MoveUp:

                            #region turn_up->down

                            TankModel[0].PointX = TankModel[8].PointX;
                            TankModel[0].PointY = TankModel[8].PointY;
                            TankModel[1].PointX = TankModel[0].PointX - 1;
                            TankModel[1].PointY = TankModel[0].PointY;
                            TankModel[2].PointX = TankModel[0].PointX - 2;
                            TankModel[2].PointY = TankModel[0].PointY;
                            TankModel[3].PointX = TankModel[0].PointX;
                            TankModel[3].PointY = TankModel[0].PointY - 1;
                            TankModel[4].PointX = TankModel[0].PointX - 1;
                            TankModel[4].PointY = TankModel[0].PointY - 1;
                            TankModel[5].PointX = TankModel[0].PointX - 2;
                            TankModel[5].PointY = TankModel[0].PointY - 1;
                            TankModel[6].PointX = TankModel[0].PointX;
                            TankModel[6].PointY = TankModel[0].PointY - 2;
                            TankModel[7].PointX = TankModel[0].PointX - 1;
                            TankModel[7].PointY = TankModel[0].PointY - 2;
                            TankModel[8].PointX = TankModel[0].PointX - 2;
                            TankModel[8].PointY = TankModel[0].PointY - 2;

                            #endregion
                            break;
                    }
                    LastDirection = Direction;
                    break;
            }
        }

        public void TankMove(List<int> borders, Dictionary<Coordinate, MapObject> map)
        {
            if (CanMove)
            {
                OnTankErase();
                NeedTurn();
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
                OnTankDraw();
                CanMove = false;
            }
        }

        protected void TankMoveRight()
        {
            TankModel[0].PointX += 1;
            TankModel[1].PointX += 1;
            TankModel[2].PointX += 1;
            TankModel[3].PointX += 1;
            TankModel[4].PointX += 1;
            TankModel[5].PointX += 1;
            TankModel[6].PointX += 1;
            TankModel[7].PointX += 1;
            TankModel[8].PointX += 1;
        }

        protected void TankMoveLeft()
        {
            TankModel[0].PointX -= 1;
            TankModel[1].PointX -= 1;
            TankModel[2].PointX -= 1;
            TankModel[3].PointX -= 1;
            TankModel[4].PointX -= 1;
            TankModel[5].PointX -= 1;
            TankModel[6].PointX -= 1;
            TankModel[7].PointX -= 1;
            TankModel[8].PointX -= 1;
        }

        protected void TankMoveDown()
        {
            TankModel[0].PointY += 1;
            TankModel[1].PointY += 1;
            TankModel[2].PointY += 1;
            TankModel[3].PointY += 1;
            TankModel[4].PointY += 1;
            TankModel[5].PointY += 1;
            TankModel[6].PointY += 1;
            TankModel[7].PointY += 1;
            TankModel[8].PointY += 1;
        }

        protected void TankMoveUp()
        {
            TankModel[0].PointY -= 1;
            TankModel[1].PointY -= 1;
            TankModel[2].PointY -= 1;
            TankModel[3].PointY -= 1;
            TankModel[4].PointY -= 1;
            TankModel[5].PointY -= 1;
            TankModel[6].PointY -= 1;
            TankModel[7].PointY -= 1;
            TankModel[8].PointY -= 1;
        }

        #endregion

        #region Tank shoot

        public void Shoot()
        {
            if (CanShoot)
            {
                switch (Direction)
                {
                    case GameControl.MoveUp:
                        Bullets.Add(new Bullet(TankModel[1].PointX, TankModel[1].PointY, Direction));
                        break;
                    case GameControl.MoveDown:
                        Bullets.Add(new Bullet(TankModel[1].PointX, TankModel[1].PointY, Direction));
                        break;
                    case GameControl.MoveLeft:
                        Bullets.Add(new Bullet(TankModel[1].PointX, TankModel[1].PointY, Direction));
                        break;
                    case GameControl.MoveRight:
                        Bullets.Add(new Bullet(TankModel[1].PointX, TankModel[1].PointY, Direction));
                        break;
                }
                CanShoot = false;
            }
        }

        #endregion

        #region Handlers

        public void OnTankDraw()
        {
            if (TankDraw != null)
            {
                TankDraw(TankModel);
            }
        }

        public void OnTankErase()
        {
            if (TankErase != null)
            {
                TankErase(TankModel);
            }
        }

        public void OnHealthDraw(int health)
        {
            if (HealthDraw != null)
            {
                HealthDraw(health);
            }
        }

        #endregion
    }
}