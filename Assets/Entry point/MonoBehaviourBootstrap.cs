using Common.Disposables;
using DI;
using SceneManagement;
using UnityEngine;

namespace EntryPoint
{
    public abstract class MonoBehaviourBootstrap: MonoBehaviour
    {
        protected DIContainer _sceneContext;
        protected CompositeDisposable _sceneDisposables;

        public void Setup(IDIContainer projectContext)
        {
            _sceneContext = new DIContainer(projectContext);
            _sceneContext.Get<ISceneManager>().beforeSceneChanged += Clean;
            _sceneDisposables = new CompositeDisposable();

            SetupInternal();
        }

        private void Clean()
        {
            _sceneContext.Get<ISceneManager>().beforeSceneChanged -= Clean;

            CleanInternal();

            _sceneDisposables?.Dispose();
        }

        protected abstract void SetupInternal();
        protected virtual void CleanInternal() {}
    }
}
