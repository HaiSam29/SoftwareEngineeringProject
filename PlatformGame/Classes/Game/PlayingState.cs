using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Interfaces.Game;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using PlatformGame.Interfaces.Ilevel;
using PlatformGame.Interfaces.Map;
using System.Collections.Generic;
using CharacterClass = PlatformGame.Classes.Character.Character;
using EnemyClass = PlatformGame.Classes.Enemy.Enemy;
using EnemyManagerClass = PlatformGame.Classes.Enemy.EnemyManager;
using AnimationClass = PlatformGame.Classes.Enemy.Animation;
using LevelClass = PlatformGame.Classes.Level.Level;
using LevelLoaderClass = PlatformGame.Classes.Level.HardcodedLevelLoader;
using TileFactoryClass = PlatformGame.Classes.Map.TileFactory;
using TileMapClass = PlatformGame.Classes.Map.TileMap;
using BackgroundClass = PlatformGame.Classes.Map.Background;
using SpriteClass = PlatformGame.Classes.Character.Sprite;
using CollisionSystemClass = PlatformGame.Classes.Character.CollisionSystem;
using PhysicsComponentClass = PlatformGame.Classes.Character.PhysicsComponent;
using InputHandlerClass = PlatformGame.Classes.Character.KeyboardInputHandler;
using GroundedStratClass = PlatformGame.Classes.Character.GroundedMovementStrategy;
using JumpStratClass = PlatformGame.Classes.Character.JumpStrategy;

namespace PlatformGame.Classes.Game
{
    public class PlayingState : IGameState
    {
        private Game1 _game;

        private ICharacter _character;
        private ISprite _sprite;
        private ITileMapRenderer _tileMapRenderer;
        private ITileCollisionProvider _tileCollisionProvider;
        private ILevelLoader _levelLoader;
        private LevelClass _currentLevel;
        private BackgroundClass _background;
        private EnemyManagerClass _enemyManager;
        private Texture2D _tileset;
        private string _currentLevelName;

