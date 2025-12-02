using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    public interface IPhysicsComponent
    {
        Vector2 Velocity { get; set; }
        void ApplyGravity(float deltaTime);
        void ApplyVelocity(ref Vector2 position, float deltaTime);
    }
}
