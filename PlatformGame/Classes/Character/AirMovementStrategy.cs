using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class AirMovementStrategy: IMovementStrategy
    {
        private readonly float _airControlFactor;

        public AirMovementStrategy(float airControlFactor = 0.8f)
        {
            _airControlFactor = airControlFactor;
        }

        public void Execute(IPhysicsComponent physics, IInputHandler input, bool isGrounded, float moveSpeed)
        {
            if (isGrounded)
            {
                return;
            }

            var direction = input.GetMovementDirection();
            physics.Velocity = new System.Numerics.Vector2(
                physics.Velocity.X + direction.X * moveSpeed * _airControlFactor,
                physics.Velocity.Y);
        }
    }
}
