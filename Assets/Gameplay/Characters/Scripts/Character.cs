using Common;
using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Characters
{
    public class Character : MonoBehaviour, IMovable, IArmsMover, ISetablePosition, IResatable
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private TransformPositionProvider _ballPivot;
        [SerializeField] private Transform _armPivot;
        [SerializeField] private Transform _elbowPivot;
        [SerializeField] private HingeJoint2D _upperArmJoint;
        [SerializeField] private HingeJoint2D _elbowJoint;
        private ICharacterStats _stats;
        private float _moveDirection;
        private Vector2 _currentArmsTarget;
        private float _bending = 0;
        private float _scale;

        public IPositionProvider BallPivot => _ballPivot;

        public float CurrentAngularSpeed {get; private set;}

        public void Init(ICharacterStats stats, float scale = 1)
        {
            _stats = stats;
            _scale = scale;
            JointAngleLimits2D jointAngleLimits2D = new JointAngleLimits2D();
            jointAngleLimits2D.min = 0;
            jointAngleLimits2D.max = _stats.MaxBendAngle;
            jointAngleLimits2D.max *= scale;
            jointAngleLimits2D.min *= scale;
            _elbowJoint.limits = jointAngleLimits2D;
        }

        public void Move(float direction) => _moveDirection = direction;
        public void Stop() => _moveDirection = 0;

        public void SetTargetPosition(Vector2 targetPosition) => _currentArmsTarget = targetPosition;

        public void StartBending(float direction) => _bending = direction;

        public void StopBending() => _bending = 0;

        public void SetPosition(IPositionProvider positionProvider)
        {
            transform.position = positionProvider.Position;
            transform.rotation = positionProvider.Rotation;
        
            ResetInnerState();
        }

        public void ResetInnerState()
        {
            _rigidBody.velocity = Vector2.zero;
            _rigidBody.angularVelocity = 0;

            JointMotor2D motor = _upperArmJoint.motor;
            motor.motorSpeed = 0;
            _upperArmJoint.motor = motor;

            motor = _elbowJoint.motor;
            motor.motorSpeed = 0;
            _elbowJoint.motor = motor;

            _upperArmJoint.connectedBody.velocity = Vector2.zero;
            _elbowJoint.connectedBody.velocity = Vector2.zero;
        }

        private void FixedUpdate() 
        {
            _rigidBody.velocity += new Vector2(_moveDirection * _stats.Acceleration, 0);
            _rigidBody.velocity = Vector2.ClampMagnitude(_rigidBody.velocity, _stats.WalkSpeed);

            RotateArm();
            BendArm();
        }

        private void BendArm()
        {
            float angle = _bending * _stats.BendingSpeed * _scale;
            JointMotor2D motor = _elbowJoint.motor;
            motor.motorSpeed = angle;
            _elbowJoint.motor = motor;
        }

        private void RotateArm()
        {
            Vector2 position = new Vector2(_upperArmJoint.transform.position.x, _upperArmJoint.transform.position.y);
            Vector2 direction = (position - _currentArmsTarget).normalized;
            float angle = Vector2.SignedAngle(_upperArmJoint.transform.up, direction);
            float signSpeed = angle * _stats.ArmsSpeed;
            CurrentAngularSpeed = -signSpeed;

            JointMotor2D motor = _upperArmJoint.motor;

            if (Mathf.Abs(angle) <= _stats.ArmsMinAngle)
                CurrentAngularSpeed = -_upperArmJoint.jointSpeed;

            motor.motorSpeed = CurrentAngularSpeed;
            _upperArmJoint.motor = motor;
        }

        
    }
}
