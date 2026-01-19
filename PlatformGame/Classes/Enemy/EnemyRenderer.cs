using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Interfaces.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Enemy
{
    // Bevat alle SpriteBatch.Draw(...) logica
    // SRP alleen tekenen
    // OCP later zonder wijzigingen aan Enemy een andere renderer toevoegen 
    public class EnemyRenderer : IEnemyRenderer
    {
        // pakt CurrentSourceRect, berekent scale = Size / frameHeight, bepaalt het middenpunt (centerX/centerY) zodat de enemy “op de grond” lijkt te staan.
        // Telt cameraOffset erbij, en flipt horizontaal bij Direction < 0
        public void Draw(SpriteBatch spriteBatch, IEnemyRenderData data, Vector2 cameraOffset)
        {
            if (data.CurrentTexture == null) return;

            Rectangle sourceRect = data.CurrentSourceRect;
            float scale = (float)data.Size / sourceRect.Height;
            Vector2 origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);

            float centerX = data.Position.X + (data.Size / 2f);
            float centerY = data.Position.Y + data.Size - (sourceRect.Height * scale / 2f);
            Vector2 drawPos = new Vector2(centerX, centerY) + cameraOffset;

            SpriteEffects effect = data.Direction < 0
                ? SpriteEffects.FlipHorizontally
                : SpriteEffects.None;

            spriteBatch.Draw(
                data.CurrentTexture,
                drawPos,
                sourceRect,
                Color.White,
                0f,
                origin,
                scale,
                effect,
                0f
            );
        }
    }
}
