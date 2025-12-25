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
        private readonly Dictionary<string, Func<Level>> _levelFactories;

        public HardcodedLevelLoader()
        {
            _levelFactories = new Dictionary<string, Func<Level>>
            {
                { "Level1", CreateLevel1 }
            };
        }

        public Level LoadLevel(string levelName)
        {
            if (!_levelFactories.ContainsKey(levelName))
            {
                throw new ArgumentException($"Level '{levelName}' niet gevonden");
            }

            return _levelFactories[levelName]();
        }

        private Level CreateLevel1()
        {
            int[,] mapData = new int[,]
            {
                // Rij 0-4: Lucht (0 = Empty)
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                
                // Rij 5-6: Lucht
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                
                // Rij 7: Hoge platforms (5 = Stone)
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, 0 },
                
                // Rij 8: Mid platforms
                { 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0 },
                
                // Rij 9: Lage platforms
                { 0, 0, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                
                // Rij 10: Lucht
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                
                // Rij 11-13: Grond (3 = GrassBlock, 4 = Dirt)
                { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
                { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }
            };

            return new Level("Level1", mapData);
        }
    }
}
