using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class GroundedMovementStrategy: IMovementStrategy
    {
        public void Execute(IPhysicsComponent physics, IInputHandler input,
                       bool isGrounded, float moveSpeed)
        {
            if (!isGrounded)
                return;

            float dirX = input.GetMoveDirectionX();
            physics.Velocity = new Vector2(dirX * moveSpeed, physics.Velocity.Y);
        }
    }
}
