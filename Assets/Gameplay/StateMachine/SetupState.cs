using Common.States;
using Gameplay.Characters;
using Gameplay.PositionProviding;

namespace Gameplay.States
{
    public class SetupState : State
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly IPositionProvider _characterStartPosition;

        public SetupState(IStateMachine stateMachine, ICharacterFactory characterFactory, IPositionProvider characterStartPosition) : base(stateMachine)
        {
            _characterFactory = characterFactory;
            _characterStartPosition = characterStartPosition;
        }

        public override void Enter()
        {
            _stateMachine.StateMachineContext.Register(() => new CharacterProvider(_characterFactory, _characterStartPosition));
            _stateMachine.StateMachineContext.Register<ICharacterProvider>(() => _stateMachine.StateMachineContext.Get<CharacterProvider>());

            _stateMachine.ChangeState<ServeState>();
        }
    }
}
