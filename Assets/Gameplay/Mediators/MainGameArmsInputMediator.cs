using System;
using Common.PlayerInput;
using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Mediators
{
    public class MainGameArmsInputMediator : IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly IArmsMover _armsMover;

        public MainGameArmsInputMediator(IPlayerInput playerInput, IArmsMover armsMover)
        {
            _playerInput = playerInput;
            _armsMover = armsMover;

            _playerInput.mousePressHold += OnMouseButtonHold;
            _playerInput.mouseRightPressHold += OnRightMouseButtonHold;
            _playerInput.mouseMove += OnMouseMove;
            _playerInput.mouseButtonUp += OnMouseButtonUp;
            _playerInput.mouseRightButtonUp += OnMouseButtonUp;

        }
        public void Dispose()
        {
            _armsMover.StopBending();
            _playerInput.mousePressHold -= OnMouseButtonHold;
            _playerInput.mouseRightPressHold -= OnRightMouseButtonHold;
            _playerInput.mouseMove -= OnMouseMove;
            _playerInput.mouseButtonUp -= OnMouseButtonUp;
            _playerInput.mouseRightButtonUp -= OnMouseButtonUp;
        }

        private void OnMouseButtonHold(Vector2 mousePosition)
        {
            _armsMover.StartBending(-1);
        }

        private void OnRightMouseButtonHold(Vector2 mousePosition)
        {
            _armsMover.StartBending(1);
        }

        private void OnMouseMove(Vector2 mousePosition)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            _armsMover.SetTargetPosition(new Vector2(worldPos.x, worldPos.y));
        }

        private void OnMouseButtonUp(Vector2 mousePosition) => _armsMover.StopBending();
    }
}
