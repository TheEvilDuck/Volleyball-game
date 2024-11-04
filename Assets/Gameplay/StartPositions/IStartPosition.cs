using UnityEngine;

namespace Gameplay.StartPositions
{
    public interface IStartPosition
    {
        public Vector2 Position {get;}
        public Quaternion Rotation {get;}
    }
}
