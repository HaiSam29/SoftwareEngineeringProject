using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Classes.Character;
using PlatformGame.Classes.Enemy;
using PlatformGame.Classes.Game;
using PlatformGame.Classes.Level;
using PlatformGame.Classes.Map;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using PlatformGame.Interfaces.Game;
using PlatformGame.Interfaces.Ilevel;
using PlatformGame.Interfaces.Map;
using System.Collections.Generic;

namespace PlatformGame
{
    // MonoGame entrypoint
    // Houdt _currentState bij en delegeert Update/Draw naar de actieve state
    // Maakt één GameConfig aan en past resolutie toe
    // Regelt global input
    // SRP 1 verantwoordelijkheid: orkestreren 
    // DIP Game1 kent concrete states, maar gebruikt verder alleen IGameState.
    // OCP Nieuwe states kun je toevoegen zonder Game1 te wijzigen
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // De huidige status van het spel (Menu, Spelen, Game Over)
        private IGameState _currentState;

        // De configuratie Dependency Injection Container
        private IGameConfig _gameConfig;

        // Om te onthouden welke toetsen vorige frame waren ingedrukt voor F11 toggle
        private KeyboardState _previousKeyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Initialiseer de Config 
            _gameConfig = new GameConfig();

            // Pas schermgrootte aan op basis van de config
            _graphics.PreferredBackBufferWidth = _gameConfig.ScreenWidth;
            _graphics.PreferredBackBufferHeight = _gameConfig.ScreenHeight;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // We beginnen in het Menu en geven de Config mee
            _currentState = new MenuState(this, _gameConfig);
        }

        // Deze methode zorgt dat States kunnen wisselen
        public void ChangeState(IGameState newState)
        {
            _currentState = newState;
        }

        protected override void Update(GameTime gameTime)
        {
            // Haal de huidige status van het toetsenbord op
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // ALTIJD AFSLUITEN BIJ ESCAPE (ONGEACHT DE STATUS)
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // F11 FULLSCREEN LOGICA
            if (currentKeyboardState.IsKeyDown(Keys.F11) && !_previousKeyboardState.IsKeyDown(Keys.F11))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            // Update de huidige state (Menu, Playing, etc.)
            _currentState.Update(gameTime);

            // Sla de keyboard state op voor de volgende frame
            _previousKeyboardState = currentKeyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // SamplerState.PointClamp zorgt voor scherpe pixel art
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Delegeer het tekenen naar de huidige state
            _currentState.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
