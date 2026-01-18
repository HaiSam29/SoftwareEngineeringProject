using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Enemy
{
    public class Animation
    {
        private readonly List<Rectangle> _frames;
        private readonly float _frameTime;
        private int _currentFrame;
        private float _timer;

        // Om te weten of de animatie rond is (voor attacks)
        public bool IsFinished { get; private set; }

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
                _currentFrame++;

                if (_currentFrame >= _frames.Count)
                {
                    _currentFrame = 0; // Loop
                    IsFinished = true; // Markeer als klaar
                }
                else
                {
                    IsFinished = false;
                }
            }
        }

        // Resetten voor als een nieuwe state begint
        public void Reset()
        {
            _currentFrame = 0;
            _timer = 0;
            IsFinished = false;
        }
    }
}
