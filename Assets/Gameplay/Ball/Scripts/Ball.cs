using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Balls
{
    public class Ball: MonoBehaviour, IBall
    {
        [SerializeField] Rigidbody2D _rigidBody;
        [SerializeField] private Collider2D _collider;

        public void AddImpulse(Vector2 impulse)
        {
            _rigidBody.AddForce(impulse, ForceMode2D.Impulse);
        }

        public void Freeze()
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.isKinematic = true;
            _collider.enabled = false;
        }

        public void Release()
        {
            _rigidBody.isKinematic = false;
            _collider.enabled = true;
        }

        public void SetPosition(IPositionProvider position)
        {
            transform.position = position.Position;
            transform.rotation = position.Rotation;
        }
    }
}
