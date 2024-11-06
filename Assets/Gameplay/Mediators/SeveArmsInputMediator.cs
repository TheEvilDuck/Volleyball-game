using System;
using Common.PlayerInput;
using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Mediators
{
    public class SeveArmsInputMediator : IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly IArmsMover _armsMover;

        public SeveArmsInputMediator(IPlayerInput playerInput, IArmsMover armsMover)
        {
            _playerInput = playerInput;
            _armsMover = armsMover;

            _playerInput.mouseMove += OnMousePositionChanged;
        }
        public void Dispose()
        {
            _playerInput.mouseMove -= OnMousePositionChanged;
        }

        private void OnMousePositionChanged(Vector2 position) => _armsMover.SetTargetPosition(Camera.main.ScreenToWorldPoint(position));
    }
}
