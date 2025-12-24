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
using System.Collections.Generic;

namespace PlatformGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ICharacter _character;
        private ISprite _sprite;
        private const int characterFrameSize = 48;
        private Texture2D _tileset;
        private TileMap _tileMap;
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
            // TODO: Add your initialization logic here

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

            var idleTexture = Content.Load<Texture2D>("idle");
            var runningTexture = Content.Load<Texture2D>("Running");
            var jumpingTexture = Content.Load<Texture2D>("Jumping");
            var landingTexture = Content.Load<Texture2D>("Landing");
            var attackingTexture = Content.Load<Texture2D>("Attacking");

            var collision = new CollisionSystem();
            collision.AddCollider(new Rectangle(0, GameConfig.groundY, GameConfig.screenWidth, GameConfig.screenHeight));

            var strategies = new List<IMovementStrategy> { new GroundedMovementStrategy(), new JumpStrategy(GameConfig.jumpForce) };

            _character = new Character(
            new Vector2(100, GameConfig.groundY - GameConfig.characterFrameSize),
            new PhysicsComponent(GameConfig.gravity),
            new KeyboardInputHandler(),
            collision,
            strategies,
            GameConfig.characterFrameSize, GameConfig.characterFrameSize, GameConfig.characterMoveSpeed
            );

            _sprite = new Sprite(GameConfig.characterFrameSize, GameConfig.characterFrameSize);
            _sprite.RegisterAnimation(CharacterState.Idle, idleTexture, 4, 0.2f);
            _sprite.RegisterAnimation(CharacterState.Running, runningTexture, 6, 0.12f);
            _sprite.RegisterAnimation(CharacterState.Jumping, jumpingTexture, 1, 0.1f, 64);
            _sprite.RegisterAnimation(CharacterState.Landing, landingTexture, 1, 0.1f, 64);
            _sprite.RegisterAnimation(CharacterState.Attacking, attackingTexture, 8, 0.5f, null, 80);

            // Load tilemap
            _tileset = Content.Load<Texture2D>("tilemap");
            var factory = new TileFactory(18, 1);

            // Load level via interface
            _currentLevel = _levelLoader.LoadLevel("Level1");

            // Create tilemap with level data
            _tileMap = new TileMap(
            _currentLevel.MapData,
            _tileset,
            80,
            factory,
            _currentLevel.Collision);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

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

            _tileMap.Draw(_spriteBatch, Vector2.Zero);

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
