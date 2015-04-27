using System.Collections.Generic;

namespace Tanks.GameEngine
{
    public class MapBase
    {
        public delegate void Map<T>(T element);

        public delegate void MapInitDelegate(out int[,] map, int[,] array, int sizeX, int sizeY);

        public List<int> Borders = new List<int>();
        public Map<List<int>> DrawBorders;
        public Map<int[,]> DrawMap;
        public int[,] GameMap;
        public MapInitDelegate InitMap;
        public event GameEngineDelegate<int[]> MapPointErase;

        public void OnMapPointErase(int[] point)
        {
            if (MapPointErase != null)
            {
                MapPointErase(point);
            }
        }

        public void OnDrawBorders(List<int> borders)
        {
            if (DrawBorders != null)
            {
                DrawBorders(borders);
            }
        }

        public void OnDrawMap(int[,] map)
        {
            if (DrawMap != null)
            {
                DrawMap(map);
            }
        }

        public bool IsMapPointAlive(int x, int y)
        {
            if (GameMap[x, y] == 1)
            {
                return true;
            }
            return false;
        }

        public void SetBorders(int topleft, int topRight, int bottomLeft, int bottomRight)
        {
            Borders.Add(topleft);
            Borders.Add(topRight);
            Borders.Add(bottomLeft);
            Borders.Add(bottomRight);
        }

        public void MapPointDelete(int x, int y)
        {
            GameMap[x, y] = 0;
            OnMapPointErase(new[] {x, y});
        }
    }
}