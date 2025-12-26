using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Map;


namespace PlatformGame.Classes.Map
{
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
                { TileType.GrassBlock, (0, 0) },
                { TileType.Dirt, (0, 4) },
                { TileType.Stone, (2, 7) },
                { TileType.Plant, (6, 4) }
            };

            _collidableTiles = new HashSet<int>
            {
                TileType.GrassBlock,
                TileType.Dirt,
                TileType.Stone
            };
        }

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
