using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Enemy;
using PlatformGame.Interfaces.Map;
using PlatformGame.Enums;
using System;

namespace PlatformGame.Classes.Enemy
{
    public class Enemy : IEnemy, IEnemyContext
    {
        // Properties
        public Vector2 Position { get; set; }
        public Vector2 StartPosition { get; private set; }
        public int Direction { get; set; } = 1;
        public float Speed { get; private set; }
        public float PatrolDistance { get; private set; }
        public int Size { get; private set; }

        public ITileCollisionProvider CollisionProvider { get; private set; }
        public int TileSize { get; private set; }

        public Texture2D CurrentTexture { get; set; }
        public Animation CurrentAnimation { get; set; }
        public Texture2D WalkTex { get; private set; }
        public Texture2D AttackTex { get; private set; }
        public Animation WalkAnim { get; private set; }
        public Animation AttackAnim { get; private set; }

        // Strategy (geïnjecteerd)
        public IEnemyMovementStrategy MovementStrategy { get; private set; }

        // State Machine
        private readonly IEnemyStateFactory _stateFactory;
        private IEnemyState _currentState;
        private Vector2 _currentCameraOffset;

        public Rectangle Bounds => CalculateBounds();

        public Enemy(Vector2 position,
                     Texture2D walkTex, Texture2D attackTex,
                     Animation walkAnim, Animation attackAnim,
                     float speed, float patrolDistance,
                     ITileCollisionProvider collisionProvider, int tileSize, int size,
                     IEnemyStateFactory stateFactory,
                     IEnemyMovementStrategy movementStrategy) // NIEUW: Injectie
        {
            Position = position;
            StartPosition = position;

            WalkTex = walkTex;
            AttackTex = attackTex;
            WalkAnim = walkAnim;
            AttackAnim = attackAnim;

            Speed = speed;
            PatrolDistance = patrolDistance;
            CollisionProvider = collisionProvider;
            TileSize = tileSize;
            Size = size;

            _stateFactory = stateFactory ?? throw new ArgumentNullException(nameof(stateFactory));
            MovementStrategy = movementStrategy ?? throw new ArgumentNullException(nameof(movementStrategy));

            // Start State
            TransitionTo(EnemyStateType.Patrol);
        }

        public void TransitionTo(EnemyStateType stateType)
        {
            _currentState = _stateFactory.Create(stateType);
            _currentState.Enter(this);
        }

        public void Attack()
        {
            if (!(_currentState is AttackState))
            {
                TransitionTo(EnemyStateType.Attack);
            }
        }

        public void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime, this);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            _currentCameraOffset = cameraOffset;
            _currentState.Draw(spriteBatch, this);
        }

        public void DrawHelper(SpriteBatch spriteBatch)
        {
            if (CurrentAnimation == null || CurrentTexture == null) return;

            Rectangle sourceRect = CurrentAnimation.CurrentFrame;
            float scale = (float)Size / sourceRect.Height;
            Vector2 origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);
            float centerX = Position.X + (Size / 2f);
            float centerY = Position.Y + Size - (sourceRect.Height * scale / 2f);
            Vector2 drawPos = new Vector2(centerX, centerY) + _currentCameraOffset;
            SpriteEffects effect = Direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(CurrentTexture, drawPos, sourceRect, Color.White, 0f, origin, scale, effect, 0f);
        }

        private Rectangle CalculateBounds()
        {
            int sideMargin = 25;
            int topMargin = 20;
            return new Rectangle((int)Position.X + sideMargin, (int)Position.Y + topMargin, Size - (sideMargin * 2), Size - topMargin);
        }
    }
}
