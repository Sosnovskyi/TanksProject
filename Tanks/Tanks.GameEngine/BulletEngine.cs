using System.Collections.Generic;

namespace Tanks.GameEngine
{
    public class BulletEngine
    {
        public BulletEngine()
        {
        }

        public BulletEngine(int x, int y, MoveDirection direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; set; }                             // private set
        public int Y { get; set; }                               // private set
        public MoveDirection Direction { get; set; }                 //private set
        public event GameEngineDelegate<BulletEngine> BulletDraw;
        public event GameEngineDelegate<BulletEngine> BulletErase;

        public void OnBulletErase(BulletEngine bullet)
        {
            if (BulletErase != null)
            {
                BulletErase(bullet);
            }
        }

        // Цей метод варто вказати з модифікатором private
        public void OnBulletDraw(BulletEngine bullet)
        {
            if (BulletDraw != null)
            {
                BulletDraw(bullet);
            }
        }

        public void BulletsMove(List<BulletEngine> bullets, MapBase map)
        {
            for (var i = 0; i < bullets.Count; i++)
            {
                if (BulletNearWall(bullets[i], map.GameMap))

                    #region BulletNearWall

                {
                    switch (bullets[i].Direction)
                    {
                        case MoveDirection.Up:
                            map.MapPointDelete(bullets[i].Y - 1, bullets[i].X - 1);
                            map.MapPointDelete(bullets[i].Y - 1, bullets[i].X);
                            map.MapPointDelete(bullets[i].Y - 1, bullets[i].X + 1);
                            break;
                        case MoveDirection.Down:
                            map.MapPointDelete(bullets[i].Y + 1, bullets[i].X - 1);
                            map.MapPointDelete(bullets[i].Y + 1, bullets[i].X);
                            map.MapPointDelete(bullets[i].Y + 1, bullets[i].X + 1);
                            break;
                        case MoveDirection.Left:
                            map.MapPointDelete(bullets[i].Y - 1, bullets[i].X - 1);
                            map.MapPointDelete(bullets[i].Y, bullets[i].X - 1);
                            map.MapPointDelete(bullets[i].Y + 1, bullets[i].X - 1);
                            break;
                        case MoveDirection.Right:
                            map.MapPointDelete(bullets[i].Y - 1, bullets[i].X + 1);
                            map.MapPointDelete(bullets[i].Y, bullets[i].X + 1);
                            map.MapPointDelete(bullets[i].Y + 1, bullets[i].X + 1);
                            break;
                    }
                        OnBulletErase(bullets[i]);
                        BulletDeleteFromMap(bullets[i], map);
                        bullets.Remove(bullets[i]);
                    break;
                }

                #endregion

                if (!BulletNearBorder(bullets[i], map.Borders))

                    #region BulletNearBorder

                {
                        OnBulletErase(bullets[i]);
                        BulletDeleteFromMap(bullets[i], map);
                    switch (bullets[i].Direction)
                    {
                        case MoveDirection.Up:
                            BulletMoveUp(bullets[i]);
                            break;
                        case MoveDirection.Down:
                            BulletMoveDown(bullets[i]);
                            break;
                        case MoveDirection.Left:
                            BulletMoveLeft(bullets[i]);
                            break;
                        case MoveDirection.Right:
                            BulletMoveRight(bullets[i]);
                            break;
                    }
                        OnBulletDraw(bullets[i]);
                        BulletCreateOnMap(bullets[i], map);
                }
                else
                {
                    OnBulletErase(bullets[i]);
                    BulletDeleteFromMap(bullets[i], map);
                    bullets.Remove(bullets[i]);
                }

                #endregion
            }
        }

        // Цей метод варто вказати з модифікатором private
        public void BulletCreateOnMap(BulletEngine bulletEngine, MapBase map)
        {
            map.GameMap[bulletEngine.Y, bulletEngine.X] = 4;
        }

        public void BulletDeleteFromMap(BulletEngine bulletEngine, MapBase map)
        {
            map.GameMap[bulletEngine.Y, bulletEngine.X] = 0;
        }

        // Всі наступні методи варто вказати з модифікатором private
        public bool BulletNearWall(BulletEngine bulletEngine, int[,] mapPoints)
        {
            var bulletNearWall = false;
            switch (bulletEngine.Direction)
            {
                case MoveDirection.Up:
                    if (mapPoints[bulletEngine.Y - 1, bulletEngine.X] == 1)
                    {
                        bulletNearWall = true;
                    }
                    break;
                case MoveDirection.Down:
                    if (mapPoints[bulletEngine.Y + 1, bulletEngine.X] == 1)
                    {
                        bulletNearWall = true;
                    }
                    break;
                case MoveDirection.Left:
                    if (mapPoints[bulletEngine.Y, bulletEngine.X - 1] == 1)
                    {
                        bulletNearWall = true;
                    }
                    break;
                case MoveDirection.Right:
                    if (mapPoints[bulletEngine.Y, bulletEngine.X + 1] == 1)
                    {
                        bulletNearWall = true;
                    }
                    break;
            }
            return bulletNearWall;
        }

        public void BulletMoveRight(BulletEngine bulletEngine)
        {
            bulletEngine.X = bulletEngine.X + 1;
        }

        public void BulletMoveLeft(BulletEngine bulletEngine)
        {
            bulletEngine.X = bulletEngine.X - 1;
        }

        public void BulletMoveDown(BulletEngine bulletEngine)
        {
            bulletEngine.Y = bulletEngine.Y + 1;
        }

        public void BulletMoveUp(BulletEngine bulletEngine)
        {
            bulletEngine.Y = bulletEngine.Y - 1;
        }

        public bool BulletNearBorder(BulletEngine bulletEngine, List<int> borders)
        {
            var bulletNearBorder = false;
            switch (bulletEngine.Direction)
            {
                case MoveDirection.Up:
                    if (bulletEngine.Y - 1 == borders[0] || bulletEngine.Y - 2 == borders[0])
                    {
                        bulletNearBorder = true;
                    }
                    break;
                case MoveDirection.Down:
                    if (bulletEngine.Y + 1 == borders[2] || bulletEngine.Y + 2 == borders[2])
                    {
                        bulletNearBorder = true;
                    }
                    break;
                case MoveDirection.Left:
                    if (bulletEngine.X - 1 == borders[0] || bulletEngine.X - 2 == borders[0])
                    {
                        bulletNearBorder = true;
                    }
                    break;
                case MoveDirection.Right:
                    if (bulletEngine.X + 2 == borders[3] || bulletEngine.X + 3 == borders[3])
                    {
                        bulletNearBorder = true;
                    }
                    break;
            }
            return bulletNearBorder;
        }
    }
}