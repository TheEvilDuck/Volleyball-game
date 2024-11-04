using Common.PlayerInput;
using Common.States;
using Common.Tickables;
using EntryPoint;
using Gameplay.BallDetection;
using Gameplay.Mediators;
using Gameplay.States;
using UnityEngine;

namespace Gameplay
{
    public class Bootstrap : MonoBehaviourBootstrap
    {
        public const string GAMEPLAY_TICKABLES_TAG = nameof(GAMEPLAY_TICKABLES_TAG);

        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private BallCollider _floor;
        protected override void SetupInternal()
        {
            _sceneContext.Register(SetupSceneTickables, GAMEPLAY_TICKABLES_TAG);
            _sceneContext.Register(SetupInput);
            _sceneContext.Register(SetupBall);
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
            stateMachine.AddState(new MainGameState(stateMachine, _floor));
            stateMachine.AddState(new RoundEndState(stateMachine));

            _sceneContext.Get<TickablesContainer>(GAMEPLAY_TICKABLES_TAG).Register(stateMachine);

            _sceneDisposables.Register(stateMachine);

            return stateMachine;
        }

        private void SetupMediators()
        {
            TestInputMediator testInputMediator = new TestInputMediator(_sceneContext);

            _sceneDisposables.Register(testInputMediator);
        }
    }
}
