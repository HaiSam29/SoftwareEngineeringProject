using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using PlatformGame.Interfaces.Enemy;

namespace PlatformGame.Classes.Enemy
{
    // implementeert de standaard hitbox-marges sideMargin, topMargin en bouwt daarmee een kleinere bounds-rectangle
    // SRP alleen bounds
    // OCP andere enemy types kunnen een andere provider krijgen zonder Enemy aan te passen
    public class DefaultBoundsProvider : IBoundsProvider
    {
        private readonly int _sideMargin;
        private readonly int _topMargin;

        public DefaultBoundsProvider(int sideMargin = 25, int topMargin = 20)
        {
            _sideMargin = sideMargin;
            _topMargin = topMargin;
        }

        // Maakt een kleinere rectangle door margins af te trekken zodat collision niet te groot voelt door transparante pixels in sprites
        public Rectangle GetBounds(Vector2 position, int size)
        {
            return new Rectangle(
                (int)position.X + _sideMargin,
                (int)position.Y + _topMargin,
                size - (_sideMargin * 2),
                size - _topMargin
            );
        }
    }
}
