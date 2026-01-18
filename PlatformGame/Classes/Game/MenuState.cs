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
    public class MenuState : IGameState
    {
        private Game1 _game;
        private SpriteFont _font;
        private Texture2D _backgroundTexture;
        private Texture2D _buttonTexture; // De 1x1 pixel
        private Rectangle _startButtonRect;

        // Kleuren instellen
        private Color _buttonColorNormal = Color.DarkSlateBlue;
        private Color _buttonColorHover = Color.CornflowerBlue;
        private bool _isHovering = false;

        public MenuState(Game1 game)
        {
            _game = game;
            var content = game.Content;

            _font = content.Load<SpriteFont>("GameFont");
            _backgroundTexture = content.Load<Texture2D>("background");

            // rechthoeken tekenen
            _buttonTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            _buttonTexture.SetData(new[] { Color.White });

            // Knop positie 
            int btnWidth = 300;
            int btnHeight = 60;
            _startButtonRect = new Rectangle(
                (GameConfig.screenWidth / 2) - (btnWidth / 2),
                (GameConfig.screenHeight / 2) + 50, // Iets onder het midden
                btnWidth,
                btnHeight
            );
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            // 1. EERST checken waar de muis is
            _isHovering = _startButtonRect.Contains(mouse.Position);

            // 2. DAN pas checken of er geklikt wordt
            if (_isHovering && mouse.LeftButton == ButtonState.Pressed)
            {
                // Start expliciet Level 1
                _game.ChangeState(new PlayingState(_game, "Level1"));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // 1. Teken de achtergrond 
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, GameConfig.screenWidth, GameConfig.screenHeight), Color.Gray);

            // 2. Teken de Titel met Schaduw
            string title = "PLATFORMER GAME";
            Vector2 titleSize = _font.MeasureString(title);
            Vector2 titlePos = new Vector2((GameConfig.screenWidth / 2) - (titleSize.X / 2), 200);

            // Schaduw 
            spriteBatch.DrawString(_font, title, titlePos + new Vector2(4, 4), Color.Black);
            // Echte tekst 
            spriteBatch.DrawString(_font, title, titlePos, Color.Gold);


            // 3. Teken de Knop 
            // Als we hoveren gebruiken we de lichte kleur, anders de donkere
            Color currentColor = _isHovering ? _buttonColorHover : _buttonColorNormal;

            spriteBatch.Draw(_buttonTexture, _startButtonRect, currentColor);

            // 4. Teken Tekst op de knop
            string btnText = "START GAME";
            Vector2 btnTextSize = _font.MeasureString(btnText);
            Vector2 btnTextPos = new Vector2(
                _startButtonRect.X + (_startButtonRect.Width / 2) - (btnTextSize.X / 2),
                _startButtonRect.Y + (_startButtonRect.Height / 2) - (btnTextSize.Y / 2)
            );

            spriteBatch.DrawString(_font, btnText, btnTextPos, Color.White);
        }
    }
}