        public PlayingState(Game1 game, string levelName = "Level1")
        {
            _game = game;
            _currentLevelName = levelName;
            var Content = _game.Content;

            _levelLoader = new LevelLoaderClass();

            // Load background
            var backgroundTexture = Content.Load<Texture2D>("background");
            _background = new BackgroundClass(backgroundTexture, GameConfig.screenWidth, GameConfig.screenHeight);

            // Load character textures
            var idleTexture = Content.Load<Texture2D>("idle");
            var runningTexture = Content.Load<Texture2D>("Running");
            var jumpingTexture = Content.Load<Texture2D>("Jumping");
            var fallingTexture = Content.Load<Texture2D>("Falling");
            var landingTexture = Content.Load<Texture2D>("Landing");
            var attackingTexture = Content.Load<Texture2D>("Attacking");
            var crouchingTexture = Content.Load<Texture2D>("Crouching");

            // Create collision system
            var collision = new CollisionSystemClass();

            // Create movement strategies
            var strategies = new List<IMovementStrategy>
            {
                new GroundedStratClass(),
                new JumpStratClass(GameConfig.jumpForce)
            };

            int scaledCharacterSize = (int)(GameConfig.characterFrameSize * GameConfig.characterScale);
            int startTileX = 0;
            int groundTileY = 15;
            int tileSize = 60;

            Vector2 startPosition = new Vector2(
                startTileX * tileSize,
                (groundTileY * tileSize) - scaledCharacterSize
            );

            Rectangle screenBounds = new Rectangle(0, 0, GameConfig.screenWidth, GameConfig.screenHeight);

            _character = new CharacterClass(
                startPosition,
                new PhysicsComponentClass(GameConfig.gravity),
                new InputHandlerClass(),
                collision,
                strategies,
                screenBounds,
                scaledCharacterSize,
                scaledCharacterSize,
                GameConfig.characterMoveSpeed
            );

            // Register sprite animations
            _sprite = new SpriteClass(GameConfig.characterFrameSize, GameConfig.characterFrameSize);
            _sprite.RegisterAnimation(CharacterState.Idle, idleTexture, 4, 0.2f);
            _sprite.RegisterAnimation(CharacterState.Running, runningTexture, 6, 0.12f);
            _sprite.RegisterAnimation(CharacterState.Jumping, jumpingTexture, 1, 0.1f, 64);
            _sprite.RegisterAnimation(CharacterState.Falling, fallingTexture, 1, 0.1f, 64);
            _sprite.RegisterAnimation(CharacterState.Landing, landingTexture, 1, 0.1f, 64);
            _sprite.RegisterAnimation(CharacterState.Attacking, attackingTexture, 8, 0.5f, null, 80);
            _sprite.RegisterAnimation(CharacterState.Crouching, crouchingTexture, 1, 0.1f);

            // FORCEER INITIALISATIE (Zodat CurrentTexture niet null is)
            _sprite.Update(CharacterState.Idle, 0);

            // Load tilemap
            _tileset = Content.Load<Texture2D>("tilemap");
            ITileFactory factory = new TileFactoryClass(18, 1);
            _currentLevel = _levelLoader.LoadLevel(levelName);

            var tileMap = new TileMapClass(_currentLevel.MapData, _tileset, tileSize, factory);

            _tileMapRenderer = tileMap;
            _tileCollisionProvider = tileMap;

            collision.SetTileCollisionProvider(_tileCollisionProvider, tileSize);

            // ENEMY SETUP
            _enemyManager = new EnemyManagerClass();

            // Textures laden
            Texture2D enemy1Texture = Content.Load<Texture2D>("Enemy1Walk");
            Texture2D enemy2Texture = Content.Load<Texture2D>("Enemy2Walk");
            Texture2D enemy3Texture = Content.Load<Texture2D>("Enemy3Walk");

            int spriteFrameWidth = 96;
            int spriteFrameHeight = 96;
            int numberOfFrames = 7;
            var frames = new List<Rectangle>();
            for (int i = 0; i < numberOfFrames; i++) frames.Add(new Rectangle(i * spriteFrameWidth, 0, spriteFrameWidth, spriteFrameHeight));
            int enemySize = 72;

            // PLAATS ENEMIES OP BASIS VAN LEVEL NAAM
            if (levelName == "Level1")
            {
                // Level 1
                _enemyManager.AddEnemy(new EnemyClass(new Vector2(15 * tileSize, (2 * tileSize) + tileSize - enemySize), enemy1Texture, new AnimationClass(frames, 0.15f), 70f, 200f, _tileCollisionProvider, tileSize, enemySize));
                _enemyManager.AddEnemy(new EnemyClass(new Vector2(15 * tileSize, (8 * tileSize) + tileSize - enemySize), enemy2Texture, new AnimationClass(frames, 0.15f), 70f, 200f, _tileCollisionProvider, tileSize, enemySize));
                _enemyManager.AddEnemy(new EnemyClass(new Vector2(15 * tileSize, (14 * tileSize) + tileSize - enemySize), enemy3Texture, new AnimationClass(frames, 0.15f), 100f, 1000f, _tileCollisionProvider, tileSize, enemySize));
            }
            else if (levelName == "Level2")
            {
                // Level 2
                _enemyManager.AddEnemy(new EnemyClass(new Vector2(15 * tileSize, (2 * tileSize) + tileSize - enemySize), enemy1Texture, new AnimationClass(frames, 0.15f), 70f, 200f, _tileCollisionProvider, tileSize, enemySize));
                _enemyManager.AddEnemy(new EnemyClass(new Vector2(15 * tileSize, (8 * tileSize) + tileSize - enemySize), enemy2Texture, new AnimationClass(frames, 0.15f), 70f, 200f, _tileCollisionProvider, tileSize, enemySize));
                _enemyManager.AddEnemy(new EnemyClass(new Vector2(15 * tileSize, (14 * tileSize) + tileSize - enemySize), enemy3Texture, new AnimationClass(frames, 0.15f), 100f, 1000f, _tileCollisionProvider, tileSize, enemySize));
            }

        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _character.Update(deltaTime);
            _sprite.Update(_character.CurrentState, deltaTime);
            _enemyManager.Update(gameTime);

            // COLLISION LOGIC
            var hitEnemy = _enemyManager.CheckCollision(_character.GetHitbox());
            if (hitEnemy != null)
            {
                if (_character.CurrentState == CharacterState.Attacking)
                {
                    _enemyManager.RemoveEnemy(hitEnemy);
                }
                else
                {
                    _game.ChangeState(new GameOverState(_game));
                    return; // Stop update zodat we niet per ongeluk winnen in dezelfde frame
                }
            }

            // WIN CONDITIE
            // Als alle vijanden dood zijn:
            if (_enemyManager.EnemyCount == 0)
            {
                if (_currentLevelName == "Level1")
                {
                    // Ga naar Level 2
                    _game.ChangeState(new PlayingState(_game, "Level2"));
                }
                else if (_currentLevelName == "Level2")
                {
                    // Klaar! Naar Victory
                    _game.ChangeState(new VictoryState(_game));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _tileMapRenderer.Draw(spriteBatch, Vector2.Zero);
            _enemyManager.Draw(spriteBatch, Vector2.Zero);

            // VEILIGHEIDSCHECK: Teken alleen als er een texture is!
            if (_sprite.CurrentTexture != null)
            {
                Vector2 drawPosition = _character.Position + _sprite.CalculateDrawOffset(
                    GameConfig.characterFrameSize, GameConfig.characterScale);

                spriteBatch.Draw(
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
            }
        }
    }
}
