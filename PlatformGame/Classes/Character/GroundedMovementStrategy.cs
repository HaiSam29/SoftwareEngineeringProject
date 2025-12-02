using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class GroundedMovementStrategy: IMovementStrategy
    {
        public void Execute(IPhysicsComponent physics, IInputHandler input, bool isGrounded, float moveSpeed)
        {
            if (!isGrounded)
            {
                return;
            }

            var direction = input.GetMovementDirection();
            physics.Velocity = new System.Numerics.Vector2(direction.X * moveSpeed, physics.Velocity.Y);
        }
    }
}
