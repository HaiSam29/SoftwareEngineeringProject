using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Interfaces.Enemy
{
    // 1 contract om een enemy te tekenen op basis van data + cameraOffset
    // SRP/ISP rendering als aparte verantwoordelijkheid en kleine interface wordt aangeboden
    // DIP code afhangt van een abstractie i.p.v. een concrete renderer
    public interface IEnemyRenderer
    {
        void Draw(SpriteBatch spriteBatch, IEnemyRenderData data, Vector2 cameraOffset);
    }
}
