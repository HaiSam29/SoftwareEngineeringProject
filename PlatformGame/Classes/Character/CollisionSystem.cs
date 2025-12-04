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
        private readonly List<Rectangle> _colliders = new();

        public void AddCollider(Rectangle collider) => _colliders.Add(collider);

        public bool IsGrounded(Rectangle hitbox, out float groundY)
        {
            groundY = 0;
            foreach (var collider in _colliders)
            {
                // simpele check: onderkant van hitbox raakt/overlapt bovenkant van collider
                if (hitbox.Bottom >= collider.Top &&
                    hitbox.Bottom <= collider.Top + 5 &&    // kleine marge
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
