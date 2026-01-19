using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Enemy
{
    // Wat de buitenwereld mag weten over een enemy.
    // ISP Klein en gefocust. Geen onnodige methods
    // EnemyManager praat tegen IEnemy, niet tegen concrete Enemy
    public interface IEnemy
    {
        Rectangle Bounds { get; }
        void Update(GameTime gameTime); 
        void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset);
        void Attack();
    }
}
