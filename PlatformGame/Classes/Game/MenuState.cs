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
        private Texture2D _buttonTexture;

        private Rectangle _startRect;  // Grote start knop
        private Rectangle _lvl1Rect;   // Klein knopje level 1
        private Rectangle _lvl2Rect;   // Klein knopje level 2

        // Kleuren
        private Color _btnColorMain = Color.DarkSlateBlue;
        private Color _btnColorSub = Color.Teal; // Andere kleur voor level knoppen
        private Color _btnColorHover = Color.CornflowerBlue;

        public MenuState(Game1 game)
        {
            _game = game;
            var content = game.Content;

            _font = content.Load<SpriteFont>("GameFont");
            _backgroundTexture = content.Load<Texture2D>("background");

            _buttonTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            _buttonTexture.SetData(new[] { Color.White });

            // POSITIES BEPALEN 
            int screenW = GameConfig.screenWidth;
            int screenH = GameConfig.screenHeight;
            int centerX = screenW / 2;
            int centerY = screenH / 2;

            // 1. De Grote Start Knop
            int mainW = 300;
            int mainH = 60;
            _startRect = new Rectangle(centerX - (mainW / 2), centerY, mainW, mainH);

            // 2. De Kleine Level Knoppen 
            int subSize = 50;
            int spacing = 20; // Ruimte tussen de knopjes
            int startY_Levels = centerY + mainH + 40; // 40px onder de startknop

            // Level 1 knop (links van het midden)
            _lvl1Rect = new Rectangle(centerX - subSize - (spacing / 2), startY_Levels, subSize, subSize);

            // Level 2 knop (rechts van het midden)
            _lvl2Rect = new Rectangle(centerX + (spacing / 2), startY_Levels, subSize, subSize);
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            bool clicked = mouse.LeftButton == ButtonState.Pressed;
            Point mousePos = mouse.Position;

            // Check Hoofdknop (Start Game -> Level 1)
            if (_startRect.Contains(mousePos) && clicked)
            {
                _game.ChangeState(new PlayingState(_game, "Level1"));
            }

            // Check Level 1 selectie
            else if (_lvl1Rect.Contains(mousePos) && clicked)
            {
                _game.ChangeState(new PlayingState(_game, "Level1"));
            }

            // Check Level 2 selectie
            else if (_lvl2Rect.Contains(mousePos) && clicked)
            {
                _game.ChangeState(new PlayingState(_game, "Level2"));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Achtergrond
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, GameConfig.screenWidth, GameConfig.screenHeight), Color.Gray);

            // Titel
            string title = "PLATFORMER GAME";
            Vector2 titleSize = _font.MeasureString(title);
            Vector2 titlePos = new Vector2((GameConfig.screenWidth / 2) - (titleSize.X / 2), 150);
            spriteBatch.DrawString(_font, title, titlePos + new Vector2(4, 4), Color.Black);
            spriteBatch.DrawString(_font, title, titlePos, Color.Gold);

            // 1. Teken Grote Start Knop
            DrawButton(spriteBatch, _startRect, "START GAME", _btnColorMain);

            // Tekstje boven de level knoppen
            string subText = "Or select level:";
            Vector2 subSize = _font.MeasureString(subText);
            spriteBatch.DrawString(_font, subText,
                new Vector2((GameConfig.screenWidth / 2) - (subSize.X / 2), _lvl1Rect.Y - 25),
                Color.White);

            // 2. Teken Level Knoppen
            DrawButton(spriteBatch, _lvl1Rect, "1", _btnColorSub);
            DrawButton(spriteBatch, _lvl2Rect, "2", _btnColorSub);
        }

        // Hulpfunctie met extra parameter voor kleur
        private void DrawButton(SpriteBatch spriteBatch, Rectangle rect, string text, Color baseColor)
        {
            MouseState mouse = Mouse.GetState();
            bool isHovering = rect.Contains(mouse.Position);

            // Hover kleur of de basiskleur die we meegeven
            Color color = isHovering ? _btnColorHover : baseColor;

            spriteBatch.Draw(_buttonTexture, rect, color);

            Vector2 textSize = _font.MeasureString(text);
            Vector2 textPos = new Vector2(
                rect.X + (rect.Width / 2) - (textSize.X / 2),
                rect.Y + (rect.Height / 2) - (textSize.Y / 2)
            );

            spriteBatch.DrawString(_font, text, textPos, Color.White);
        }
    }
}
