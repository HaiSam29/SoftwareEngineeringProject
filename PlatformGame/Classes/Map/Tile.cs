using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Map
{
    public class Tile
    {
        public TileType Type { get; set; }
        public Rectangle SourceRect { get; set; }

        public Tile(TileType type, Rectangle sourceRect)
        {
            Type = type;
            SourceRect = sourceRect;
        }
    }
}
