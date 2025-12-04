using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    public interface ICollisionSystem
    {
        void AddCollider(Rectangle collider);
        bool CheckGround(Rectangle hitbox, out float groundY);
        bool CheckCeiling(Rectangle hitbox, out float ceilingY);
        bool CheckWall(Rectangle hitbox, Vector2 velocity);
    }
}
