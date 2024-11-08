using Common.PlayerInput;
using Common.States;
using Gameplay.BallDetection;
using Gameplay.Characters;
using Gameplay.Maps;
using Gameplay.Mediators;

namespace Gameplay.States
{
    public class MainGameState: State
    {
        private readonly IPlayerInput _playerInput;
        private readonly IMapState _mapState;
        private CharacterInputMediator _characterInputMediator;
        private MainGameArmsInputMediator _mainGameArmsInputMediator;

        public MainGameState(IStateMachine stateMachine, IMapState mapState, IPlayerInput playerInput): base(stateMachine)
        {
            _mapState = mapState;
            _playerInput = playerInput;
        }
        public override void Enter()
        {
            Character character = _stateMachine.StateMachineContext.Get<ICharacterProvider>().Character;
            _mapState.floorHit += OnBallHitTheFloor;
            _mapState.ballOut += OnBallOut;

            _characterInputMediator = new CharacterInputMediator(_playerInput, character);
            _mainGameArmsInputMediator = new MainGameArmsInputMediator(_playerInput, character);
        }

        public override void Exit()
        {
            _mapState.floorHit -= OnBallHitTheFloor;
            _mapState.ballOut -= OnBallOut;
            _characterInputMediator?.Dispose();
            _mainGameArmsInputMediator?.Dispose();
        }

        private void OnBallHitTheFloor()
        {
            _stateMachine.ChangeState<RoundEndState>();
        }

        private void OnBallOut()
        {

        }
    }
}
