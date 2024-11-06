using UnityEngine;

namespace Gameplay.PositionProviding
{
    public interface IPositionProvider
    {
        public Vector2 Position {get;}
        public Quaternion Rotation {get;}
    }
}
