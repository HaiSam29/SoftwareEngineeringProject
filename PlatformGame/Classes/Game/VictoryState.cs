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
    public class VictoryState : IGameState
    {
        private Game1 _game;
        private SpriteFont _font;
        private Texture2D _backgroundTexture;
        private Texture2D _pixelTexture; // Voor de panelen

        public VictoryState(Game1 game)
        {
            _game = game;
            _font = game.Content.Load<SpriteFont>("GameFont");
            _backgroundTexture = game.Content.Load<Texture2D>("background");

            // Maak de 'magische pixel' aan voor gekleurde vlakken
            _pixelTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            _pixelTexture.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            // Terug naar menu met Enter
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _game.ChangeState(new MenuState(_game));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int screenW = GameConfig.screenWidth;
            int screenH = GameConfig.screenHeight;
            Vector2 center = new Vector2(screenW / 2, screenH / 2);

            // 1. ACHTERGROND (Getint met 'SeaGreen' voor een succes-gevoel)
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, screenW, screenH), Color.SeaGreen);

            // 2. HET PANEEL
            int panelW = 600;
            int panelH = 300;
            Rectangle panelRect = new Rectangle(
                (int)center.X - panelW / 2,
                (int)center.Y - panelH / 2,
                panelW,
                panelH
            );

            // 2a. Gouden Rand (Iets groter dan het paneel tekenen)
            int border = 4;
            Rectangle borderRect = new Rectangle(
                panelRect.X - border,
                panelRect.Y - border,
                panelRect.Width + (border * 2),
                panelRect.Height + (border * 2)
            );
            spriteBatch.Draw(_pixelTexture, borderRect, Color.Gold);

            // 2b. Het Zwarte Vlak (Semi-transparant)
            spriteBatch.Draw(_pixelTexture, panelRect, Color.Black * 0.8f);

            // 3. TEKST: "VICTORY!"
            string title = "VICTORY!";
            Vector2 titleSize = _font.MeasureString(title);
            Vector2 titlePos = center - (titleSize / 2) - new Vector2(0, 60);

            // Schaduw
            spriteBatch.DrawString(_font, title, titlePos + new Vector2(3, 3), Color.Black);
            // Tekst (Goud)
            spriteBatch.DrawString(_font, title, titlePos, Color.Gold);

            // 4. SUBTEKST: "All enemies defeated!"
            string sub = "All levels completed!";
            Vector2 subSize = _font.MeasureString(sub);
            spriteBatch.DrawString(_font, sub, center - (subSize / 2) + new Vector2(0, 10), Color.White);

            // 5. INSTRUCTIE: "Press Enter"
            string prompt = "Press ENTER for Menu";
            Vector2 promptSize = _font.MeasureString(prompt);
            spriteBatch.DrawString(_font, prompt, center - (promptSize / 2) + new Vector2(0, 80), Color.LightGreen);
        }
    }
    
}
