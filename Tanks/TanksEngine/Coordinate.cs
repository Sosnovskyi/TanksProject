namespace TanksGame.TanksEngine
{
    public class Coordinate
    {
        public int PointX { get; set; }
        public int PointY { get; set; }

        public Coordinate(int pointx, int pointY)
        {
            PointX = pointx;
            PointY = pointY;
        }
    }
}