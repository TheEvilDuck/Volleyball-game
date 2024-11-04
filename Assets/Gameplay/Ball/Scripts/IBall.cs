
using UnityEngine;

namespace Gameplay
{
    public interface IBall
    {
        public void ResetBall();
        public void AddImpulse(Vector2 impulse);
    }
}
