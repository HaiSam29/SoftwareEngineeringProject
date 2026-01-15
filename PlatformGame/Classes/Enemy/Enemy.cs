using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Enemy;
using PlatformGame.Interfaces.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Enemy
{
    public class Enemy: IEnemy
    {
        private Vector2 _position;
        private Vector2 _startPosition;
        private int _direction;
        private readonly int _size;
        private readonly float _speed;
        private readonly float _patrolDistance;
        private readonly Texture2D _texture;
        private readonly Animation _animation;
        private readonly ITileCollisionProvider _collisionProvider;
        private readonly int _tileSize;

        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, _size, _size);

        public Enemy(Vector2 position, Texture2D texture, Animation animation,
            float speed, float patrolDistance, ITileCollisionProvider collisionProvider, int tileSize, int size)
        {
            _position = position;
            _startPosition = position;
            _texture = texture;
            _animation = animation;
            _speed = speed;
            _patrolDistance = patrolDistance;
            _collisionProvider = collisionProvider;
            _tileSize = tileSize;
            _size = size;
            _direction = 1;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check of moet omkeren
            if (ShouldTurnAround())
            {
                _direction *= -1;
            }

            // Beweeg
            _position.X += _speed * _direction * deltaTime;

            // Update animatie
            _animation.Update(deltaTime);
        }

        private bool ShouldTurnAround()
        {
            // Check patrol limiet
            float distance = _position.X - _startPosition.X;
            if (distance >= _patrolDistance || distance <= -_patrolDistance)
                return true;

            // Check cliff en muur
            int nextX = _direction > 0 ? (int)(_position.X + _size + 2) / _tileSize : (int)(_position.X - 2) / _tileSize;
            int groundY = (int)(_position.Y + _size + 2) / _tileSize;
            int wallY = (int)(_position.Y + _size / 2) / _tileSize;

            bool hasGround = _collisionProvider.HasCollision(nextX, groundY);
            bool hasWall = _collisionProvider.HasCollision(nextX, wallY);

            return !hasGround || hasWall;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            Rectangle sourceRect = _animation.CurrentFrame;

            // 1. BEREKEN SCHAAL
            float scale = (float)_size / sourceRect.Width;

            // 2. ORIGIN (Draaipunt)
            Vector2 origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);

            // 3. POSITIE
            float centerX = _position.X + (_size / 2f);
            float centerY = _position.Y + (_size / 2f);

            centerY = _position.Y + _size - (sourceRect.Height * scale / 2f);

            Vector2 drawPos = new Vector2(centerX, centerY) + cameraOffset;

            SpriteEffects effect = _direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(
                _texture,
                drawPos,
                sourceRect,
                Color.White,
                0f,
                origin,     
                scale,     
                effect,    
                0f
            );
        }
    }
}
