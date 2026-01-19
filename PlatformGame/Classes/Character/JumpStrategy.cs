using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    // Dit zijn beweging.
    // Kijkt of je op de grond staat en drukt op springen -> geeft verticale snelheid omhoog.
    // OCP Wil je iets toevoegen. Maak een nieuwe class DoubleJumpStrategy en voeg die toe aan de lijst in de Factory.
    // Je hoeft de Character class niet open te breken.
    public class JumpStrategy: IMovementStrategy
    {
        private readonly float _jumpForce;
        private bool _wasJumpPressed;

        public JumpStrategy(float jumpForce = 400f)
        {
            _jumpForce = jumpForce;
        }

        public bool CanExecute(ICharacterContext context)
        {
            // Jump mag alleen als we op de grond staan
            // We checken dit direct via de context
            return context.Collision.IsGrounded(context.GetHitbox(), out _);
        }

        public void Execute(ICharacterContext context)
        {
            bool isJumpPressed = context.Input.IsJumpPressed();

            // Omdat CanExecute al true teruggaf, weten we dat we grounded zijn.
            // We checken alleen nog de input.
            if (isJumpPressed && !_wasJumpPressed)
            {
                context.Physics.Velocity = new Vector2(context.Physics.Velocity.X, -_jumpForce);
            }

            _wasJumpPressed = isJumpPressed;
        }
    }
}
