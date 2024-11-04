using Common.States;
using Gameplay.Characters;
using Gameplay.StartPositions;

namespace Gameplay.States
{
    public class SetupState : State
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly IStartPosition _characterStartPosition;

        public SetupState(IStateMachine stateMachine, ICharacterFactory characterFactory, IStartPosition characterStartPosition) : base(stateMachine)
        {
            _characterFactory = characterFactory;
            _characterStartPosition = characterStartPosition;
        }

        public override void Enter()
        {
            Character character = _characterFactory.Get(_characterStartPosition);
            _stateMachine.StateMachineContext.Register(() => character);

            _stateMachine.ChangeState<ServeState>();
        }
    }
}
