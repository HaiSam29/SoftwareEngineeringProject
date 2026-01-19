using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Interfaces.Enemy
{
    // contract voor het berekenen van Rectangle Bounds hitbox op basis van position/size
    // SRP hitbox-tuning is een aparte reden om te veranderen
    // DIP Enemy hangt af van abstractie.
    public interface IBoundsProvider
    {
        Rectangle GetBounds(Vector2 position, int size);
    }
}
