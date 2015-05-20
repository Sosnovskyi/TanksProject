using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TanksGame.TanksEngine;
using TanksGame.DesktopUI.Properties;

namespace TanksGame.DesktopUI
{
    public partial class frmDesktopGame : Form
    {
        #region Properties
        public static Graphics Graphic { get; set; }
        public static GameControl UserAction { get; set; }
        private InitObjects Init { get; set; }
        public static ExitStatus Status { get; set; }
        public static string Result { get; set; }

        #endregion

        public frmDesktopGame()
        {
            InitializeComponent();
            Graphic = pbCanvas.CreateGraphics();
        }

        private void GameLoad(object sender, EventArgs e)
        {
            Paint += Draw;
            Init = new InitObjects();
            Init.Init(lblPlayerHealthValue, lblEnemyHealthValue);
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            DrawMap();
            DrawBorders();
            Init.Player.OnTankDraw();
            Init.Enemy.OnEnemyTankDraw();
        }

        public static void Exit()
        {
            DialogResult result = MessageBox.Show(Resources.frmDesktopGame_Exit_You_Want_To_Restart_Game_, Result, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            Application.Exit();
        }

        #region GameMethods
        public void DrawPlayerHealth(string health)
        {
            lblPlayerHealthValue.Text = health;
        }
        public void DrawEnemyHealth(string health)
        {
            lblEnemyHealthValue.Text = health;
        }

        public void DrawMap()
        {
            var map = GetMap();
            foreach (var m in map)
            {
                Graphic.FillRectangle(Brushes.SlateGray, m.Key.PointX * 10, m.Key.PointY * 10, 5, 5);
            }
        }

        public void DrawBorders()
        {
            for (int i = 1; i < 108; i++)
            {
                Graphic.FillRectangle(Brushes.Aqua, i * 10, 40, 5, 5);
            }
            for (int i = 1; i < 108; i++)
            {
                Graphic.FillRectangle(Brushes.Aqua, i * 10, 490, 5, 5);
            }
            for (int i = 4; i <= 49; i++)
            {
                Graphic.FillRectangle(Brushes.Aqua, 1, i * 10, 5, 5);
            }
            for (int i = 4; i <= 49; i++)
            {
                Graphic.FillRectangle(Brushes.Aqua, 1075, i * 10, 5, 5);
            }
        }
        public static Dictionary<Coordinate, MapObject> GetMap()
        {
            Dictionary<Coordinate, MapObject> map = new Dictionary<Coordinate, MapObject>();
            for (int i = 1; i < 108; i++)
            {
                for (int j = 6; j < 49; j++)
                {
                    if ((j + 1)%10 == 0)
                    {
                        map.Add(new Coordinate(i, j), MapObject.Wall);
                        if (!map.Keys.ToList().Exists(k => k.PointX == i && k.PointY == j + 1))
                        {
                            map.Add(new Coordinate(i, j + 1), MapObject.Wall);
                        }
                        if (!map.Keys.ToList().Exists(k => k.PointX == i && k.PointY == j + 2))
                        {
                            map.Add(new Coordinate(i, j + 2), MapObject.Wall);
                        }
                    }
                    else if ((i + 1)%10 == 0)
                    {
                        map.Add(new Coordinate(i, j), MapObject.Wall);
                        map.Add(new Coordinate(i + 1, j), MapObject.Wall);
                        map.Add(new Coordinate(i + 2, j), MapObject.Wall);
                    }
                }
            }
            return map;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    UserAction = GameControl.MoveUp;
                    break;
                case Keys.Down:
                    UserAction = GameControl.MoveDown;
                    break;
                case Keys.Left:
                    UserAction = GameControl.MoveLeft;
                    break;
                case Keys.Right:
                    UserAction = GameControl.MoveRight;
                    break;
                case Keys.Enter:
                    UserAction = GameControl.StartGame;
                    break;
                case Keys.Escape:
                    UserAction = GameControl.EndGame;
                    break;
                case Keys.Space:
                    UserAction = GameControl.TankShoot;
                    break;
                case Keys.P:
                    UserAction = GameControl.PauseGame;
                    break;
                case Keys.R:
                    UserAction = GameControl.ResumeGame;
                    break;
            }
        }

        #endregion
    }
}
