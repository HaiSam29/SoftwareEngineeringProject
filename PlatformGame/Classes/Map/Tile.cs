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
        public int Type { get; set; }
        public Rectangle SourceRect { get; set; }
        public bool IsCollidable { get; set; }

        public Tile(int type, Rectangle sourceRect, bool isCollidable)
        {
            Type = type;
            SourceRect = sourceRect;
            IsCollidable = isCollidable;
        }
    }
}
