using UnityEngine;

namespace Gameplay.StartPositions
{
    public class StartPosition : MonoBehaviour, IStartPosition
    {
        public Vector2 Position => transform.position;

        public Quaternion Rotation => transform.rotation;

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position, Vector3.one * 0.5f);
        }
    }
}
