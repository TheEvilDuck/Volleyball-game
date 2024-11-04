using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SimpleSceneManager : ISceneManager
    {
        public event Action beforeSceneChanged;

        public async Awaitable AddScene(string sceneId)
        {
            await SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        }

        public async Awaitable ChangeScene(string sceneId)
        {
            beforeSceneChanged?.Invoke();

            await SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);
        }
    }
}
