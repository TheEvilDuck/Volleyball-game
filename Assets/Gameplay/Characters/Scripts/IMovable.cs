using UnityEngine;

namespace Gameplay.Characters
{
    public interface IMovable
    {
        public void Move(float direction);
        public void Stop();
    }
}
