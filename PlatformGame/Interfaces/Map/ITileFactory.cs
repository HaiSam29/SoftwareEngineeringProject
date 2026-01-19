using PlatformGame.Classes.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Map
{
    // SRP logica in één plek, en geeft TileMap een nette abstractie
    public interface ITileFactory
    {
        Tile CreateTile(int tileType);
        bool IsCollidable(int tileType);
    }
}
