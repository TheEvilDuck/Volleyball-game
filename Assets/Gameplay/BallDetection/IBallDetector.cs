using System;

namespace Gameplay.BallDetection
{
    public interface IBallDetector
    {
        public event Action detectionStarted;
        public event Action detectionEnd;
    }
}
