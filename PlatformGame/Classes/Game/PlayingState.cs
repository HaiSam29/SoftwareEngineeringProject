using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Interfaces.Game;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using PlatformGame.Interfaces.Ilevel;
using PlatformGame.Interfaces.Map;
using System.Collections.Generic;
using PlatformGame.Classes.Enemy;
using PlatformGame.Classes.Character;
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
using PlatformGame.Classes.Level;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Interfaces.Enemy;

namespace PlatformGame.Classes.Game
{
    // Speel state
    // SRP Coördineert veel, maar de details zijn uitbesteed
    // Levelcreatie → LevelFactory
    // Character → CharacterFactory
    // Enemies → EnemyFactory + EnemyManager
    // Collision bij enemy → EnemyManager.CheckCollision + enemy Attack/TakeDamage
    // Voor een spelstate is dit een gebruikelijke hoeveelheid verantwoordelijkheid
    // DIP Ontvangt IGameConfig via constructor en Gebruikt interfaces voor alle game‑objecten
    // OCP Nieuwe enemies of beweging → via factories/strategies, zonder PlayingState te wijzigen
    // Nieuwe levels → via LevelFactory/LevelLoader
    public class PlayingState : IGameState
    {
        private Game1 _game;
        private IGameConfig _config;
        private SpriteFont _font;

        private ICharacter _character;
        private ISprite _sprite;
        private EnemyManagerClass _enemyManager;

        private ITileMapRenderer _tileMapRenderer;
        private ITileCollisionProvider _tileCollisionProvider;
        private LevelClass _currentLevel;
        private BackgroundClass _background;
        private string _currentLevelName;

        private Texture2D _redPixel;
        private float _damageFlashOpacity = 0f;

        public PlayingState(Game1 game, IGameConfig config, string levelName = "Level1")
        {
            _game = game;
            _config = config;
            _currentLevelName = levelName;
            var Content = _game.Content;

            _font = Content.Load<SpriteFont>("GameFont");
            _redPixel = new Texture2D(_game.GraphicsDevice, 1, 1);
            _redPixel.SetData(new[] { Color.Red });

            // LEVEL SETUP 
            var levelFactory = new LevelFactory(Content, _config);

            _background = levelFactory.CreateBackground();
            (_currentLevel, _tileMapRenderer, _tileCollisionProvider) = levelFactory.CreateLevel(levelName);

            int tileSize = 60;

            // CHARACTER SETUP 
            var charFactory = new CharacterFactory(Content, _config);

            int startTileX = 0;
            int groundTileY = 15;
            int charSize = (int)(_config.CharacterFrameSize * _config.CharacterScale);

            Vector2 startPos = new Vector2(startTileX * tileSize, (groundTileY * tileSize) - charSize);
            Rectangle screenBounds = new Rectangle(0, 0, _config.ScreenWidth, _config.ScreenHeight);

            (_character, _sprite) = charFactory.CreateCharacter(startPos, screenBounds, _tileCollisionProvider, tileSize);

            //  ENEMY SETUP 
            // Maak renderer en boundsProvider aan
            IEnemyRenderer enemyRenderer = new EnemyRenderer();
            IBoundsProvider boundsProvider = new DefaultBoundsProvider(sideMargin: 25, topMargin: 20);
            IEnemyStateFactory stateFactory = new EnemyStateFactory();

            // Geef renderer en boundsProvider mee
            var enemyFactory = new EnemyFactory(Content, _config, stateFactory, enemyRenderer, boundsProvider);
            _enemyManager = new EnemyManagerClass();

            LoadEnemies(levelName, enemyFactory, tileSize);
        }

        private void LoadEnemies(string levelName, EnemyFactory factory, int tileSize)
        {
            int enemySize = 72;

            if (levelName == "Level1")
            {
                AddEnemy(factory, EnemyType.Normal, 15, 2, tileSize, enemySize);
                AddEnemy(factory, EnemyType.Fast, 15, 8, tileSize, enemySize);
                AddEnemy(factory, EnemyType.Strong, 15, 14, tileSize, enemySize);
            }
            else if (levelName == "Level2")
            {
                AddEnemy(factory, EnemyType.Normal, 15, 5, tileSize, enemySize);
                AddEnemy(factory, EnemyType.Fast, 7, 7, tileSize, enemySize);
                AddEnemy(factory, EnemyType.Strong, 23, 7, tileSize, enemySize);
            }
        }

