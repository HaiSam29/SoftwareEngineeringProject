using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Map
{
    // Const ints voor tiles 
    // SRP 1 taak: symbolische namen voor tile-ID’s
    // OCP Breidt je uit door nieuwe consts toe te voegen
    public static class TileType
    {
        public const int Empty = 0;
        public const int GrassLeft = 1;
        public const int GrassMiddel = 2;
        public const int GrassRight = 3;
        public const int Dirt = 4;
        public const int Stone = 5;
        public const int Plant = 6;
        public const int PlantHigh = 7;
        public const int MushRoom = 8;
        public const int ArrowLeft = 9;
        public const int ArrowRight = 10;
        public const int StoneLeft = 11;
        public const int StoneMiddel = 12;
        public const int StoneRight = 13;
        public const int DirtSnowMiddel = 14;
        public const int SnowPlant = 15;
        public const int SnowMan = 16;
        public const int SnowBmiddel = 17;
        public const int SnowBLeft = 18;
        public const int SnowBRight = 19;
        public const int SnowSingle = 20;

    }
}
