using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character.States
{
    public class RunningState : CharacterStateBase
    {
        public override void Enter(ICharacterContext context)
        {
            context.CurrentStateEnum = CharacterState.Running;
        }

        public override void HandleInput(ICharacterContext context)
        {
            base.HandleInput(context);

            if (context.Input.IsCrouchPressed())
            {
                context.TransitionTo(CharacterState.Crouching);
                return;
            }

            // Uniforme Strategy Call
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
            }

            if (Math.Abs(context.Input.GetMoveDirectionX()) == 0)
            {
                context.TransitionTo(CharacterState.Idle);
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
