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
        private readonly IPlayerInput _playerInput;
        public ServeState(IStateMachine stateMachine, IPlayerInput playerInput) : base(stateMachine)
        {
            _playerInput = playerInput;
        }

        public override void Enter()
        {
            _playerInput.mouseClicked += OnMouseClicked;

            IGameState gameState = _stateMachine.StateMachineContext.Get<IGameState>();
            gameState.ResetInnerState();
            gameState.SwitchTeams();

            foreach (ICharacterProvider characterProvider in gameState.AllCharacters)
            {
                characterProvider.Controller.SetCanBending(false);
                characterProvider.Controller.SetCanMove(false);
                characterProvider.Controller.SetCanRotateArm(false);
            }

            gameState.CurrentTeam.CurrentServer.Controller.SetCanRotateArm(true);

            _stateMachine.StateMachineContext.Get<IBall>().Freeze();
        }

        public override void Update(float deltaTime)
        {
            IGameState gameState = _stateMachine.StateMachineContext.Get<IGameState>();
            _stateMachine.StateMachineContext.Get<IBall>().SetPosition(gameState.CurrentTeam.CurrentServer.Character.BallPivot);
        }

        public override void Exit()
        {
            _stateMachine.StateMachineContext.Get<IBall>().Release();
        }

        private void OnMouseClicked(Vector2 position)
        {
            _playerInput.mouseClicked -= OnMouseClicked;
            _stateMachine.ChangeState<MainGameState>();
        }
    }
}