using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Enemy;
using PlatformGame.Interfaces.Map;
using PlatformGame.Enums;
using System;

namespace PlatformGame.Classes.Enemy
{
    // Dit is de centrale hub van een vijand.
    // Blijft de “context” voor states/strategies en bewaart data
    // maar tekent niet zelf en berekent
    // SRP heeft duidelijke verantwoorderlijkheid
    // DIP is sterker omdat Enemy nu afhankelijk is van IEnemyRenderer en IBoundsProvider abstracties
    public class Enemy : IEnemy, IEnemyContext, IEnemyRenderData
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

        // vertaalt CurrentAnimation.CurrentFrame naar een Rectangle dat de renderer kan gebruiken.
        // Als er geen animatie is, geef je Rectangle.Empty terug
        public Rectangle CurrentSourceRect => CurrentAnimation?.CurrentFrame ?? Rectangle.Empty;

        // Strategy geïnjecteerd
        public IEnemyMovementStrategy MovementStrategy { get; private set; }

        // Renderer en BoundsProvider geïnjecteerd
        private readonly IEnemyRenderer _renderer;
        private readonly IBoundsProvider _boundsProvider;

        // State Machine
        private readonly IEnemyStateFactory _stateFactory;
        private IEnemyState _currentState;
        private EnemyStateType _currentStateType;

        // Bounds berekend via BoundsProvider
        public Rectangle Bounds => _boundsProvider.GetBounds(Position, Size);

        public Enemy(Vector2 position,
                     Texture2D walkTex, Texture2D attackTex,
                     Animation walkAnim, Animation attackAnim,
                     float speed, float patrolDistance,
                     ITileCollisionProvider collisionProvider, int tileSize, int size,
                     IEnemyStateFactory stateFactory,
                     IEnemyMovementStrategy movementStrategy,
                     IEnemyRenderer renderer,              
                     IBoundsProvider boundsProvider)        
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
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _boundsProvider = boundsProvider ?? throw new ArgumentNullException(nameof(boundsProvider));

            TransitionTo(EnemyStateType.Patrol);
        }

        // bewaart _currentStateType, vraagt via _stateFactory.Create(stateType) de juiste state op en roept Enter(this) aan
        // Zodat die state zijn animatie/waarden kan initialiseren.
        public void TransitionTo(EnemyStateType stateType)
        {
            _currentStateType = stateType;
            _currentState = _stateFactory.Create(stateType);
            _currentState.Enter(this);
        }

        public void Attack()
        {
            if (_currentStateType != EnemyStateType.Attack)
            {
                TransitionTo(EnemyStateType.Attack);
            }
        }

        public void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime, this);
        }

        // Draw delegeert naar renderer
        public void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            _renderer.Draw(spriteBatch, this, cameraOffset);
        }

    }
}
