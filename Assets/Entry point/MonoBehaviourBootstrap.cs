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
        private CompositeDisposable _projectDisposables;

        public void Setup(IDIContainer projectContext, CompositeDisposable projectDisposables)
        {
            _sceneContext = new DIContainer(projectContext);
            _projectDisposables = projectDisposables;
            _sceneContext.Get<ISceneManager>().beforeSceneChanged += Clean;
            _sceneDisposables = new CompositeDisposable();
            _projectDisposables.Register(_sceneDisposables);

            SetupInternal();
        }

        private void Clean()
        {
            _sceneContext.Get<ISceneManager>().beforeSceneChanged -= Clean;

            CleanInternal();

            _projectDisposables.Remove(_sceneDisposables);
            _sceneDisposables?.Dispose();
        }

        protected abstract void SetupInternal();
        protected virtual void CleanInternal() {}
    }
}
