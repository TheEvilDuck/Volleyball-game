using Common.Disposables;
using Common.PlayerInput;
using Common.Tickables;
using EntryPoint;
using Gameplay.Mediators;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{
    public class Bootstrap : MonoBehaviourBootstrap
    {
        public const string GAMEPLAY_TICKABLES_TAG = nameof(GAMEPLAY_TICKABLES_TAG);

        [SerializeField] private Transform _ballSpawnPoint;
        protected override void SetupInternal()
        {
            _sceneContext.Register(SetupSceneTickables, GAMEPLAY_TICKABLES_TAG);
            _sceneContext.Register(SetupInput);
            _sceneContext.Register(SetupBall);

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

        private void SetupMediators()
        {
            TestInputMediator testInputMediator = new TestInputMediator(_sceneContext);

            _sceneDisposables.Register(testInputMediator);
        }
    }
}
