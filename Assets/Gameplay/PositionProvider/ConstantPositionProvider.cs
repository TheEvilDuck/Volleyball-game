using UnityEngine;

namespace Gameplay.PositionProviding
{
    public struct ConstantPositionProvider : IPositionProvider
    {
        public Vector2 Position {get; private set;}
        public Quaternion Rotation {get; private set;}

        public ConstantPositionProvider(Vector2 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
