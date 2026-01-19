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
    public class PatrolStrategy : IEnemyMovementStrategy
    {
        public void Move(IEnemyContext context, float deltaTime)
        {
            // 1. Check of we moeten omdraaien
            if (ShouldTurnAround(context))
            {
                context.Direction *= -1;
            }

            // 2. Beweeg
            context.Position = new Vector2(
                context.Position.X + context.Speed * context.Direction * deltaTime,
                context.Position.Y
            );
        }

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
