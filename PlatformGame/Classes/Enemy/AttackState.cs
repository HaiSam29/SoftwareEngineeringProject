using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformGame.Interfaces.Enemy;
using EnemyClass = PlatformGame.Classes.Enemy.Enemy;
using PlatformGame.Enums;

namespace PlatformGame.Classes.Enemy
{
    public class AttackState : IEnemyState
    {
        public void Enter(IEnemyContext context)
        {
            context.CurrentAnimation = context.AttackAnim;
            context.CurrentTexture = context.AttackTex;
            context.CurrentAnimation.Reset();
        }

        public void Update(GameTime gameTime, IEnemyContext context)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            context.CurrentAnimation.Update(dt);

            if (context.CurrentAnimation.IsFinished)
            {
                // OUD: context.SetState(new PatrolState());
                // NIEUW:
                context.TransitionTo(EnemyStateType.Patrol);
            }
        }

        public void Draw(SpriteBatch spriteBatch, IEnemyContext context)
        {
            context.DrawHelper(spriteBatch);
        }
    }
}
