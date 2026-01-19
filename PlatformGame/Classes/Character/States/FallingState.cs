using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using PlatformGame.Interfaces.Character;

namespace PlatformGame.Classes.Character.States
{
    public class FallingState : CharacterStateBase
    {
        public override void Enter(ICharacterContext context)
        {
            context.CurrentStateEnum = CharacterState.Falling;
        }

        public override void HandleInput(ICharacterContext context)
        {
            // Air Control: Voer strategies uit
            // Omdat we vallen, zal JumpStrategy.CanExecute() false teruggeven (want niet grounded)
            // Maar GroundedMovementStrategy (of een aparte AirStrategy) kan true geven voor links/rechts
            foreach (var strat in context.Strategies)
            {
                if (strat.CanExecute(context))
                    strat.Execute(context);
            }
        }

        public override void Update(ICharacterContext context, float deltaTime)
        {
            base.Update(context, deltaTime);

            if (context.Collision.IsGrounded(context.GetHitbox(), out float groundY))
            {
                // Snap to ground
                context.Position = new Vector2(context.Position.X, groundY - context.FrameHeight);
                context.Physics.Velocity = new Vector2(context.Physics.Velocity.X, 0);

                context.LandingTimer = 0.2f; // Hardcoded of context property
                context.TransitionTo(CharacterState.Landing);
            }
        }
    }
}
