using Gameplay.StartPositions;
using UnityEngine;

namespace Gameplay.Characters
{
    public class Character : MonoBehaviour, IMovable
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private StartPosition _ballPivot;
        private float _moveDirection;
        private float _acceleration;
        private float _walkSpeed;

        public IStartPosition BallPivot => _ballPivot;

        public void Init(float acceleration, float walkSpeed)
        {
            _acceleration = acceleration;
            _walkSpeed = walkSpeed;
        }

        public void Move(float direction) => _moveDirection = direction;

        private void FixedUpdate() 
        {
            _rigidBody.velocity += new Vector2(_moveDirection * _acceleration, 0);
            _rigidBody.velocity = Vector2.ClampMagnitude(_rigidBody.velocity, _walkSpeed);
        }
    }
}
