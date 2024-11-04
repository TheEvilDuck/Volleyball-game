using UnityEngine;

namespace Gameplay
{
    public class Ball: MonoBehaviour, IBall
    {
        [SerializeField] Rigidbody2D _rigidBody;

        public void AddImpulse(Vector2 impulse)
        {
            _rigidBody.AddForce(impulse, ForceMode2D.Impulse);
        }

        public void ResetBall()
        {
            _rigidBody.velocity = Vector2.zero;
        }
    }
}
