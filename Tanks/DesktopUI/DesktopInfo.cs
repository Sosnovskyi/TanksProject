using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using TanksGame.TanksEngine;

namespace TanksGame.DesktopUI
{
    public class DesktopInfo
    {
        #region Constructor
        public DesktopInfo()
        {
            MapPointSize = Int32.Parse(ConfigurationManager.AppSettings["MapPointSize"]);
            ObjectsPointSize = Int32.Parse(ConfigurationManager.AppSettings["ObjectsPointSize"]);
            CoordinateMultiplier = Int32.Parse(ConfigurationManager.AppSettings["CoordinateMultiplier"]);
            TankInitPointX = Int32.Parse(ConfigurationManager.AppSettings["TankInitPointX"]);
            TankInitPointY = Int32.Parse(ConfigurationManager.AppSettings["TankInitPointY"]);
            EnemyInitPointX = Int32.Parse(ConfigurationManager.AppSettings["EnemyInitPointX"]);
            EnemyInitPointY = Int32.Parse(ConfigurationManager.AppSettings["EnemyInitPointY"]);
            BorderXMin = Int32.Parse(ConfigurationManager.AppSettings["BorderXMin"]);
            BorderYMin = Int32.Parse(ConfigurationManager.AppSettings["BorderYMin"]);
            BorderXMax = Int32.Parse(ConfigurationManager.AppSettings["BorderXMax"]);
            BorderYMax = Int32.Parse(ConfigurationManager.AppSettings["BorderYMax"]);
            PlayerHealth = Int32.Parse(ConfigurationManager.AppSettings["PlayerHealth"]);
            EnemyHealth = Int32.Parse(ConfigurationManager.AppSettings["EnemyHealth"]);
        }

        #endregion

        #region Properties
        public int EnemyHealth { get; set; }

        public int BorderYMax { get; set; }

        public int BorderXMax { get; set; }

        public int BorderYMin { get; set; }

        public int BorderXMin { get; set; }

        public int EnemyInitPointY { get; set; }

        public int EnemyInitPointX { get; set; }

        public int TankInitPointY { get; set; }

        public int TankInitPointX { get; set; }

        public int CoordinateMultiplier { get; set; }

        public int ObjectsPointSize { get; set; }

        public int MapPointSize { get; set; }
        public int PlayerHealth { get; set; }

        #endregion

        #region Tank Methods
        public void OnTankErase(List<TankFragment> tank)
        {
            foreach (var t in tank)
            {
                frmDesktopGame.Graphic.FillRectangle(Brushes.Black, t.PointX * CoordinateMultiplier, t.PointY * CoordinateMultiplier,
                    ObjectsPointSize, ObjectsPointSize);
            }
        }

        public void OnTankDraw(List<TankFragment> tank)
        {
            frmDesktopGame.Graphic.FillRectangle(Brushes.GreenYellow, tank[1].PointX * CoordinateMultiplier,
                tank[1].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.GreenYellow, tank[3].PointX * CoordinateMultiplier,
                tank[3].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.GreenYellow, tank[4].PointX * CoordinateMultiplier,
                tank[4].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.GreenYellow, tank[5].PointX * CoordinateMultiplier,
                tank[5].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.GreenYellow, tank[6].PointX * CoordinateMultiplier,
                tank[6].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.GreenYellow, tank[7].PointX * CoordinateMultiplier,
                tank[7].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.GreenYellow, tank[8].PointX * CoordinateMultiplier,
                tank[ObjectsPointSize].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
        }


        #endregion 

        #region Enemy tank methods
        public void OnEnemyTankErase(List<TankFragment> tank)
        {
            foreach (var t in tank)
            {
                frmDesktopGame.Graphic.FillRectangle(Brushes.Black, t.PointX * CoordinateMultiplier, t.PointY * CoordinateMultiplier,
                    ObjectsPointSize, ObjectsPointSize);
            }
        }

        public void OnEnemyTankDraw(List<TankFragment> tank)
        {
            frmDesktopGame.Graphic.FillRectangle(Brushes.MediumBlue, tank[1].PointX * CoordinateMultiplier,
                tank[1].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.MediumBlue, tank[3].PointX * CoordinateMultiplier,
                tank[3].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.MediumBlue, tank[4].PointX * CoordinateMultiplier,
                tank[4].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.MediumBlue, tank[5].PointX * CoordinateMultiplier,
                tank[5].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.MediumBlue, tank[6].PointX * CoordinateMultiplier,
                tank[6].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.MediumBlue, tank[7].PointX * CoordinateMultiplier,
                tank[7].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
            frmDesktopGame.Graphic.FillRectangle(Brushes.MediumBlue, tank[8].PointX * CoordinateMultiplier,
                tank[ObjectsPointSize].PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
        }

        #endregion

        #region Control methods
        public GameControl OnGetAction()
        {
            var action = frmDesktopGame.UserAction;
            frmDesktopGame.UserAction = GameControl.DafaultAction;
            return action;
        }

        #endregion

        #region Map methods

        public List<int> SetGameSpaceBorders()
        {
            var borsers = new List<int>
            {
                BorderXMin,
                BorderYMin,
                BorderXMax,
                BorderYMax
            };
            return borsers;
        }

        public void OnMapPointDelete(Coordinate point)
        {
            frmDesktopGame.Graphic.FillRectangle(Brushes.Black, point.PointX * CoordinateMultiplier,
                point.PointY * CoordinateMultiplier, MapPointSize, MapPointSize);
        }

        #endregion

        #region Bullet methods
        public void OnBulletDraw(Coordinate point)
        {
            frmDesktopGame.Graphic.FillEllipse(Brushes.Crimson, point.PointX * CoordinateMultiplier,
                point.PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
        }

        public void OnBulletErase(Coordinate point)
        {
            frmDesktopGame.Graphic.FillEllipse(Brushes.Black, point.PointX * CoordinateMultiplier,
                point.PointY * CoordinateMultiplier, ObjectsPointSize, ObjectsPointSize);
        }

        #endregion 
    }
}