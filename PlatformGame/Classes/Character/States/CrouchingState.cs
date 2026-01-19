using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Character;

namespace PlatformGame.Classes.Character.States
{
    public class CrouchingState : CharacterStateBase
    {
        public override void Enter(ICharacterContext context)
        {
            context.CurrentStateEnum = CharacterState.Crouching;
            context.Physics.Velocity = new Vector2(0, context.Physics.Velocity.Y);
        }

        public override void HandleInput(ICharacterContext context)
        {
            if (!context.Input.IsCrouchPressed())
            {
                context.TransitionTo(CharacterState.Idle);
                return;
            }

            if (context.Input.IsAttackPressed())
            {
                context.TransitionTo(CharacterState.Attacking);
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
