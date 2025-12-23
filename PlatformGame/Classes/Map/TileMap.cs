using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Map
{
    public class TileMap
    {
        private Tile[,] _tiles;
        private Texture2D _tileset;
        private int _tileSize;
        private bool[,] _collision;

        public int Width => _tiles.GetLength(1);
        public int Height => _tiles.GetLength(0);
        public int TileSize => _tileSize;

        public TileMap(TileType[,] mapData, Texture2D tileset, int tileSize, TileFactory factory, bool[,] collision)
        {
            _tileset = tileset;
            _tileSize = tileSize;
            _collision = collision;

            int rows = mapData.GetLength(0);
            int cols = mapData.GetLength(1);
            _tiles = new Tile[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    _tiles[y, x] = factory.CreateTile(mapData[y, x]);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)  // Voeg offset parameter toe
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var tile = _tiles[y, x];

                    if (tile.Type == TileType.Empty)
                        continue;

                    var destRect = new Rectangle(
                        (int)(x * _tileSize + offset.X),  // Voeg offset.X toe
                        (int)(y * _tileSize + offset.Y),  // Voeg offset.Y toe
                        _tileSize,
                        _tileSize
                    );

                    spriteBatch.Draw(_tileset, destRect, tile.SourceRect, Color.White);
                }
            }
        }

        public bool HasCollision(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return false;

            return _collision[y, x];
        }
    }
}
