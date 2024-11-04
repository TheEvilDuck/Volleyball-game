using UnityEngine;

namespace Gameplay
{
    public class Ball: MonoBehaviour, IBall
    {
        [SerializeField] Rigidbody2D _rigidBody;

        public void ResetBall()
        {
            _rigidBody.velocity = Vector2.zero;
        }
    }
}
