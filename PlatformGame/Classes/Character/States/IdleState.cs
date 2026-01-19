using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;

namespace PlatformGame.Classes.Character.States
{
    public class IdleState: CharacterStateBase
    {
        // Zet snelheid op 0.
        // Checkt continu: "Wordt er op een pijltoets gedrukt?" -> Ga naar Running.
        // "Wordt er gesprongen?" -> Ga naar Jumping.
        public override void Enter(ICharacterContext context)
        {
            context.CurrentStateEnum = CharacterState.Idle;
            context.Physics.Velocity = new Vector2(0, context.Physics.Velocity.Y);
        }

        public override void HandleInput(ICharacterContext context)
        {
            base.HandleInput(context); // Checks attack

            if (context.Input.IsCrouchPressed())
            {
                context.TransitionTo(CharacterState.Crouching);
                return;
            }

            // UNIFORME STRATEGY UITVOERING
            foreach (var strat in context.Strategies)
            {
                if (strat.CanExecute(context))
                {
                    strat.Execute(context);
                }
            }

            // Check transities op basis van wat strategies deden
            if (context.Physics.Velocity.Y < 0) // Jumped?
            {
                context.TransitionTo(CharacterState.Jumping);
                return;
            }

            if (Math.Abs(context.Input.GetMoveDirectionX()) > 0) // Moved?
            {
                context.TransitionTo(CharacterState.Running);
            }
        }

        public override void Update(ICharacterContext context, float deltaTime)
        {
            base.Update(context, deltaTime);

            if (!context.Collision.IsGrounded(context.GetHitbox(), out _))
            {
                context.TransitionTo(CharacterState.Falling);
            }
        }
    }
}
