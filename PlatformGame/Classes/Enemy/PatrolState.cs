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
    public class PatrolState : IEnemyState
    {
        public void Enter(EnemyClass enemy)
        {
            enemy.CurrentAnimation = enemy.WalkAnim;
            enemy.CurrentTexture = enemy.WalkTex;
            enemy.CurrentAnimation.Reset();
        }

        public void Update(GameTime gameTime, EnemyClass enemy)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (enemy.ShouldTurnAround())
            {
                enemy.Direction *= -1;
            }

            enemy.Position.X += enemy.Speed * enemy.Direction * dt;
            enemy.CurrentAnimation.Update(dt);
        }

        public void Draw(SpriteBatch spriteBatch, EnemyClass enemy)
        {
            enemy.DrawHelper(spriteBatch);
        }
    }
}
