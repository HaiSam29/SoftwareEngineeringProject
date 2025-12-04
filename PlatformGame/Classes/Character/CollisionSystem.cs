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
        private readonly List<Rectangle> _colliders;

        public CollisionSystem()
        {
            _colliders = new List<Rectangle>();
        }

        public void AddCollider(Rectangle collider)
        {
            _colliders.Add(collider);
        }

        public bool CheckGround(Rectangle hitbox, out float groundY)
        {
            groundY = 0;

            foreach (var collider in _colliders)
            {
                if (hitbox.Intersects(collider) && hitbox.Bottom >= collider.Top && hitbox.Bottom <= collider.Top + 20)
                {
                    groundY = collider.Top;
                    return true;
                }
            }

            return false;
        }

        public bool CheckCeiling(Rectangle hitbox, out float ceilingY)
        {
            ceilingY = 0;

            foreach (var collider in _colliders)
            {
                if (hitbox.Intersects(collider) && hitbox.Top <= collider.Bottom)
                {
                    ceilingY = collider.Bottom;
                    return true;
                }
            }

            return false;
        }

        public bool CheckWall(Rectangle hitbox, Vector2 velocity)
        {
            foreach (var collider in _colliders)
            {
                if (hitbox.Intersects(collider))
                {
                    if (velocity.X > 0 && hitbox.Right > collider.Left)
                    {
                        return true;
                    }

                    if (velocity.X < 0 && hitbox.Left < collider.Right)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
