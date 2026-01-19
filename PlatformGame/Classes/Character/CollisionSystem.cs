using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformGame.Interfaces.Map;

namespace PlatformGame.Classes.Character
{
    // Voorkomt dat de speler door muren of vloeren zakt.
    public class CollisionSystem : ICollisionSystem
    {
        private const float GROUND_DETECTION_THRESHOLD = 5f;
        private readonly List<Rectangle> _colliders = new();
        private ITileCollisionProvider _tileCollisionProvider;
        private int _tileSize;

        public void SetTileCollisionProvider(ITileCollisionProvider provider, int tileSize)
        {
            _tileCollisionProvider = provider;
            _tileSize = tileSize;
        }

        public void AddCollider(Rectangle collider) => _colliders.Add(collider);

        // Deze methode kijkt of er vlak onder de voeten van de speler hitbox.Bottom + een klein beetje marge een tegel of platform zit.
        // Zo weet de JumpStrategy of je mag springen.
        public bool IsGrounded(Rectangle hitbox, out float groundY)
        {
            groundY = 0;

            // Check tile collisions
            if (_tileCollisionProvider != null)
            {
                int leftTileX = hitbox.Left / _tileSize;
                int rightTileX = (hitbox.Right - 1) / _tileSize;
                int bottomTileY = (hitbox.Bottom - 1) / _tileSize;
                int checkTileY = bottomTileY + 1; // Tile directly below

                // Check tiles directly below character's feet
                for (int x = leftTileX; x <= rightTileX; x++)
                {
                    if (_tileCollisionProvider.HasCollision(x, checkTileY))
                    {
                        float tileTop = checkTileY * _tileSize;
                        float distance = hitbox.Bottom - tileTop;

                        // More lenient grounded check
                        if (distance >= -2 && distance <= GROUND_DETECTION_THRESHOLD)
                        {
                            groundY = tileTop;
                            return true;
                        }
                    }
                }
            }

            // Check static colliders 
            foreach (var collider in _colliders)
            {
                if (hitbox.Bottom >= collider.Top - 2 &&
                    hitbox.Bottom <= collider.Top + GROUND_DETECTION_THRESHOLD &&
                    hitbox.Right > collider.Left &&
                    hitbox.Left < collider.Right)
                {
                    groundY = collider.Top;
                    return true;
                }
            }

            return false;
        }

        // Dit gebeurt in twee stappen X en Y apart om haken aan hoekjes te voorkomen.
        // Beweeg de speler eerst horizontaal. Raak je een muur? Stop horizontale snelheid.
        // Beweeg de speler daarna verticaal. Raak je de grond? Stop verticale snelheid en zet speler precies op de grond.
        public Vector2 ResolveCollision(Rectangle hitbox, Vector2 velocity, float deltaTime)
        {
            if (_tileCollisionProvider == null)
                return velocity;

            Vector2 newVelocity = velocity;

            // Resolve horizontal collision
            newVelocity.X = CheckHorizontalCollision(hitbox, velocity.X, deltaTime);

            // Update hitbox for vertical check
            var tempHitbox = new Rectangle(
                hitbox.X + (int)(newVelocity.X * deltaTime),
                hitbox.Y,
                hitbox.Width,
                hitbox.Height
            );

            // Resolve vertical collision
            newVelocity.Y = CheckVerticalCollision(tempHitbox, velocity.Y, deltaTime);

            return newVelocity;
        }

        private float CheckHorizontalCollision(Rectangle hitbox, float velocityX, float deltaTime)
        {
            if (Math.Abs(velocityX) < 0.1f) return velocityX;

            float movement = velocityX * deltaTime;
            bool movingRight = movement > 0;

            // Calculate which column of tiles to check
            int checkColumn = movingRight
                ? (hitbox.Right + (int)Math.Ceiling(Math.Abs(movement))) / _tileSize
                : (hitbox.Left + (int)Math.Floor(movement)) / _tileSize;

            // Calculate row range
            int topRow = hitbox.Top / _tileSize;
            int bottomRow = (hitbox.Bottom - 1) / _tileSize;

            // Check all tiles in the column that the player could hit
            for (int row = topRow; row <= bottomRow; row++)
            {
                if (_tileCollisionProvider.HasCollision(checkColumn, row))
                {
                    return 0; // Stop horizontal movement
                }
            }

            return velocityX; // No collision
        }

        private float CheckVerticalCollision(Rectangle hitbox, float velocityY, float deltaTime)
        {
            if (Math.Abs(velocityY) < 0.1f) return velocityY;

            float movement = velocityY * deltaTime;
            bool movingDown = movement > 0;

            // Calculate which row of tiles to check
            int checkRow = movingDown
                ? (hitbox.Bottom + (int)Math.Ceiling(Math.Abs(movement))) / _tileSize
                : (hitbox.Top + (int)Math.Floor(movement)) / _tileSize;

            // Calculate column range
            int leftColumn = hitbox.Left / _tileSize;
            int rightColumn = (hitbox.Right - 1) / _tileSize;

            // Check all tiles in the row that the player could hit
            for (int col = leftColumn; col <= rightColumn; col++)
            {
                if (_tileCollisionProvider.HasCollision(col, checkRow))
                {
                    return 0; // Stop vertical movement
                }
            }

            return velocityY; // No collision
        }
    }
}
