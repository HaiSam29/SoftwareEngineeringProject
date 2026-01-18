using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Game
{
    public class GameOverState : IGameState
    {
        private Game1 _game;
        private SpriteFont _font;
        private Texture2D _backgroundTexture;
        private Texture2D _darkOverlay;

        public GameOverState(Game1 game)
        {
            _game = game;
            _font = game.Content.Load<SpriteFont>("GameFont");
            _backgroundTexture = game.Content.Load<Texture2D>("background");

            // Maak een zwarte semi-transparante pixel
            _darkOverlay = new Texture2D(_game.GraphicsDevice, 1, 1);
            _darkOverlay.SetData(new[] { Color.Black * 0.6f }); // 60% zichtbaar zwart
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _game.ChangeState(new MenuState(_game));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // 1. Teken achtergrond met een RODE tint 
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, GameConfig.screenWidth, GameConfig.screenHeight), Color.Red);

            // 2. Teken een zwart paneel in het midden 
            int panelWidth = 600;
            int panelHeight = 300;
            Rectangle panelRect = new Rectangle(
                (GameConfig.screenWidth / 2) - (panelWidth / 2),
                (GameConfig.screenHeight / 2) - (panelHeight / 2),
                panelWidth,
                panelHeight
            );
            spriteBatch.Draw(_darkOverlay, panelRect, Color.White);

            // 3. Teken "GAME OVER"
            string text = "GAME OVER";
            Vector2 textSize = _font.MeasureString(text);
            Vector2 center = new Vector2(GameConfig.screenWidth / 2, GameConfig.screenHeight / 2);

            // Schaduw
            spriteBatch.DrawString(_font, text, center - (textSize / 2) + new Vector2(2, 2) - new Vector2(0, 40), Color.Black);
            // Tekst
            spriteBatch.DrawString(_font, text, center - (textSize / 2) - new Vector2(0, 40), Color.White);

            // 4. Teken instructie
            string subText = "Press ENTER to Main Menu";
            Vector2 subSize = _font.MeasureString(subText);

            spriteBatch.DrawString(_font, subText, center - (subSize / 2) + new Vector2(0, 50), Color.Yellow);
        }
    }
}
