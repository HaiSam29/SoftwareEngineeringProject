using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Enemy;
using PlatformGame.Interfaces.Map;

namespace PlatformGame.Classes.Enemy
{
    public class Enemy : IEnemy
    {
        public Vector2 Position;
        public Vector2 StartPosition;
        public int Direction = 1;
        public float Speed;
        public float PatrolDistance;
        public int Size;
        public ITileCollisionProvider CollisionProvider;
        public int TileSize;

        // Assets voor beide staten
        public Texture2D WalkTex;
        public Texture2D AttackTex;
        public Animation WalkAnim;
        public Animation AttackAnim;

        // Huidige weergave
        public Texture2D CurrentTexture;
        public Animation CurrentAnimation;

        // De State Machine
        private IEnemyState _currentState;

        public Rectangle Bounds => CalculateBounds();

        public Enemy(Vector2 position,
                     Texture2D walkTex, Texture2D attackTex,
                     Animation walkAnim, Animation attackAnim,
                     float speed, float patrolDistance,
                     ITileCollisionProvider collisionProvider, int tileSize, int size)
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

            // Begin met Patrol
            SetState(new PatrolState());
        }

        public void SetState(IEnemyState newState)
        {
            _currentState = newState;
            _currentState.Enter(this);
        }

        // Wordt aangeroepen vanuit PlayingState bij collision
        public void Attack()
        {
            // Alleen switchen als we niet al aanvallen
            if (!(_currentState is AttackState))
            {
                SetState(new AttackState());
            }
        }

        public void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime, this);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            // Geef camera offset door aan de state (die roept DrawHelper aan)
            // Omdat DrawHelper de offset nodig heeft slaan we hem tijdelijk op of passen we DrawHelper aan.
            _currentCameraOffset = cameraOffset;
            _currentState.Draw(spriteBatch, this);
        }

        private Vector2 _currentCameraOffset;

        // Hulpfunctie om dubbele draw code in states te voorkomen
        public void DrawHelper(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = CurrentAnimation.CurrentFrame;
            float scale = (float)Size / sourceRect.Height;

            // Origin onderaan/midden voor stabiele positie tijdens animatiewissels
            Vector2 origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);

            // Positie berekenen
            float centerX = Position.X + (Size / 2f);
            float centerY = Position.Y + Size - (sourceRect.Height * scale / 2f);

            Vector2 drawPos = new Vector2(centerX, centerY) + _currentCameraOffset;

            SpriteEffects effect = Direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(CurrentTexture, drawPos, sourceRect, Color.White, 0f, origin, scale, effect, 0f);
        }

        public bool ShouldTurnAround()
        {
            float distance = Position.X - StartPosition.X;
            if (distance >= PatrolDistance || distance <= -PatrolDistance) return true;

            int nextX = Direction > 0 ? (int)(Position.X + Size + 5) / TileSize : (int)(Position.X - 5) / TileSize;
            int groundY = (int)(Position.Y + Size + 2) / TileSize;
            int wallY = (int)(Position.Y + Size / 2) / TileSize;

            bool hasGround = CollisionProvider.HasCollision(nextX, groundY);
            bool hasWall = CollisionProvider.HasCollision(nextX, wallY);

            return !hasGround || hasWall;
        }

        private Rectangle CalculateBounds()
        {
            int sideMargin = 25;
            int topMargin = 20;
            return new Rectangle((int)Position.X + sideMargin, (int)Position.Y + topMargin, Size - (sideMargin * 2), Size - topMargin);
        }
    }
}
