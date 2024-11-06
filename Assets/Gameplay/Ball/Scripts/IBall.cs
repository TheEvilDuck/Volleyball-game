
using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Balls
{
    public interface IBall: ISetablePosition
    {
        public void AddImpulse(Vector2 impulse);
        public void Freeze();
        public void Release();
    }
}
