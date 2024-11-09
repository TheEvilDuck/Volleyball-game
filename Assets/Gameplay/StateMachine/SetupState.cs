using Common.PlayerInput;
using Common.States;
using Gameplay.Balls;
using Gameplay.Characters;
using Gameplay.GameStates;
using Gameplay.Maps;
using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.States
{
    public class SetupState : State
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly Map _map;
        private readonly IPlayerInput _playerInput;
        private readonly IBallFactory _ballFactory;

        public SetupState(IStateMachine stateMachine, ICharacterFactory characterFactory, IBallFactory  ballFactory, Map map, IPlayerInput playerInput) : base(stateMachine)
        {
            _characterFactory = characterFactory;
            _map = map;
            _ballFactory = ballFactory;
            _playerInput = playerInput;
        }

        public override void Enter()
        {
            _stateMachine.StateMachineContext.Register<IMapInfo>(_map);
            _stateMachine.StateMachineContext.Register<IMapState>(_map);
            _stateMachine.StateMachineContext.Register(SetupGameState);
            _stateMachine.StateMachineContext.Register(SetupBall);

            IGameState gameState = _stateMachine.StateMachineContext.Get<IGameState>();

            foreach (ICharacterProvider characterProvider in gameState.Team1.Characters)
                characterProvider.SetController(new PlayerInputCharacterController(_playerInput));

            foreach (ICharacterProvider characterProvider in gameState.Team2.Characters)
                characterProvider.SetController(new PlayerInputCharacterController(_playerInput));

            _stateMachine.ChangeState<ServeState>();
        }

        private IGameState SetupGameState()
        {
            GameState gameState = new GameState(_characterFactory, _map, _playerInput);
            return gameState;
        }

        private IBall SetupBall()
        {
            IBall ball = _ballFactory.Get(new ConstantPositionProvider(Vector2.zero, Quaternion.identity));
            return ball;
        }
    }
}
