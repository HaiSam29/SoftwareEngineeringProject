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
    // Beheert een lijst van alle enemies in de wereld.
    // Het roept Update() en Draw() aan op alle enemies, en heeft een simpele collision check om te detecteren of de speler een enemy raakt.
    // SRP Doet alleen collectie beheer. Weet niet hoe enemies bewegen of tekenen.
    // DIP Gebruikt List<IEnemy>, niet List<Enemy>. Hierdoor kun je later verschillende enemy types mixen
    // OCP Als je een nieuwe enemy type toevoegt, hoef je deze class niet aan te passen.
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

        // Loop door alle enemies en check of hun hitbox overlapt met de gegeven bounds.
        // Als er overlap is, geef de enemy terug. Anders null.
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
