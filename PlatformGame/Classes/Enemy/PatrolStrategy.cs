using PlatformGame.Interfaces.Character;
using PlatformGame.Interfaces.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Enemy
{
    // Bepaalt de beweging van een patroullerende enemy.
    // Het beweegt de enemy in één richting, en draait om als: De patrol range bereikt is, Er een muur voor is, Er een afgrond voor is
    // SRP Doet alleen bewegingslogica. Weet niets van animaties of states.
    // OCP Als je later een ChaseStrategy of FlyStrategy wilt, maak je een nieuwe class zonder deze aan te passen.
    // Strategy Pattern: De state roept strategy.Move(...) aan zonder te weten wat voor beweging het is.
    public class PatrolStrategy : IEnemyMovementStrategy
    {
        public void Move(IEnemyContext context, float deltaTime)
        {
            // Check of we moeten omdraaien
            if (ShouldTurnAround(context))
            {
                context.Direction *= -1;
            }

            // Beweeg
            context.Position = new Vector2(
                context.Position.X + context.Speed * context.Direction * deltaTime,
                context.Position.Y
            );
        }

        // Check patrol range
        // Voorspel de volgende positie
        // Check grond en muur
        // Besluit
        private bool ShouldTurnAround(IEnemyContext context)
        {
            float distance = context.Position.X - context.StartPosition.X;

            // Check Patrol Range
            if (distance >= context.PatrolDistance || distance <= -context.PatrolDistance)
                return true;

            // Check Map Collision (Muur of Afgrond)
            int nextX = context.Direction > 0
                ? (int)(context.Position.X + context.Size + 5) / context.TileSize
                : (int)(context.Position.X - 5) / context.TileSize;

            int groundY = (int)(context.Position.Y + context.Size + 2) / context.TileSize;
            int wallY = (int)(context.Position.Y + context.Size / 2) / context.TileSize;

            bool hasGround = context.CollisionProvider.HasCollision(nextX, groundY);
            bool hasWall = context.CollisionProvider.HasCollision(nextX, wallY);

            return !hasGround || hasWall;
        }
    }
}
