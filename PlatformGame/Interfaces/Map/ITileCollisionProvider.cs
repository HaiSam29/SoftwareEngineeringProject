using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Map
{
    // Biedt collision-informatie op tile-niveau
    // ISP Alleen collision, geen rendering of andere functies
    // DIP Collision consumers (Character, Enemy) hangen af van deze abstractie niet van TileMap
    public interface ITileCollisionProvider
    {
        bool HasCollision(int x, int y);
        int GetTileType(int x, int y);
    }
}
