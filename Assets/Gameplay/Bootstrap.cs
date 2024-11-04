using EntryPoint;
using UnityEngine;

namespace Gameplay
{
    public class Bootstrap : MonoBehaviourBootstrap
    {
        [SerializeField] private Transform _ballSpawnPoint;
        protected override void SetupInternal()
        {
            _sceneContext.Register(SetupBall).NonLazy();
        }

        private IBall SetupBall()
        {
            Ball prefab = Resources.Load<Ball>("Prefabs/Ball");
            Ball instance = Instantiate(prefab, _ballSpawnPoint.position, Quaternion.identity);
            return instance;
        }
    }
}
