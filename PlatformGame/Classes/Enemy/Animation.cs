using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Enemy
{
    public class Animation
    {
        private readonly List<Rectangle> _frames;
        private readonly float _frameTime;
        private int _currentFrame;
        private float _timer;

        public Rectangle CurrentFrame => _frames[_currentFrame];

        public Animation(List<Rectangle> frames, float frameTime)
        {
            _frames = frames;
            _frameTime = frameTime;
        }

        public void Update(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer >= _frameTime)
            {
                _timer = 0;
                _currentFrame = (_currentFrame + 1) % _frames.Count;
            }
        }
    }
}
