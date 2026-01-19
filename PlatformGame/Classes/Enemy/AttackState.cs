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
    // De aanval state.
    // Speelt de attack-animatie af, en zodra die klaar is IsFinished, gaat de enemy terug naar PatrolState.
    // SRP Doet alleen attack-gedrag
    // State Pattern: De enemy hoeft niet te weten wanneer een aanval klaar is; de state beslist dat.
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
                context.TransitionTo(EnemyStateType.Patrol);
            }
        }

        public void Draw(SpriteBatch spriteBatch, IEnemyContext context)
        {
            // States doen niet langer rendering 
        }
    }
}
