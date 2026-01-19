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
        private IGameConfig _config; // Config opslaan
        private SpriteFont _font;
        private Texture2D _backgroundTexture;
        private Texture2D _buttonTexture;

        private Rectangle _startRect;
        private Rectangle _lvl1Rect;
        private Rectangle _lvl2Rect;

        private Color _btnColorMain = Color.DarkSlateBlue;
        private Color _btnColorSub = Color.Teal;
        private Color _btnColorHover = Color.CornflowerBlue;

        // Constructor update: voeg config toe
        public MenuState(Game1 game, IGameConfig config)
        {
            _game = game;
            _config = config; // Opslaan voor later gebruik
            var content = game.Content;

            _font = content.Load<SpriteFont>("GameFont");
            _backgroundTexture = content.Load<Texture2D>("background");

            _buttonTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            _buttonTexture.SetData(new[] { Color.White });

            // GEBRUIK _config.ScreenWidth IPV GameConfig.screenWidth
            int screenW = _config.ScreenWidth;
            int screenH = _config.ScreenHeight;
            int centerX = screenW / 2;
            int centerY = screenH / 2;

            int mainW = 300;
            int mainH = 60;
            _startRect = new Rectangle(centerX - (mainW / 2), centerY, mainW, mainH);

            int subSize = 50;
            int spacing = 20;
            int startY_Levels = centerY + mainH + 40;

            _lvl1Rect = new Rectangle(centerX - subSize - (spacing / 2), startY_Levels, subSize, subSize);
            _lvl2Rect = new Rectangle(centerX + (spacing / 2), startY_Levels, subSize, subSize);
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            bool clicked = mouse.LeftButton == ButtonState.Pressed;
            Point mousePos = mouse.Position;

            // FIX: Geef de _config mee aan PlayingState!
            if (_startRect.Contains(mousePos) && clicked)
            {
                _game.ChangeState(new PlayingState(_game, _config, "Level1"));
            }
            else if (_lvl1Rect.Contains(mousePos) && clicked)
            {
                _game.ChangeState(new PlayingState(_game, _config, "Level1"));
            }
            else if (_lvl2Rect.Contains(mousePos) && clicked)
            {
                _game.ChangeState(new PlayingState(_game, _config, "Level2"));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Gebruik _config
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _config.ScreenWidth, _config.ScreenHeight), Color.Gray);

            string title = "PLATFORMER GAME";
            Vector2 titleSize = _font.MeasureString(title);
            // Gebruik _config
            Vector2 titlePos = new Vector2((_config.ScreenWidth / 2) - (titleSize.X / 2), 150);

            spriteBatch.DrawString(_font, title, titlePos + new Vector2(4, 4), Color.Black);
            spriteBatch.DrawString(_font, title, titlePos, Color.Gold);

            DrawButton(spriteBatch, _startRect, "START GAME", _btnColorMain);

            string subText = "Or select level:";
            Vector2 subSize = _font.MeasureString(subText);
            // Gebruik _config
            spriteBatch.DrawString(_font, subText,
                new Vector2((_config.ScreenWidth / 2) - (subSize.X / 2), _lvl1Rect.Y - 25),
                Color.White);

            DrawButton(spriteBatch, _lvl1Rect, "1", _btnColorSub);
            DrawButton(spriteBatch, _lvl2Rect, "2", _btnColorSub);
        }

        private void DrawButton(SpriteBatch spriteBatch, Rectangle rect, string text, Color baseColor)
        {
            MouseState mouse = Mouse.GetState();
            bool isHovering = rect.Contains(mouse.Position);
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
