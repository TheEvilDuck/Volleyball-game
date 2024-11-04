using System;
using UnityEngine;

namespace SceneManagement
{
    public interface ISceneManager
    {
        public event Action beforeSceneChanged;
        public Awaitable ChangeScene(string sceneId);
        public Awaitable AddScene(string sceneId);
    }
}
