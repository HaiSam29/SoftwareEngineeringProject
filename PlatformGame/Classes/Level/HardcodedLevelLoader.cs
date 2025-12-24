using PlatformGame.Enums;
using PlatformGame.Interfaces.Ilevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Level
{
    public class HardcodedLevelLoader: ILevelLoader
    {
        public Level LoadLevel(string levelName)
        {
            return levelName switch
            {
                "Level1" => CreateLevel1(),
                _ => throw new ArgumentException($"Level '{levelName}' niet gevonden")
            };
        }

        private Level CreateLevel1()
        {
            var mapData = new TileType[,]
            {
                // Rij 0-4: Lucht
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                
                // Rij 5-6: Lucht
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                
                // Rij 7: Hoge platforms
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Stone, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Stone, TileType.Stone, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                
                // Rij 8: Mid platforms
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Stone, TileType.Stone, TileType.Stone, TileType.Empty, TileType.Empty, TileType.Stone, TileType.Stone, TileType.Stone, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                
                // Rij 9: Lage platforms
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Stone, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                
                // Rij 10: Lucht
                { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                
                // Rij 11-13: Grond
                { TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock, TileType.GrassBlock },
                { TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt},
                { TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt }
            };

            var collision = new bool[,]
            {
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, true,  true,  false, false, false, false, false, false, false, false, false, false, false, false, true,  true,  false, false, false, false },
                { false, false, false, false, false, false, false, false, true,  true,  true,  false, false, true,  true,  true,  false, false, false, false, false, false, false, false },
                { false, false, true,  true,  false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true,  true,  false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true  },
                { true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true  },
                { true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true  }
            };

            return new Level("Level1", mapData, collision);
        }
    }
}
