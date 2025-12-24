using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class CollisionSystem : ICollisionSystem
    {
        private const float GROUND_DETECTION_THRESHOLD = 5f;
        private readonly List<Rectangle> _colliders = new();

        public void AddCollider(Rectangle collider) => _colliders.Add(collider);

        public bool IsGrounded(Rectangle hitbox, out float groundY)
        {
            groundY = 0;
            foreach (var collider in _colliders)
            {
                if (hitbox.Bottom >= collider.Top &&
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

    }
}
