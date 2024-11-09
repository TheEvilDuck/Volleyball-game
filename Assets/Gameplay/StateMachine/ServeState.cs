using Common.PlayerInput;
using Common.States;
using Gameplay.Balls;
using Gameplay.Characters;
using Gameplay.GameStates;
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
        public ServeState(IStateMachine stateMachine, IBallFactory ballFactory, IPlayerInput playerInput, IPositionProvider playerStartPosition) : base(stateMachine)
        {
            _ballFactory = ballFactory;
            _playerInput = playerInput;
            _playerStaraPosition = playerStartPosition;
        }

        public override void Enter()
        {
            _playerInput.mouseClicked += OnMouseClicked;

            IGameState gameState = _stateMachine.StateMachineContext.Get<IGameState>();
            gameState.SwitchTeams();

            foreach (ICharacterProvider characterProvider in gameState.OtherTeam.Characters)
            {
                characterProvider.Controller.SetCanBending(true);
                characterProvider.Controller.SetCanMove(true);
                characterProvider.Controller.SetCanRotateArm(true);
            }
        }

        public override void Update(float deltaTime)
        {
            
        }

        public override void Exit()
        {
            
        }

        private void OnMouseClicked(Vector2 position)
        {
            _playerInput.mouseClicked -= OnMouseClicked;
            _stateMachine.ChangeState<MainGameState>();
        }
    }
}