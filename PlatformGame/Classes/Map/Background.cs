using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Map
{
    // Laadt één texture en berekent een _destinationRect zodat de achtergrond wordt geschaald en gecentreerd op het scherm
    // SRP  Doet alleen background-rendering en schaalberekening
    // OCP/DIP: Wordt alleen via constructor gebruikt, geen afhankelijkheden op andere game‑logica
    public class Background
    {
        private Texture2D _texture;
        private Rectangle _destinationRect;

        public Background(Texture2D texture, int screenWidth, int screenHeight)
        {
            _texture = texture;

            // Bepaal schaalfactor 
            float scaleX = (float)screenWidth / texture.Width;
            float scaleY = (float)screenHeight / texture.Height;
            float scale = MathF.Max(scaleX, scaleY); 

            int width = (int)(texture.Width * scale);
            int height = (int)(texture.Height * scale);

            // Centreer
            int x = (screenWidth - width) / 2;
            int y = (screenHeight - height) / 2;

            _destinationRect = new Rectangle(x, y, width, height);
        }

        //  Tekent de background
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _destinationRect, Color.White);
        }
    }
}
