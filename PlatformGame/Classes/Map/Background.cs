using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Map
{
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _destinationRect, Color.White);
        }
    }
}
