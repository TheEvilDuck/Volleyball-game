using System;
using Common.PlayerInput;
using DI;
using UnityEngine;

namespace Gameplay.Mediators
{
    public class TestInputMediator : IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly IBall _ball;

        public TestInputMediator(IDIContainer context)
        {
            _playerInput = context.Get<IPlayerInput>();
            _ball = context.Get<IBall>();

            _playerInput.jumpPressed += OnJumpInput;
        }

        public void Dispose()
        {
            _playerInput.jumpPressed -= OnJumpInput;
        }

        private void OnJumpInput()
        {
            _ball.AddImpulse(Vector2.up * 10f);
        }
    }
}
