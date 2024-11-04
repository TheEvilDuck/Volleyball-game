using System.Collections;
using Common;
using Common.Disposables;
using Common.LoadingScreen;
using Common.Tickables;
using DI;
using SceneManagement;
using UnityEngine;

namespace EntryPoint
{
    public sealed class Bootstrap
    {
        public const string PROJECT_TICKABLES_TAG = nameof(PROJECT_TICKABLES_TAG);
        public const string PERSISTANT_CANVAS_TAG = nameof(PERSISTANT_CANVAS_TAG);

        private static Bootstrap _instance;
        DIContainer _projectContext;
        CompositeDisposable _projectDisposables;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void EntryPoint()
        {
            Application.targetFrameRate = 60;

            _instance = new Bootstrap();
            _instance.Run();
        }

        private void Run()
        {
            _projectContext = new DIContainer();
            _projectDisposables = new CompositeDisposable();

            Application.quitting += OnApplicationQuit;

            _projectContext.Register(SetupCoroutines);
            _projectContext.Register(SetupTickables, PROJECT_TICKABLES_TAG);
            _projectContext.Register(SetupLoadingScreen);
            _projectContext.Register(SetupPersistantCanvas, PERSISTANT_CANVAS_TAG);
            _projectContext.Register(SetupSceneManager);

            SetupScene();
        }

        private void OnApplicationQuit()
        {
            Application.quitting -= OnApplicationQuit;
            _projectDisposables?.Dispose();
        }

        private Coroutines SetupCoroutines()
        {
            GameObject coroutinesGameObject = new GameObject("COROUTINES");
            GameObject.DontDestroyOnLoad(coroutinesGameObject);
            return coroutinesGameObject.AddComponent<Coroutines>();
        }

        private TickablesContainer SetupTickables()
        {
            TickablesContainer tickablesContainer = new TickablesContainer();
            Coroutines coroutines = _projectContext.Get<Coroutines>();

            IEnumerator TickTickables()
            {
                while (true)
                {
                    tickablesContainer?.Tick(Time.deltaTime);
                    yield return null;
                }
            }

            coroutines.StartCoroutine(TickTickables());

            return tickablesContainer;
        }

        private ILoadingScreen SetupLoadingScreen()
        {
            LoadingScreenObject prefab = Resources.Load<LoadingScreenObject>("Prefabs/Loading screen object");
            Canvas persistantCanvas = _projectContext.Get<Canvas>(PERSISTANT_CANVAS_TAG);
            LoadingScreenObject instance = GameObject.Instantiate(prefab, persistantCanvas.transform);
            GameObject.DontDestroyOnLoad(instance.gameObject);
            instance.Hide();

            return instance;
        }

        private Canvas SetupPersistantCanvas()
        {
            Canvas prefab = Resources.Load<Canvas>("Prefabs/PersistantCanvas");
            Canvas instance = GameObject.Instantiate(prefab);
            GameObject.DontDestroyOnLoad(instance.gameObject);

            return instance;
        }

        private ISceneManager SetupSceneManager()
        {
            ISceneManager sceneManager = new SimpleSceneManager();
            return sceneManager;
        }

        private async void SetupScene()
        {
            MonoBehaviourBootstrap sceneBootstrap = GameObject.FindAnyObjectByType<MonoBehaviourBootstrap>();

            if (sceneBootstrap == null)
            {
                await _projectContext.Get<ISceneManager>().ChangeScene(SceneIDs.GAMEPLAY);
                SetupScene();
                return;
            }

            sceneBootstrap.Setup(_projectContext, _projectDisposables);
        }
    }
}
