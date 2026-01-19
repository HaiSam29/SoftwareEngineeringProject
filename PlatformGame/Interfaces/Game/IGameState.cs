using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Game
{
    // Klein contract met Update(GameTime) en Draw(SpriteBatch) voor alle gamestates
    // ISP Heel kleine, taakgerichte interface. States hoeven geen onnodige methods te implementeren
    // DIP Game1 hangt af van de abstractie IGameState, niet van concrete states
    public interface IGameState
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
