using Common.PlayerInput;
using Common.States;
using Gameplay.Balls;
using Gameplay.Characters;
using Gameplay.StartPositions;
using UnityEngine;

namespace Gameplay.States
{
    public class ServeState : State
    {
        private readonly IBallFactory _ballFactory;
        private readonly IPlayerInput _playerInput;
        private IStartPosition _ballPivot;

        public ServeState(IStateMachine stateMachine, IBallFactory ballFactory, IPlayerInput playerInput) : base(stateMachine)
        {
            _ballFactory = ballFactory;
            _playerInput = playerInput;
        }

        public override void Enter()
        {
            Character character = _stateMachine.StateMachineContext.Get<Character>();
            
            if (!_stateMachine.StateMachineContext.ContainsRegistration<IBall>())
                _stateMachine.StateMachineContext.Register<IBall>(() => _ballFactory.Get(character.BallPivot));

            _stateMachine.StateMachineContext.Get<IBall>().Freeze();
            _ballPivot = character.BallPivot;

            _playerInput.mouseClicked += OnMouseClicked;
        }

        public override void Update(float deltaTime)
        {
            _stateMachine.StateMachineContext.Get<IBall>().SetPosition(_ballPivot);
        }

        private void OnMouseClicked(Vector2 position)
        {
            _playerInput.mouseClicked -= OnMouseClicked;
            Vector2 direction = _ballPivot.Position - position;
            _stateMachine.StateMachineContext.Get<IBall>().Release();
            _stateMachine.StateMachineContext.Get<IBall>().AddImpulse(direction.normalized * 25f);
            _stateMachine.ChangeState<MainGameState>();
        }
    }
}