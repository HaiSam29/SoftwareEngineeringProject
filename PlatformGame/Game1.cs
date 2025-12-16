using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Classes.Character;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var idleTexture = Content.Load<Texture2D>("idle");
            var runningTexture = Content.Load<Texture2D>("Running");
            var jumpingTexture = Content.Load<Texture2D>("Jumping");
            var landingTexture = Content.Load<Texture2D>("Landing");

            var collision = new CollisionSystem();
            collision.AddCollider(new Rectangle(0, 450, 800, 50));

            var strategies = new List<IMovementStrategy> { new GroundedMovementStrategy(), new JumpStrategy(300f) };

            _character = new Character(
                new Vector2(100, 450 - 48),
                new PhysicsComponent(800f),
                new KeyboardInputHandler(),
                collision,
                strategies,
                48, 48, 150f
            );

            _sprite = new Sprite(48, 48);
            _sprite.RegisterAnimation(CharacterState.Idle, idleTexture, 4, 0.2f);
            _sprite.RegisterAnimation(CharacterState.Running, runningTexture, 6, 0.12f);
            _sprite.RegisterAnimation(CharacterState.Jumping, jumpingTexture, 1, 0.1f, 64);
            _sprite.RegisterAnimation(CharacterState.Landing, landingTexture, 1, 0.1f, 64);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _character.Update(deltaTime);
            _sprite.Update(_character.CurrentState, deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            Vector2 drawPosition = _character.Position;

            if (_sprite.CurrentFrame.Height > characterFrameSize)
            {
                drawPosition.Y -= (_sprite.CurrentFrame.Height - characterFrameSize);
            }

            _spriteBatch.Draw(
                _sprite.CurrentTexture,
                drawPosition,
                _sprite.CurrentFrame,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                _character.FacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f
            );

            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
