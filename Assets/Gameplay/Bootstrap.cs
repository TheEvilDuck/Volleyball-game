using Common.PlayerInput;
using Common.States;
using Common.Tickables;
using EntryPoint;
using Gameplay.BallDetection;
using Gameplay.Balls;
using Gameplay.Characters;
using Gameplay.StartPositions;
using Gameplay.States;
using UnityEngine;

namespace Gameplay
{
    public class Bootstrap: MonoBehaviourBootstrap
    {
        public const string GAMEPLAY_TICKABLES_TAG = nameof(GAMEPLAY_TICKABLES_TAG);

        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private BallCollider _floor;
        [SerializeField] private StartPosition _playerStartPosition;
        [SerializeField] private StartPosition _ballStartPosition;
        protected override void SetupInternal()
        {
            _sceneContext.Register(SetupSceneTickables, GAMEPLAY_TICKABLES_TAG);
            _sceneContext.Register(SetupInput);
            _sceneContext.Register(SetupBall);
            _sceneContext.Register(SetupCharacterFactory);
            _sceneContext.Register(SetupBallFactory);
            _sceneContext.Register(SetupGameplayStateMachine).NonLazy();

            SetupMediators();
        }

        protected override void CleanInternal()
        {
            TickablesContainer tickables = _sceneContext.Get<TickablesContainer>(GAMEPLAY_TICKABLES_TAG);
            _sceneContext.Get<TickablesContainer>(EntryPoint.Bootstrap.PROJECT_TICKABLES_TAG).RemoveTickable(tickables);
        }

        private IBall SetupBall()
        {
            Ball prefab = Resources.Load<Ball>("Prefabs/Ball");
            Ball instance = Instantiate(prefab, _ballSpawnPoint.position, Quaternion.identity);
            return instance;
        }

        private TickablesContainer SetupSceneTickables()
        {
            TickablesContainer tickables = new TickablesContainer();
            _sceneContext.Get<TickablesContainer>(EntryPoint.Bootstrap.PROJECT_TICKABLES_TAG).Register(tickables);
            return tickables;
        }

        private IPlayerInput SetupInput()
        {
            DesktopInput playerInput = new DesktopInput();
            TickablesContainer tickables = _sceneContext.Get<TickablesContainer>(GAMEPLAY_TICKABLES_TAG);
            tickables.Register(playerInput);

            return playerInput;
        }

        private StateMachine SetupGameplayStateMachine()
        {
            StateMachine stateMachine = new StateMachine();
            IPlayerInput playerInput = _sceneContext.Get<IPlayerInput>();
            stateMachine.AddState(new SetupState(stateMachine, _sceneContext.Get<ICharacterFactory>(), _playerStartPosition));
            stateMachine.AddState(new ServeState(stateMachine, _sceneContext.Get<IBallFactory>(), playerInput));
            stateMachine.AddState(new MainGameState(stateMachine, _floor, playerInput));
            stateMachine.AddState(new RoundEndState(stateMachine));

            _sceneContext.Get<TickablesContainer>(GAMEPLAY_TICKABLES_TAG).Register(stateMachine);

            _sceneDisposables.Register(stateMachine);

            stateMachine.ChangeState<SetupState>();

            return stateMachine;
        }

        private ICharacterFactory SetupCharacterFactory()
        {
            CharacterFactorySO characterFactorySO = Resources.Load<CharacterFactorySO>("Character factory");
            return characterFactorySO;
        }

        private IBallFactory SetupBallFactory()
        {
            BallFactorySO ballFactorySO = Resources.Load<BallFactorySO>("Balls factory");
            return ballFactorySO;
        }

        private void SetupMediators()
        {

        }
    }
}
