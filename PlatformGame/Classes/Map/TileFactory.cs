using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Enums;


namespace PlatformGame.Classes.Map
{
    public class TileFactory
    {
        private readonly int _tileSize;
        private readonly int _spacing;

        public TileFactory(int tileSize, int spacing = 1)
        {
            _tileSize = tileSize;
            _spacing = spacing;
        }

        public Tile CreateTile(TileType type)
        {
            (int row, int col) = GetTilePosition(type);

            var sourceRect = new Rectangle(
                col * (_tileSize + _spacing),
                row * (_tileSize + _spacing),
                _tileSize,
                _tileSize
            );

            return new Tile(type, sourceRect);
        }

        private (int row, int col) GetTilePosition(TileType type)
        {
            return type switch
            {
                
                TileType.GrassBlock => (0, 0),
                TileType.Dirt => (0, 4),
                _ => (0, 0)
            };
        }
    }
}
