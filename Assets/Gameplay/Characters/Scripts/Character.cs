using Gameplay.StartPositions;
using UnityEngine;

namespace Gameplay.Characters
{
    public class Character : MonoBehaviour, IMovable
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private StartPosition _ballPivot;
        private ICharacterStats _stats;
        private float _moveDirection;

        public IStartPosition BallPivot => _ballPivot;

        public void Init(ICharacterStats stats)
        {
            _stats = stats;
        }

        public void Move(float direction) => _moveDirection = direction;

        private void FixedUpdate() 
        {
            _rigidBody.velocity += new Vector2(_moveDirection * _stats.Acceleration, 0);
            _rigidBody.velocity = Vector2.ClampMagnitude(_rigidBody.velocity, _stats.WalkSpeed);
        }
    }
}
