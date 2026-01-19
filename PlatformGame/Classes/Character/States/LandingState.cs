using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character.States
{
    public class LandingState : CharacterStateBase
    {
        public override void Enter(ICharacterContext context)
        {
            context.CurrentStateEnum = CharacterState.Landing;
            if (context.LandingTimer <= 0) context.LandingTimer = 0.2f;
        }

        public override void HandleInput(ICharacterContext context)
        {
            base.HandleInput(context);

            if (context.Input.IsCrouchPressed())
            {
                context.TransitionTo(CharacterState.Crouching);
                return;
            }

            // Strategies (Jump cancel landings, of rennen uit landing)
            foreach (var strat in context.Strategies)
            {
                if (strat.CanExecute(context))
                {
                    strat.Execute(context);
                }
            }

            // Check resultaten
            if (context.Physics.Velocity.Y < 0)
            {
                context.TransitionTo(CharacterState.Jumping);
                return;
            }

            if (Math.Abs(context.Input.GetMoveDirectionX()) > 0)
            {
                context.TransitionTo(CharacterState.Running);
                return;
            }
        }

        public override void Update(ICharacterContext context, float deltaTime)
        {
            base.Update(context, deltaTime);

            if (context.LandingTimer <= 0)
            {
                context.TransitionTo(CharacterState.Idle);
            }

            if (!context.Collision.IsGrounded(context.GetHitbox(), out _))
            {
                context.TransitionTo(CharacterState.Falling);
            }
        }
    }
}
