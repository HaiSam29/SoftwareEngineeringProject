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
    // De lopende state van een enemy.
    // Hij zet de walk-animatie, en delegeert alle bewegingslogica naar de MovementStrategy.
    // SRP Doet alleen state-specifiek gedrag
    // Als je later een FlyingPatrolState wilt, maak je een nieuwe class zonder deze aan te passen
    // Strategy Pattern: De state weet niet hoe bewegen werkt; hij roept alleen context.MovementStrategy.Move(...) aan.
    public class PatrolState : IEnemyState
    {
        public void Enter(IEnemyContext context)
        {
            context.CurrentAnimation = context.WalkAnim;
            context.CurrentTexture = context.WalkTex;
            context.CurrentAnimation.Reset();
        }

        public void Update(GameTime gameTime, IEnemyContext context)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            context.MovementStrategy.Move(context, dt);
            context.CurrentAnimation.Update(dt);
        }

        public void Draw(SpriteBatch spriteBatch, IEnemyContext context)
        {
            // States doen niet langer rendering
        }
    }
}
