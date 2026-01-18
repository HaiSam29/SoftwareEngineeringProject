using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformGame.Interfaces.Enemy;
using EnemyClass = PlatformGame.Classes.Enemy.Enemy;

namespace PlatformGame.Classes.Enemy
{
    public class AttackState : IEnemyState
    {
        public void Enter(EnemyClass enemy)
        {
            enemy.CurrentAnimation = enemy.AttackAnim;
            enemy.CurrentTexture = enemy.AttackTex;
            enemy.CurrentAnimation.Reset();
        }

        public void Update(GameTime gameTime, EnemyClass enemy)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Tijdens attack bewegen we niet alleen animatie updaten
            enemy.CurrentAnimation.Update(dt);

            // Als klaar -> terug naar Patrol
            if (enemy.CurrentAnimation.IsFinished)
            {
                enemy.SetState(new PatrolState());
            }
        }

        public void Draw(SpriteBatch spriteBatch, EnemyClass enemy)
        {
            enemy.DrawHelper(spriteBatch);
        }
    }
}
