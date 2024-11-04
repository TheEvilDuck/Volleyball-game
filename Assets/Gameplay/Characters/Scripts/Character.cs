using UnityEngine;

namespace Gameplay.Characters
{
    public class Character : MonoBehaviour, IMovable
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField, Min(0)] private float _acceleration = 1f;
        [SerializeField, Min(0)] private float _walkSpeed = 5f;
        private float _moveDirection;

        public void Move(float direction) => _moveDirection = direction;

        private void FixedUpdate() 
        {
            _rigidBody.velocity += new Vector2(_moveDirection * _acceleration, 0);
            _rigidBody.velocity = Vector2.ClampMagnitude(_rigidBody.velocity, _walkSpeed);
        }
    }
}
