using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Classes.Game;
using PlatformGame.Classes.Map;
using PlatformGame.Interfaces.Game;
using PlatformGame.Interfaces.Ilevel;
using PlatformGame.Interfaces.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelData = PlatformGame.Classes.Level.Level;
using LevelLoader = PlatformGame.Classes.Level.HardcodedLevelLoader;

namespace PlatformGame.Classes.Level
{
    public class LevelFactory
    {
        private readonly ContentManager _content;
        private readonly ILevelLoader _levelLoader;
        private readonly IGameConfig _config; // 2. Opslag voor de config

        // 3. Constructor accepteert nu IGameConfig (Dependency Injection)
        public LevelFactory(ContentManager content, IGameConfig config)
        {
            _content = content;
            _config = config;
            _levelLoader = new LevelLoader();
        }

        public Background CreateBackground()
        {
            var bgTexture = _content.Load<Texture2D>("background");

            // 4. Gebruik nu _config i.p.v. GameConfig.static
            return new Background(bgTexture, _config.ScreenWidth, _config.ScreenHeight);
        }

        public (LevelData level, ITileMapRenderer renderer, ITileCollisionProvider collision) CreateLevel(string levelName)
        {
            // 1. Data ophalen
            LevelData levelData = _levelLoader.LoadLevel(levelName);

            // 2. Texture laden
            var tileset = _content.Load<Texture2D>("tilemap");

            // 3. Instellingen
            int sourceTileSize = 18;
            int spacing = 1;

            // 5. Optioneel: Je zou destTileSize ook uit de config kunnen halen als je dat wilt
            // Bijv: int destTileSize = _config.TileSize; 
            // Voor nu laten we het op 60 staan zoals in je voorbeeld:
            int destTileSize = 60;

            // 4. Maak de factory en map aan
            ITileFactory tileFactory = new TileFactory(sourceTileSize, spacing);
            var tileMap = new TileMap(levelData.MapData, tileset, destTileSize, tileFactory);

            return (levelData, tileMap, tileMap);
        }
    }
}
