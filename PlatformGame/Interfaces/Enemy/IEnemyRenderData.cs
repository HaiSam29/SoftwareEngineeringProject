using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Classes.Enemy;

namespace PlatformGame.Interfaces.Enemy
{
    // read-only data view die de renderer nodig heeft (position, direction, size, texture, source rect)
    // ISP alleen render-gerelateerde properties
    // SRP doordat EnemyRenderer niets hoeft te weten over AI/states
    public interface IEnemyRenderData
    {
        Vector2 Position { get; }
        int Direction { get; }
        int Size { get; }
        Texture2D CurrentTexture { get; }
        Rectangle CurrentSourceRect { get; }
    }
}
