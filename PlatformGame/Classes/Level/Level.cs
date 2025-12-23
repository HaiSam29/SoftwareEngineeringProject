using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Level
{
    public class Level
    {
        public string Name { get; }
        public TileType[,] MapData { get; }
        public bool[,] Collision { get; }
        public int Width => MapData.GetLength(1);
        public int Height => MapData.GetLength(0);

        public Level(string name, TileType[,] mapData, bool[,] collision)
        {
            Name = name;
            MapData = mapData;
            Collision = collision;

            // Validation
            if (mapData.GetLength(0) != collision.GetLength(0) ||
                mapData.GetLength(1) != collision.GetLength(1))
            {
                throw new System.ArgumentException("MapData en Collision moeten dezelfde afmetingen hebben");
            }
        }
    }
}
