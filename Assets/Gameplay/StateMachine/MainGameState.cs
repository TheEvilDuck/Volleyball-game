using Common.PlayerInput;
using Common.States;
using Gameplay.BallDetection;
using Gameplay.Characters;
using Gameplay.Mediators;

namespace Gameplay.States
{
    public class MainGameState: State
    {
        private readonly IBallDetector _floor;
        private readonly IPlayerInput _playerInput;
        private CharacterInputMediator _characterInputMediator;

        public MainGameState(IStateMachine stateMachine, IBallDetector floor, IPlayerInput playerInput): base(stateMachine)
        {
            _floor = floor;
            _playerInput = playerInput;
        }
        public override void Enter()
        {
            Character character = _stateMachine.StateMachineContext.Get<Character>();
            _floor.detectionStarted += OnBallHitTheFloor;
            _characterInputMediator = new CharacterInputMediator(_playerInput, character);
        }

        public override void Exit()
        {
            _floor.detectionStarted -= OnBallHitTheFloor;
            _characterInputMediator?.Dispose();
        }

        private void OnBallHitTheFloor()
        {
            _stateMachine.ChangeState<RoundEndState>();
        }
    }
}
