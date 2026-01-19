using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Classes.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Map;
using PlatformGame.Enums;

namespace PlatformGame.Interfaces.Enemy
{
    // Wat states en strategies mogen gebruiken. Dit is een uitgebreide interface met directe toegang tot data (Position, Speed, Textures, ...)
    // ISP de interface is meer taakgericht en states blijven puur logica doen
    public interface IEnemyContext
    {
        // Data
        Vector2 Position { get; set; }
        Vector2 StartPosition { get; }
        int Direction { get; set; }
        float Speed { get; }
        float PatrolDistance { get; }
        int Size { get; }

        ITileCollisionProvider CollisionProvider { get; }
        int TileSize { get; }

        // Visuals
        Texture2D CurrentTexture { get; set; }
        Animation CurrentAnimation { get; set; }
        Texture2D WalkTex { get; }
        Texture2D AttackTex { get; }
        Animation WalkAnim { get; }
        Animation AttackAnim { get; }

        // Strategy
        IEnemyMovementStrategy MovementStrategy { get; }

        // Methods
        void TransitionTo(EnemyStateType stateType);
    }
}
