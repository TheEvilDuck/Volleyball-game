using System;
using Common.PlayerInput;
using Gameplay.Characters;

namespace Gameplay.Mediators
{
    public class CharacterInputMediator: IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly IMovable _movable;

        public CharacterInputMediator(IPlayerInput playerInput, IMovable movable)
        {
            _playerInput = playerInput;
            _movable = movable;

            _playerInput.horizontalInput += OnHorizontalInput;
        }

        public void Dispose()
        {
            _movable.Stop();
            _playerInput.horizontalInput -= OnHorizontalInput;
        }

        private void OnHorizontalInput(float direction) => _movable.Move(direction);
    }
}
