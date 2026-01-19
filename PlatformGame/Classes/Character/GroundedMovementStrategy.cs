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
        public bool CanExecute(ICharacterContext context)
        {
            // Deze strategie is altijd actief zolang er input is (of altijd, afhankelijk van je game)
            // Je zou hier ook "IsGrounded" kunnen checken als je air-control wil verbieden
            return true;
        }

        public void Execute(ICharacterContext context)
        {
            float dirX = context.Input.GetMoveDirectionX();

            // Pas snelheid toe
            context.Physics.Velocity = new Vector2(dirX * context.MoveSpeed, context.Physics.Velocity.Y);
        }
    }
}
