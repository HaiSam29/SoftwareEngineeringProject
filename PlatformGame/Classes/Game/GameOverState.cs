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
    // Tekent een Game Over‑scherm met overlay
    // SRP 1 taak: de Game Over UI tonen en 1 input‑actie
    // DIP Krijgt IGameConfig en Game1 via DI, gebruikt ChangeState om terug te gaan naar menu
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
            // Berekening van panelRect op basis van _config.ScreenWidth/Height centreert de panel
            Rectangle panelRect = new Rectangle(
                (_config.ScreenWidth / 2) - (panelWidth / 2),
                (_config.ScreenHeight / 2) - (panelHeight / 2),
                panelWidth,
                panelHeight
            );
            // _darkOverlay is 1×1 texture die als semi‑transparant paneel over het scherm wordt geschaald
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
