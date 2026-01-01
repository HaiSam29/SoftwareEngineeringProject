using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Enemy
{
    public interface IEnemy
    {
        Rectangle Bounds { get; }
        void Update(GameTime gameTime); 
        void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset);
    }
}
