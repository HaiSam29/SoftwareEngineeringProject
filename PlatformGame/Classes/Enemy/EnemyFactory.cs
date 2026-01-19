using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Interfaces.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Enums;
using EnemyClass = PlatformGame.Classes.Enemy.Enemy;
using PlatformGame.Interfaces.Game;
using PlatformGame.Interfaces.Enemy;

namespace PlatformGame.Classes.Enemy
{
    // Maakt enemies aan op basis van een EnemyType (Normal, Fast, Strong).
    // Het laadt de juiste textures, maakt animaties.
    // kiest stats (speed/patrol range), en injecteert alle dependencies.
    // SRP Doet alleen "creatie" van enemies
    // OCP Als je een nieuw enemy type wilt, voeg je een case toe aan de switch en laad je nieuwe textures.
    // Je hoeft de Enemy class zelf niet aan te passen.
    // DIP De factory geen concrete renderer/bounds hoeft hard te coderen
    public class EnemyFactory
    {
        private ContentManager _content;
        private IGameConfig _config;
        private IEnemyStateFactory _stateFactory;
        private IEnemyRenderer _renderer;              
        private IBoundsProvider _boundsProvider;        

        private Dictionary<EnemyType, Texture2D> _walkTextures = new();
        private Dictionary<EnemyType, Texture2D> _attackTextures = new();

        // Constructor accepteert renderer en boundsProvider
        public EnemyFactory(
            ContentManager content,
            IGameConfig config,
            IEnemyStateFactory stateFactory,
            IEnemyRenderer renderer,
            IBoundsProvider boundsProvider)
        {
            _content = content;
            _config = config;
            _stateFactory = stateFactory ?? throw new ArgumentNullException(nameof(stateFactory));
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _boundsProvider = boundsProvider ?? throw new ArgumentNullException(nameof(boundsProvider));
            LoadTextures();
        }

        private void LoadTextures()
        {
            _walkTextures[EnemyType.Normal] = _content.Load<Texture2D>("Enemy1Walk");
            _attackTextures[EnemyType.Normal] = _content.Load<Texture2D>("Enemy1Attack");

            _walkTextures[EnemyType.Fast] = _content.Load<Texture2D>("Enemy2Walk");
            _attackTextures[EnemyType.Fast] = _content.Load<Texture2D>("Enemy2Attack");

            _walkTextures[EnemyType.Strong] = _content.Load<Texture2D>("Enemy3Walk");
            _attackTextures[EnemyType.Strong] = _content.Load<Texture2D>("Enemy3Attack");
        }

        // Kiest stats op basis van EnemyType, bouwt animaties frames, kiest een movement strategy, en injecteert stateFactory + renderer + boundsProvider in de Enemy constructor.
        public EnemyClass CreateEnemy(EnemyType type, Vector2 position, ITileCollisionProvider collision, int tileSize)
        {
            float speed = 70f;
            float patrolRange = 200f;
            int size = 72;

            switch (type)
            {
                case EnemyType.Normal:
                    speed = 70f; patrolRange = 200f;
                    break;
                case EnemyType.Fast:
                    speed = 120f; patrolRange = 300f;
                    break;
                case EnemyType.Strong:
                    speed = 40f; patrolRange = 100f;
                    break;
            }

            var walkAnim = new Animation(CreateFrames(96, 96, 7), 0.15f);
            var attackAnim = new Animation(CreateFrames(96, 96, 4), 0.15f);
            IEnemyMovementStrategy strategy = new PatrolStrategy();

            return new EnemyClass(
                position,
                _walkTextures[type],
                _attackTextures[type],
                walkAnim,
                attackAnim,
                speed,
                patrolRange,
                collision,
                tileSize,
                size,
                _stateFactory,
                strategy,
                _renderer,          
                _boundsProvider     
            );
        }

        private List<Rectangle> CreateFrames(int w, int h, int count)
        {
            var list = new List<Rectangle>();
            for (int i = 0; i < count; i++) list.Add(new Rectangle(i * w, 0, w, h));
            return list;
        }
    }
}
