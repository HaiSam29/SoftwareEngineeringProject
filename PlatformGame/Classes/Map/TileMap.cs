using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Map;

namespace PlatformGame.Classes.Map
{
    // TileMap bouwt een 2D array van Tile op basis van mapData en ITileFactory en tekent deze in Draw(...)
    // OCP Draw en HasCollision zijn netjes gescheiden via interfaces
    // dus code die alleen collision nodig heeft, ziet de draw-logica niet
    // DIP PlayingState en CollisionSystem praten tegen interfaces in plaats van concrete TileMap
    public class TileMap: ITileMapRenderer, ITileCollisionProvider
    {
        private readonly Tile[,] _tiles;
        private readonly Texture2D _tileset;
        private readonly int _tileSize;

        public int Width => _tiles.GetLength(1);
        public int Height => _tiles.GetLength(0);
        public int TileSize => _tileSize;

        // leest mapData en maakt voor elke cel een Tile via ITileFactory.
        // Dit houdt de mapping van ints naar visuals/collision centraal in één plek
        public TileMap(int[,] mapData, Texture2D tileset, int tileSize, ITileFactory factory)
        {
            _tileset = tileset;
            _tileSize = tileSize;

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

        // Itereert over alle tiles, slaat Empty over, en tekent rechtstreeks op basis van x * tileSize en y * tileSize plus offset
        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var tile = _tiles[y, x];

                    if (tile.Type == TileType.Empty)
                        continue;

                    var destRect = new Rectangle(
                        (int)(x * _tileSize + offset.X),
                        (int)(y * _tileSize + offset.Y),
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

            return _tiles[y, x].IsCollidable;
        }

        public int GetTileType(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return TileType.Empty;

            return _tiles[y, x].Type;
        }
    }
}
