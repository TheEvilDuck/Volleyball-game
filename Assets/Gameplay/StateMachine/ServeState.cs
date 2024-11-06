using Common.PlayerInput;
using Common.States;
using Gameplay.Balls;
using Gameplay.Characters;
using Gameplay.Mediators;
using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.States
{
    public class ServeState : State
    {
        private readonly IBallFactory _ballFactory;
        private readonly IPlayerInput _playerInput;
        private IPositionProvider _ballPivot;
        private IPositionProvider _playerStaraPosition;
        private SeveArmsInputMediator _armsInputMediator;
        public ServeState(IStateMachine stateMachine, IBallFactory ballFactory, IPlayerInput playerInput, IPositionProvider playerStartPosition) : base(stateMachine)
        {
            _ballFactory = ballFactory;
            _playerInput = playerInput;
            _playerStaraPosition = playerStartPosition;
        }

        public override void Enter()
        {
            Character character = _stateMachine.StateMachineContext.Get<Character>();
            
            if (!_stateMachine.StateMachineContext.ContainsRegistration<IBall>())
                _stateMachine.StateMachineContext.Register<IBall>(() => _ballFactory.Get(character.BallPivot));

            _stateMachine.StateMachineContext.Get<IBall>().Freeze();
            _ballPivot = character.BallPivot;

            _playerInput.mouseClicked += OnMouseClicked;

            _armsInputMediator = new SeveArmsInputMediator(_playerInput, character);

            character.SetPosition(_playerStaraPosition);
        }

        public override void Update(float deltaTime)
        {
            _stateMachine.StateMachineContext.Get<IBall>().SetPosition(_ballPivot);
        }

        public override void Exit()
        {
            _armsInputMediator?.Dispose();
        }

        private void OnMouseClicked(Vector2 position)
        {
            Character character = _stateMachine.StateMachineContext.Get<Character>();
            _playerInput.mouseClicked -= OnMouseClicked;
            _stateMachine.StateMachineContext.Get<IBall>().Release();
            _stateMachine.StateMachineContext.Get<IBall>().AddImpulse(Vector2.up * character.CurrentAngularSpeed);
            _stateMachine.ChangeState<MainGameState>();
        }
    }
}