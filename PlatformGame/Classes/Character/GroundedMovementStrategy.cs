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
    // Dit zijn beweging.
    // Leest input (-1 of 1) en zet horizontale snelheid.
    // OCP Wil je iets toevoegen. Maak een nieuwe class DoubleJumpStrategy en voeg die toe aan de lijst in de Factory.
    // Je hoeft de Character class niet open te breken.
    public class GroundedMovementStrategy: IMovementStrategy
    {
        public bool CanExecute(ICharacterContext context)
        {
            // Deze strategie is altijd actief zolang er input is of altijd, afhankelijk van je game
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
