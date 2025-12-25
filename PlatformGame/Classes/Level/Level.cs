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
        public int[,] MapData { get; }
        public int Width => MapData.GetLength(1);
        public int Height => MapData.GetLength(0);

        public Level(string name, int[,] mapData)
        {
            Name = name;
            MapData = mapData;
        }
    }
}
