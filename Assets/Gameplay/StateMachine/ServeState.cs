using Common.States;
using Gameplay.Balls;
using Gameplay.Characters;
using Gameplay.StartPositions;

namespace Gameplay.States
{
    public class ServeState : State
    {
        private readonly IBallFactory _ballFactory;
        private IStartPosition _ballPivot;
        private IBall _ball;

        public ServeState(IStateMachine stateMachine, IBallFactory ballFactory) : base(stateMachine)
        {
            _ballFactory = ballFactory;
        }

        public override void Enter()
        {
            Character character = _stateMachine.StateMachineContext.Get<Character>();
            _ball = _ballFactory.Get(character.BallPivot);
            _ball.Freeze();
            _ballPivot = character.BallPivot;
            _stateMachine.StateMachineContext.Register(() => _ball);
        }

        public override void Update(float deltaTime)
        {
            _ball.SetPosition(_ballPivot);
        }

        public override void Exit()
        {
            _ball.Release();
        }
    }
}