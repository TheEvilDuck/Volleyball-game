using System;
using Common.PlayerInput;
using UnityEngine;

namespace Gameplay.Characters
{
    public class PlayerInputCharacterController : ICharacterController, IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private bool _canBending = false;
        private bool _canMove = false;
        private bool _canRotateArm = false;
        private IMovable _currentMovable;
        private IArmsMover _currentArmsMover;

        public PlayerInputCharacterController(IPlayerInput playerInput) => _playerInput = playerInput;

        public void Attach(IMovable movable, IArmsMover armsMover)
        {
            DetachCurrent();

            _currentMovable = movable;
            _currentArmsMover = armsMover;

            _playerInput.horizontalInput += OnHorizontalInput;
            _playerInput.mousePressHold += OnMouseButtonHold;
            _playerInput.mouseRightPressHold += OnRightMouseButtonHold;
            _playerInput.mouseMove += OnMouseMove;
            _playerInput.mouseButtonUp += OnMouseButtonUp;
            _playerInput.mouseRightButtonUp += OnMouseButtonUp;
        }

        public void DetachCurrent()
        {
            if (_currentMovable != null)
            {
                _playerInput.horizontalInput -= OnHorizontalInput;
            }

            if (_currentArmsMover != null)
            {
                _playerInput.mousePressHold -= OnMouseButtonHold;
                _playerInput.mouseRightPressHold -= OnRightMouseButtonHold;
                _playerInput.mouseMove -= OnMouseMove;
                _playerInput.mouseButtonUp -= OnMouseButtonUp;
                _playerInput.mouseRightButtonUp -= OnMouseButtonUp;
            }
        }

        public void Dispose() => DetachCurrent();

        public void SetCanBending(bool enabled)
        {
            _canBending = enabled;

            if (!_canBending)
                _currentArmsMover?.StopBending();
        }
        public void SetCanMove(bool enabled)
        {
            _canMove = enabled;

            if (!_canMove)
                _currentMovable.Stop();
        }
        public void SetCanRotateArm(bool enabled) => _canRotateArm = enabled;

        private void OnHorizontalInput(float direction)
        {
            if (!_canMove)
                return;

            _currentMovable?.Move(direction);
        }
        private void OnMouseButtonHold(Vector2 mousePosition)
        {
            if (!_canBending)
                return;

            _currentArmsMover.StartBending(-1);
        }

        private void OnRightMouseButtonHold(Vector2 mousePosition)
        {
            if (!_canBending)
                return;

            _currentArmsMover.StartBending(1);
        }

        private void OnMouseMove(Vector2 mousePosition)
        {
            if (!_canRotateArm)
                return;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            _currentArmsMover.SetTargetPosition(new Vector2(worldPos.x, worldPos.y));
        }

        private void OnMouseButtonUp(Vector2 mousePosition) => _currentArmsMover.StopBending();
    }
}
