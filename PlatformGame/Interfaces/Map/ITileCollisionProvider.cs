using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Map
{
    public interface ITileCollisionProvider
    {
        bool HasCollision(int x, int y);
        int GetTileType(int x, int y);
    }
}
