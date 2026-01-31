using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.SceneManagerController
{
    public class SceneLoader
    {
#if UNITY_EDITOR
        [SerializeField] private SceneRef exclude;
#endif

        public SceneLoader()
        {
        }

        private readonly List<Scene> _activeScenes = new();


        private void StartLoading(SceneRef sceneRef, LoadSceneMode mode)
        {
            try
            {
                SceneManager.LoadScene(sceneRef.Index, mode);

                Scene loadedScene = SceneManager.GetSceneByBuildIndex(sceneRef.Index);

                if (loadedScene.IsValid() && loadedScene.isLoaded)
                    _activeScenes.Add(loadedScene);
                else
                    throw new Exception($"The Scene with name {sceneRef.Name} didn't load correctly.");
            }
            catch (Exception e)
            {
                Debug.LogError($"{sceneRef.Name} {{{sceneRef.Index}}}: is not on the loading list");

                Debug.LogException(e);
            }
        }

        private void StartUnloading(Scene activeScenes)
        {
            SceneManager.UnloadSceneAsync(activeScenes);
        }

        /// <inheritdoc/>
        public void LoadScene(SceneRef sceneRef, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if (sceneRef == null)
            {
                Debug.LogWarning("No SceneRef assigned");
                return;
            }

            if (sceneRef.Index < 0)
            {
                Debug.LogWarning($"Not Valid SceneRef. Cause SceneIndex: {sceneRef.Name} is < 0");
                return;
            }

            if (IsSceneLoaded(sceneRef))
            {
                Debug.LogWarning($"Scene: {sceneRef.Name} is already loaded.");
                return;
            }

            StartLoading(sceneRef, mode);
        }

        /// <inheritdoc/>
        public void LoadSceneAsync(params SceneRef[] sceneRef)
        {
            foreach (SceneRef t in sceneRef)
               LoadScene(t);
        }

        /// <inheritdoc/>
        public void UnloadAll()
        {
            for (int i = _activeScenes.Count - 1; i >= 0; i--)
            {
                Scene scene = _activeScenes[i];

                _activeScenes.RemoveAt(i);
                StartUnloading(scene);
            }
        }

        /// <inheritdoc/>
        public void UnloadAll(SceneRef exception)
        {
            for (int i = _activeScenes.Count - 1; i >= 0; i--)
            {
                if (_activeScenes[i].buildIndex == exception.Index)
                    continue;

                StartUnloading(_activeScenes[i]);
                _activeScenes.RemoveAt(i);
            }
        }

        public void UnloadAll(SceneRef[] exeptions)
        {
            for (int i = _activeScenes.Count - 1; i >= 0; i--)
            {
                if (IsSceneInExceptionArray(_activeScenes[i].buildIndex, exeptions))
                    continue;

                StartUnloading(_activeScenes[i]);
                _activeScenes.RemoveAt(i);
            }
        }

        private static bool IsSceneInExceptionArray(int index, SceneRef[] sceneRefs) => sceneRefs.Any(t => index == t.Index);

        public bool IsSceneLoaded(SceneRef sceneRef) => _activeScenes.Any(t => t.buildIndex == sceneRef.Index);
    }
}