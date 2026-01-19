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
    // Verantwoordelijk voor: Achtergrond maken, Tileset inladen,TileFactory en TileMap maken, Level + renderer + collision provider bundelen
    // SRP Levelscene opbouwen is hier de verantwoordelijkheid
    // DIP LevelFactory krijgt IGameConfig via DI
    public class LevelFactory
    {
        private readonly ContentManager _content;
        private readonly ILevelLoader _levelLoader;
        private readonly IGameConfig _config; // Opslag voor de config

        // Constructor accepteert IGameConfig (Dependency Injection)
        public LevelFactory(ContentManager content, IGameConfig config)
        {
            _content = content;
            _config = config;
            _levelLoader = new LevelLoader();
        }

        public Background CreateBackground()
        {
            var bgTexture = _content.Load<Texture2D>("background");

            return new Background(bgTexture, _config.ScreenWidth, _config.ScreenHeight);
        }

        public (LevelData level, ITileMapRenderer renderer, ITileCollisionProvider collision) CreateLevel(string levelName)
        {
            // Data ophalen
            LevelData levelData = _levelLoader.LoadLevel(levelName);

            // Texture laden
            var tileset = _content.Load<Texture2D>("tilemap");

            // Instellingen
            int sourceTileSize = 18;
            int spacing = 1;

            int destTileSize = 60;

            // Maak de factory en map aan
            ITileFactory tileFactory = new TileFactory(sourceTileSize, spacing);
            var tileMap = new TileMap(levelData.MapData, tileset, destTileSize, tileFactory);

            return (levelData, tileMap, tileMap);
        }
    }
}