        private void AddEnemy(EnemyFactory factory, EnemyType type, int tileX, int tileY, int tileSize, int size)
        {
            Vector2 pos = new Vector2(
                tileX * tileSize,
                (tileY * tileSize) + tileSize - size
            );

            var enemy = factory.CreateEnemy(type, pos, _tileCollisionProvider, tileSize);
            _enemyManager.AddEnemy(enemy);
        }

        // Checkt eerst of de speler dood is → zo ja direct naar GameOverState
        // Anders:
        // _character.Update
        // _sprite.Update
        // _enemyManager.Update
        // CheckCollisions
        // Damage flash effect laten aflopen
        // Winconditie
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_character.Health <= 0)
            {
                _game.ChangeState(new GameOverState(_game, _config));
                return;
            }

            _character.Update(deltaTime);
            _sprite.Update(_character.CurrentState, deltaTime);
            _enemyManager.Update(gameTime);

            CheckCollisions();

            if (_damageFlashOpacity > 0)
            {
                _damageFlashOpacity -= 2f * deltaTime;
                if (_damageFlashOpacity < 0) _damageFlashOpacity = 0;
            }

            if (_enemyManager.EnemyCount == 0)
            {
                if (_currentLevelName == "Level1")
                    _game.ChangeState(new PlayingState(_game, _config, "Level2"));
                else if (_currentLevelName == "Level2")
                    _game.ChangeState(new VictoryState(_game, _config));
            }
        }

        // Vraagt _enemyManager.CheckCollision(_character.GetHitbox())
        // Als hit: 
        // Als character in Attacking‑state → enemy wordt verwijderd
        // Anders → enemy krijgt Attack(), character krijgt TakeDamage() en damage flash wordt geactiveerd.
        private void CheckCollisions()
        {
            var hitEnemy = _enemyManager.CheckCollision(_character.GetHitbox());
            if (hitEnemy != null)
            {
                if (_character.CurrentState == CharacterState.Attacking)
                    _enemyManager.RemoveEnemy(hitEnemy);
                else
                {
                    hitEnemy.Attack();
                    if (_character.TakeDamage())
                        _damageFlashOpacity = 0.6f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _tileMapRenderer.Draw(spriteBatch, Vector2.Zero);
            _enemyManager.Draw(spriteBatch, Vector2.Zero);

            if (_sprite.CurrentTexture != null)
            {
                Vector2 drawPosition = _character.Position + _sprite.CalculateDrawOffset(
                    _config.CharacterFrameSize, _config.CharacterScale);

                Color drawColor = _character.IsInvulnerable ? Color.White * 0.5f : Color.White;

                spriteBatch.Draw(
                    _sprite.CurrentTexture,
                    drawPosition,
                    _sprite.CurrentFrame,
                    drawColor,
                    0f,
                    Vector2.Zero,
                    _config.CharacterScale,
                    _character.FacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    0f
                );
            }

            DrawHUD(spriteBatch);
        }

        private void DrawHUD(SpriteBatch spriteBatch)
        {
            string text = $"LIVES: {_character.Health}";
            Vector2 pos = new Vector2(20, 20);
            spriteBatch.DrawString(_font, text, pos + new Vector2(2, 2), Color.Black);
            Color textColor = _character.Health == 1 ? Color.Red : Color.White;
            spriteBatch.DrawString(_font, text, pos, textColor);

            if (_damageFlashOpacity > 0)
            {
                spriteBatch.Draw(_redPixel,
                    new Rectangle(0, 0, _config.ScreenWidth, _config.ScreenHeight),
                    Color.Red * _damageFlashOpacity);
            }
        }
    }
}
