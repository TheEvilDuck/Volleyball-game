using Common.PlayerInput;
using Common.States;
using Gameplay.Characters;
using Gameplay.Maps;

namespace Gameplay.States
{
    public class MainGameState: State
    {
        private readonly IMapState _mapState;

        public MainGameState(IStateMachine stateMachine, IMapState mapState): base(stateMachine)
        {
            _mapState = mapState;
        }
        public override void Enter()
        {
            _mapState.floorHit += OnBallHitTheFloor;
            _mapState.ballOut += OnBallOut;
        }

        public override void Exit()
        {
            _mapState.floorHit -= OnBallHitTheFloor;
            _mapState.ballOut -= OnBallOut;
        }

        private void OnBallHitTheFloor()
        {
            _stateMachine.ChangeState<RoundEndState>();
        }

        private void OnBallOut()
        {
            _stateMachine.ChangeState<RoundEndState>();
        }
    }
}
