using DI;
using UnityEngine;

namespace EntryPoint
{
    public abstract class MonoBehaviourBootstrap: MonoBehaviour
    {
        protected DIContainer _sceneContext;

        public void Setup(IDIContainer projectContext)
        {
            _sceneContext = new DIContainer(projectContext);

            SetupInternal();
        }

        protected abstract void SetupInternal();
    }
}
