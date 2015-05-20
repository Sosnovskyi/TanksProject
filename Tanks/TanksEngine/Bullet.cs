using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace TanksGame.TanksEngine
{
    public class Bullet
    {
        #region Public properties
        public int PointX { get; set; }

        public int PointY { get; set; }
        public GameControl Direction { get; set; }
        #endregion

        #region Public events

        public event GameEngineDelegate<Coordinate> BulletDraw;

        public event GameEngineDelegate<Coordinate> BulletErase;

        #endregion

        #region Constructors
        public Bullet()
        {
        }

        public Bullet(int pointX, int pointY, GameControl direction)
        {
            PointX = pointX;
            PointY = pointY;
            Direction = direction;
        }

        #endregion

        #region Move checking
        public bool BulletNearWall(Bullet bullet, Dictionary<Coordinate, MapObject> map)
        {
            var bulletNearWall = false;
            switch (bullet.Direction)
            {
                case GameControl.MoveUp:
                    if (map.Keys.ToList().Exists(key => key.PointX == bullet.PointX && key.PointY == bullet.PointY - 1))
                    {
                        bulletNearWall = true;
                    }
                    break;
                case GameControl.MoveDown:
                    if (map.Keys.ToList().Exists(key => key.PointX == bullet.PointX && key.PointY == bullet.PointY +1))
                    {
                        bulletNearWall = true;
                    }
                    break;
                case GameControl.MoveLeft:
                    if (map.Keys.ToList().Exists(key => key.PointX == bullet.PointX - 1 && key.PointY == bullet.PointY))
                    {
                        bulletNearWall = true;
                    }
                    break;
                case GameControl.MoveRight:
                    if (map.Keys.ToList().Exists(key => key.PointX == bullet.PointX + 1 && key.PointY == bullet.PointY))
                    {
                        bulletNearWall = true;
                    }
                    break;
            }
            return bulletNearWall;
        }

        public bool BulletNearBorder(Bullet bulletEngine, List<int> borders)
        {
            var bulletNearBorder = false;
            switch (bulletEngine.Direction)
            {
                case GameControl.MoveUp:
                    if (bulletEngine.PointY - 1 == borders[1])
                    {
                        bulletNearBorder = true;
                    }
                    break;
                case GameControl.MoveDown:
                    if (bulletEngine.PointY + 1 == borders[3])
                    {
                        bulletNearBorder = true;
                    }
                    break;
                case GameControl.MoveLeft:
                    if (bulletEngine.PointX - 1 == borders[0])
                    {
                        bulletNearBorder = true;
                    }
                    break;
                case GameControl.MoveRight:
                    if (bulletEngine.PointX + 2 == borders[2])
                    {
                        bulletNearBorder = true;
                    }
                    break;
            }
            return bulletNearBorder;
        }
        #endregion

        #region Moving

        public void BulletsMove(List<Bullet> bullets, Map map)
        {
            for (var i = 0; i < bullets.Count; i++)
            {
                if (BulletNearWall(bullets[i], map.GameMap))

                #region BulletNearWall

                {
                    switch (bullets[i].Direction)
                    {
                        case GameControl.MoveUp:
                            map.OnMapPointDelete(bullets[i].PointX - 1, bullets[i].PointY - 1);
                            map.OnMapPointDelete(bullets[i].PointX, bullets[i].PointY - 1);
                            map.OnMapPointDelete(bullets[i].PointX + 1, bullets[i].PointY - 1);
                            break;
                        case GameControl.MoveDown:
                            map.OnMapPointDelete(bullets[i].PointX - 1, bullets[i].PointY + 1);
                            map.OnMapPointDelete(bullets[i].PointX, bullets[i].PointY + 1);
                            map.OnMapPointDelete(bullets[i].PointX + 1, bullets[i].PointY + 1);
                            break;
                        case GameControl.MoveLeft:
                            map.OnMapPointDelete(bullets[i].PointX - 1, bullets[i].PointY - 1);
                            map.OnMapPointDelete(bullets[i].PointX - 1, bullets[i].PointY);
                            map.OnMapPointDelete(bullets[i].PointX - 1, bullets[i].PointY + 1);
                            break;
                        case GameControl.MoveRight:
                            map.OnMapPointDelete(bullets[i].PointX + 1, bullets[i].PointY - 1);
                            map.OnMapPointDelete(bullets[i].PointX + 1, bullets[i].PointY);
                            map.OnMapPointDelete(bullets[i].PointX + 1, bullets[i].PointY + 1);
                            break;
                    }
                    OnBulletErase(new Coordinate(bullets[i].PointX, bullets[i].PointY));
                    bullets.Remove(bullets[i]);
                    break;
                }

                #endregion

                if (bullets.Contains(bullets[i]))
                {
                    if (!BulletNearBorder(bullets[i], map.GameSpace))

                    #region Bullet move
                    {
                        OnBulletErase(new Coordinate(bullets[i].PointX, bullets[i].PointY));
                        switch (bullets[i].Direction)
                        {
                            case GameControl.MoveUp:
                                BulletMoveUp(bullets[i]);
                                break;
                            case GameControl.MoveDown:
                                BulletMoveDown(bullets[i]);
                                break;
                            case GameControl.MoveLeft:
                                BulletMoveLeft(bullets[i]);
                                break;
                            case GameControl.MoveRight:
                                BulletMoveRight(bullets[i]);
                                break;
                        }
                        OnBulletDraw(new Coordinate(bullets[i].PointX, bullets[i].PointY));
                    }
                    else
                    {
                        OnBulletErase(new Coordinate(bullets[i].PointX, bullets[i].PointY));
                        bullets.Remove(bullets[i]);
                    }

                    #endregion
                }
            }
        }

        private void BulletMoveRight(Bullet bulletEngine)
        {
            bulletEngine.PointX = bulletEngine.PointX + 1;
        }

        private void BulletMoveLeft(Bullet bulletEngine)
        {
            bulletEngine.PointX = bulletEngine.PointX - 1;
        }

        private void BulletMoveDown(Bullet bulletEngine)
        {
            bulletEngine.PointY = bulletEngine.PointY + 1;
        }

        private void BulletMoveUp(Bullet bulletEngine)
        {
            bulletEngine.PointY = bulletEngine.PointY - 1;
        }

        #endregion

        #region Handlers
        public void OnBulletErase(Coordinate coordinate)
        {
            if (BulletErase != null)
            {
                BulletErase(coordinate);
            }
        }

        public void OnBulletDraw(Coordinate coordinate)
        {
            if (BulletDraw != null)
            {
                BulletDraw(coordinate);
            }
        }

        #endregion 
    }
}