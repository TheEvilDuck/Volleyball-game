using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Characters
{
    public class Character : MonoBehaviour, IMovable, IArmsMover, ISetablePosition
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private Rigidbody2D _upperArmRigidBody;
        [SerializeField] private Rigidbody2D _lowerArmRigidBody;
        [SerializeField] private TransformPositionProvider _ballPivot;
        [SerializeField] private Transform _armPivot;
        [SerializeField] private Transform _elbowPivot;
        private ICharacterStats _stats;
        private float _moveDirection;
        private Vector2 _currentArmsTarget;
        private float _bending = 0;

        public IPositionProvider BallPivot => _ballPivot;

        public float CurrentAngularSpeed {get; private set;}

        public void Init(ICharacterStats stats)
        {
            _stats = stats;
        }

        public void Move(float direction) => _moveDirection = direction;
        public void Stop() => _moveDirection = 0;

        public void SetTargetPosition(Vector2 targetPosition) => _currentArmsTarget = targetPosition;

        public void StartBending(float direction) => _bending = direction;

        public void StopBending() => _bending = 0;

        private void FixedUpdate() 
        {
            _rigidBody.velocity += new Vector2(_moveDirection * _stats.Acceleration, 0);
            _rigidBody.velocity = Vector2.ClampMagnitude(_rigidBody.velocity, _stats.WalkSpeed);

            RotateArm();
            BendArm();
        }

        private void BendArm()
        {
            
        }

        private void RotateArm()
        {
            Vector2 position = new Vector2(_armPivot.position.x, _armPivot.position.y);
            Vector2 direction = (position - _currentArmsTarget).normalized;
            float angle = Vector2.SignedAngle(_armPivot.up, direction);
            float signSpeed = angle * Time.deltaTime * _stats.ArmsSpeed;
            CurrentAngularSpeed = Mathf.Abs(signSpeed);
            _upperArmRigidBody.MoveRotation(signSpeed);
        }

        public void SetPosition(IPositionProvider positionProvider)
        {
            transform.position = positionProvider.Position;
            transform.rotation = positionProvider.Rotation;
            _rigidBody.velocity = Vector2.zero;
        }
    }
}
