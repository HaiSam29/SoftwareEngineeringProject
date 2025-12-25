using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Interfaces.Map
{
    public interface ITileMapRenderer
    {
        void Draw(SpriteBatch spriteBatch, Vector2 offset);
    }
}
