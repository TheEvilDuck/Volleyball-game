using UnityEngine;

namespace Gameplay.Characters
{
    public interface IArmsMover
    {
        public float CurrentAngularSpeed {get;}
        public void SetTargetPosition(Vector2 targetPosition);
        public void StartBending(float direction);
        public void StopBending();
    }
}
