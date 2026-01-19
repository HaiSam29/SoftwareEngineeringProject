using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnemyClass = PlatformGame.Classes.Enemy.Enemy;

namespace PlatformGame.Interfaces.Enemy
{
    public interface IEnemyState
    {
        void Enter(IEnemyContext context);
        void Update(GameTime gameTime, IEnemyContext context);
        void Draw(SpriteBatch spriteBatch, IEnemyContext context);
    }
}
