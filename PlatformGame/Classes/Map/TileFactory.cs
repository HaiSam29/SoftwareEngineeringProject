using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Map;


namespace PlatformGame.Classes.Map
{
    // Zet tileType om naar row,col in de tileset, bouwt SourceRect en bepaalt of het een collidable tile is
    // SRP 1 verantwoordelijkheid: van tile-IDs naar Tile met juiste sprite en collision
    // OCP Makkelijk tiles toevoegen, bestaande code blijft bestaan
    // DIP Je gebruikt ITileFactory als abstractie, en TileMap hangt daarvan af i.p.v. van een concrete factory
    public class TileFactory: ITileFactory
    {
        private readonly int _tileSize;
        private readonly int _spacing;
        private readonly Dictionary<int, (int row, int col)> _tilePositions;
        private readonly HashSet<int> _collidableTiles;

        public TileFactory(int tileSize, int spacing = 1)
        {
            _tileSize = tileSize;
            _spacing = spacing;

            _tilePositions = new Dictionary<int, (int row, int col)>
            {
                { TileType.Empty, (0, 0) },
                { TileType.GrassLeft, (1, 1) },
                { TileType.GrassMiddel, (1, 2) },
                { TileType.GrassRight, (1, 3) },
                { TileType.Dirt, (6, 2) },
                { TileType.Stone, (2, 7) },
                { TileType.Plant, (6, 4) },
                { TileType.PlantHigh, (6, 5) },
                { TileType.MushRoom, (6, 8) },
                { TileType.ArrowLeft, (4, 7) },
                { TileType.ArrowRight, (4, 8) },
                { TileType.StoneLeft, (2, 8) },
                { TileType.StoneMiddel, (2, 9) },
                { TileType.StoneRight, (2, 10) },
                { TileType.DirtSnowMiddel, (5, 2) },
                { TileType.SnowPlant, (7, 4)},
                { TileType.SnowMan, (7, 5)},
                { TileType.SnowBmiddel, (4, 2) },
                { TileType.SnowBLeft, (4, 1) },
                { TileType.SnowBRight, (4, 3) },
                { TileType.SnowSingle, (4, 0) }
            };

            _collidableTiles = new HashSet<int>
            {
                TileType.GrassLeft,
                TileType.GrassMiddel,
                TileType.GrassRight,
                TileType.Dirt,
                TileType.Stone,
                TileType.StoneLeft,
                TileType.StoneMiddel,
                TileType.StoneRight,
                TileType.DirtSnowMiddel,
                TileType.SnowBmiddel,
                TileType.SnowBLeft,
                TileType.SnowBRight,
                TileType.SnowSingle

            };
        }

        // Onbekende tileType → fallback naar TileType.Empty
        // Berekent bronpositie in de tileset
        // Vraagt IsCollidable(tileType) om de collisionflag te zetten
        public Tile CreateTile(int tileType)
        {
            if (!_tilePositions.ContainsKey(tileType))
            {
                tileType = TileType.Empty;
            }

            var (row, col) = _tilePositions[tileType];
            var sourceRect = new Rectangle(
                col * (_tileSize + _spacing),
                row * (_tileSize + _spacing),
                _tileSize,
                _tileSize
            );

            return new Tile(tileType, sourceRect, IsCollidable(tileType));
        }

        public bool IsCollidable(int tileType)
        {
            return _collidableTiles.Contains(tileType);
        }
    }
}
