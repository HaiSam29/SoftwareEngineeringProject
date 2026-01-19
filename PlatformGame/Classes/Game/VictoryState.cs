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
    // Tekent Victory‑scherm en stuurt bij Enter terug naar MenuState
    // SRP Alleen Victory UI + input
    // DIP Zelfde patroon als GameOverState: Game1 + IGameConfig via constructor
    public class VictoryState : IGameState
    {
        private Game1 _game;
        private IGameConfig _config; // Config toevoegen
        private SpriteFont _font;
        private Texture2D _backgroundTexture;
        private Texture2D _pixelTexture;

        // Voeg IGameConfig config toe aan de constructor
        public VictoryState(Game1 game, IGameConfig config)
        {
            _game = game;
            _config = config; // Opslaan

            _font = game.Content.Load<SpriteFont>("GameFont");
            _backgroundTexture = game.Content.Load<Texture2D>("background");

            _pixelTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            _pixelTexture.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // Geef config ook weer door aan MenuState
                _game.ChangeState(new MenuState(_game, _config));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Gebruik _config 
            int screenW = _config.ScreenWidth;
            int screenH = _config.ScreenHeight;
            Vector2 center = new Vector2(screenW / 2, screenH / 2);

            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, screenW, screenH), Color.SeaGreen);

            int panelW = 600;
            int panelH = 300;
            Rectangle panelRect = new Rectangle(
                (int)center.X - panelW / 2,
                (int)center.Y - panelH / 2,
                panelW,
                panelH
            );

            int border = 4;
            Rectangle borderRect = new Rectangle(
                panelRect.X - border,
                panelRect.Y - border,
                panelRect.Width + (border * 2),
                panelRect.Height + (border * 2)
            );
            // _pixelTexture als 1×1 texture om borderRect en panelRect te tekenen
            spriteBatch.Draw(_pixelTexture, borderRect, Color.Gold);
            spriteBatch.Draw(_pixelTexture, panelRect, Color.Black * 0.8f);

            string title = "VICTORY!";
            Vector2 titleSize = _font.MeasureString(title);
            Vector2 titlePos = center - (titleSize / 2) - new Vector2(0, 60);

            spriteBatch.DrawString(_font, title, titlePos + new Vector2(3, 3), Color.Black);
            spriteBatch.DrawString(_font, title, titlePos, Color.Gold);

            string sub = "All levels completed!";
            Vector2 subSize = _font.MeasureString(sub);
            spriteBatch.DrawString(_font, sub, center - (subSize / 2) + new Vector2(0, 10), Color.White);

            string prompt = "Press ENTER for Menu";
            Vector2 promptSize = _font.MeasureString(prompt);
            spriteBatch.DrawString(_font, prompt, center - (promptSize / 2) + new Vector2(0, 80), Color.LightGreen);
        }
    }
    
}
