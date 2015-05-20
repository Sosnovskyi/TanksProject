namespace TanksGame.TanksEngine
{
    public class TankFragment
    {
        #region Public properties

        public int PointX { get; set; }
        public int PointY { get; set; }

        #endregion

        #region Constructor

        public TankFragment(int pointX, int pointY)
        {
            PointX = pointX;
            PointY = pointY;
        }

        #endregion 
    }
}