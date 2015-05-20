using System.Collections.Generic;
using System.Linq;

namespace TanksGame.TanksEngine
{
    public class Map
    {
         #region Public events

        public delegate void MapDelegate();
        public event MapDelegate DrawBorders;

        public event GameEngineDelegate<Coordinate> MapPointDelete;

        public event MapDelegate DrawMap;

        #endregion

        #region Public properties

        public Dictionary<Coordinate, MapObject> GameMap { get; set; }

        public List<int> GameSpace { get; set; }


        #endregion

        #region Constructor

        public Map(Dictionary<Coordinate, MapObject> gameMap, List<int> gameSpace)
        {
            GameMap = gameMap;
            GameSpace = gameSpace;
        }

        #endregion

        #region Handlers

        public void OnMapPointDelete(int pointX, int pointY)
        {
            var keyToRemove = GameMap.Keys.FirstOrDefault(key => key.PointX == pointX && key.PointY == pointY);
            if (keyToRemove != null)
            {
                GameMap.Remove(keyToRemove);
                if (MapPointDelete != null)
                {
                    MapPointDelete(new Coordinate(pointX, pointY));
                }
            }
        }

        public void OnDrawBorders()
        {
            if (DrawBorders != null)
            {
                DrawBorders();
            }
        }

        public void OnDrawMap()
        {
            if (DrawMap != null)
            {
                DrawMap();
            }
        }

        #endregion 
    }
}