using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Classes.Character;
using PlatformGame.Classes.Game;
using PlatformGame.Classes.Level;
using PlatformGame.Classes.Map;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using PlatformGame.Interfaces.Ilevel;
using PlatformGame.Interfaces.Map;
using System.Collections.Generic;

namespace PlatformGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ICharacter _character;
        private ISprite _sprite;
        private Texture2D _tileset;
        private ITileMapRenderer _tileMapRenderer;
        private ITileCollisionProvider _tileCollisionProvider;
        private ILevelLoader _levelLoader;
        private Level _currentLevel;
        private Background _background;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = GameConfig.screenWidth;
            _graphics.PreferredBackBufferHeight = GameConfig.screenHeight;
            _graphics.ApplyChanges();

            _levelLoader = new HardcodedLevelLoader();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load background
            var backgroundTexture = Content.Load<Texture2D>("background");
            _background = new Background(
                backgroundTexture,
                GameConfig.screenWidth,
                GameConfig.screenHeight
            );

            // Load character textures
            var idleTexture = Content.Load<Texture2D>("idle");
            var runningTexture = Content.Load<Texture2D>("Running");
            var jumpingTexture = Content.Load<Texture2D>("Jumping");
            var landingTexture = Content.Load<Texture2D>("Landing");
            var attackingTexture = Content.Load<Texture2D>("Attacking");

            // Create collision system
            var collision = new CollisionSystem();

            // Create movement strategies
            var strategies = new List<IMovementStrategy>
    {
        new GroundedMovementStrategy(),
        new JumpStrategy(GameConfig.jumpForce)
    };

            // Calculate scaled character dimensions
            int scaledCharacterSize = (int)(GameConfig.characterFrameSize * GameConfig.characterScale); // 48 * 3 = 144

            // Calculate character start position
            int startTileX = 2;      // Kolom 2
            int groundTileY = 11;    // Rij 11 (GrassBlock)
            int tileSize = 80;

            Vector2 startPosition = new Vector2(
                startTileX * tileSize,                          // X: 2 * 80 = 160 pixels
                (groundTileY * tileSize) - scaledCharacterSize  // Y: (11 * 80) - 144 = 736 pixels
            );

            // Create character - HITBOX is 144x144 (scaled)
            _character = new Character(
                startPosition,
                new PhysicsComponent(GameConfig.gravity),
                new KeyboardInputHandler(),
                collision,
                strategies,
                scaledCharacterSize,  // frameWidth = 144 (dit is je HITBOX)
                scaledCharacterSize,  // frameHeight = 144 (dit is je HITBOX)
                GameConfig.characterMoveSpeed
            );

            // Register sprite animations - SPRITE is 48x48 (unscaled)
            _sprite = new Sprite(GameConfig.characterFrameSize, GameConfig.characterFrameSize);
            _sprite.RegisterAnimation(CharacterState.Idle, idleTexture, 4, 0.2f);
            _sprite.RegisterAnimation(CharacterState.Running, runningTexture, 6, 0.12f);
            _sprite.RegisterAnimation(CharacterState.Jumping, jumpingTexture, 1, 0.1f, 64);
            _sprite.RegisterAnimation(CharacterState.Landing, landingTexture, 1, 0.1f, 64);
            _sprite.RegisterAnimation(CharacterState.Attacking, attackingTexture, 8, 0.5f, null, 80);

            // Load tilemap
            _tileset = Content.Load<Texture2D>("tilemap");
            ITileFactory factory = new TileFactory(18, 1);
            _currentLevel = _levelLoader.LoadLevel("Level1");

            // Create tilemap
            var tileMap = new TileMap(
                _currentLevel.MapData,
                _tileset,
                tileSize,  // 80 pixels per tile
                factory
            );

            _tileMapRenderer = tileMap;
            _tileCollisionProvider = tileMap;

            // Link tile collision to character collision system
            collision.SetTileCollisionProvider(_tileCollisionProvider, tileSize);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F11))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _character.Update(deltaTime);
            _sprite.Update(_character.CurrentState, deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _background.Draw(_spriteBatch);

            _tileMapRenderer.Draw(_spriteBatch, Vector2.Zero);

            Vector2 drawPosition = _character.Position + _sprite.CalculateDrawOffset(
                GameConfig.characterFrameSize,
                GameConfig.characterScale
            );

            _spriteBatch.Draw(
                _sprite.CurrentTexture,
                drawPosition,
                _sprite.CurrentFrame,
                Color.White,
                0f,
                Vector2.Zero,
                GameConfig.characterScale,
                _character.FacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f
            );

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
