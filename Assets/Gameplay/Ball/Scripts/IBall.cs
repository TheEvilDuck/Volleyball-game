
using Gameplay.StartPositions;
using UnityEngine;

namespace Gameplay.Balls
{
    public interface IBall
    {
        public void AddImpulse(Vector2 impulse);
        public void SetPosition(IStartPosition position);
        public void Freeze();
        public void Release();
    }
}
