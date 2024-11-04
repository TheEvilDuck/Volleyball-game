using Common.States;
using Gameplay.BallDetection;

namespace Gameplay.States
{
    public class MainGameState: State
    {
        private readonly IBallDetector _floor;

        public MainGameState(IStateMachine stateMachine, IBallDetector floor): base(stateMachine)
        {
            _floor = floor;
        }
        public override void Enter()
        {
            _floor.detectionStarted += OnBallHitTheFloor;
        }

        public override void Exit()
        {
            _floor.detectionStarted -= OnBallHitTheFloor;
        }

        private void OnBallHitTheFloor()
        {
            _stateMachine.ChangeState<RoundEndState>();
        }
    }
}
