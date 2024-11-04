using System;
using UnityEngine;

namespace Gameplay.BallDetection
{
    [RequireComponent(typeof(Collider2D))]
    public class BallCollider : MonoBehaviour, IBallDetector
    {
        [SerializeField] private LayerMask _ballMask;
        public event Action detectionStarted;
        public event Action detectionEnd;

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (!other.collider.TryGetComponent<IBall>(out IBall ball))
                return;

            detectionStarted?.Invoke();
        }

        private void OnCollisionExit2D(Collision2D other) 
        {
            if (!other.collider.TryGetComponent<IBall>(out IBall ball))
                return;

            detectionEnd?.Invoke();
        }
    }
}
