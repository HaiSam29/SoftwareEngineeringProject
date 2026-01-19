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
        private IGameConfig _config; // Config veld
        private SpriteFont _font;
        private Texture2D _backgroundTexture;
        private Texture2D _darkOverlay;

        // Constructor update
        public GameOverState(Game1 game, IGameConfig config)
        {
            _game = game;
            _config = config; // Opslaan
            _font = game.Content.Load<SpriteFont>("GameFont");
            _backgroundTexture = game.Content.Load<Texture2D>("background");

            _darkOverlay = new Texture2D(_game.GraphicsDevice, 1, 1);
            _darkOverlay.SetData(new[] { Color.Black * 0.6f });
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // Geef config door aan Menu
                _game.ChangeState(new MenuState(_game, _config));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Gebruik _config.ScreenWidth/Height
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _config.ScreenWidth, _config.ScreenHeight), Color.Red);

            int panelWidth = 600;
            int panelHeight = 300;
            Rectangle panelRect = new Rectangle(
                (_config.ScreenWidth / 2) - (panelWidth / 2),
                (_config.ScreenHeight / 2) - (panelHeight / 2),
                panelWidth,
                panelHeight
            );
            spriteBatch.Draw(_darkOverlay, panelRect, Color.White);

            string text = "GAME OVER";
            Vector2 textSize = _font.MeasureString(text);
            Vector2 center = new Vector2(_config.ScreenWidth / 2, _config.ScreenHeight / 2);

            spriteBatch.DrawString(_font, text, center - (textSize / 2) + new Vector2(2, 2) - new Vector2(0, 40), Color.Black);
            spriteBatch.DrawString(_font, text, center - (textSize / 2) - new Vector2(0, 40), Color.White);

            string subText = "Press ENTER to Main Menu";
            Vector2 subSize = _font.MeasureString(subText);

            spriteBatch.DrawString(_font, subText, center - (subSize / 2) + new Vector2(0, 50), Color.Yellow);
        }
    }
}
