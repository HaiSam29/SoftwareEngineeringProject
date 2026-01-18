using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformGame.Interfaces.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Enemy
{
    public class EnemyManager
    {
        private readonly List<IEnemy> _enemies = new List<IEnemy>();
        public int EnemyCount => _enemies.Count;

        public void AddEnemy(IEnemy enemy) => _enemies.Add(enemy);

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in _enemies)
                enemy.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            foreach (var enemy in _enemies)
                enemy.Draw(spriteBatch, cameraOffset);
        }

        public IEnemy CheckCollision(Rectangle bounds)
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.Bounds.Intersects(bounds))
                    return enemy;
            }
            return null;
        }

        public void RemoveEnemy(IEnemy enemy)
        {
            // Verwijder de vijand uit de interne lijst
            _enemies.Remove(enemy);
        }
    }
}
