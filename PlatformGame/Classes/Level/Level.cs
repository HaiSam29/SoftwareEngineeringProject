using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Level
{
    // Eenvoudige data‑klasse: naam, int[,] MapData, en Width/Height properties
    // Geen OCP/DIP-issues; wordt alleen gebruikt als leveldata container
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
