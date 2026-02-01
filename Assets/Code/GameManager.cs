using Code.Service;
using Code.InputMG;
using Systems.CentralizeEventSystem;
using UnityEngine;
using Code.SceneManagerController;

namespace Code.Manager
{
    public delegate void LoadGamplayScene();
     public delegate void LoadMainManuScene();
    public sealed class GameManager : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [SerializeField] SceneRef _mainmenuScene;
        [SerializeField] SceneRef _gameplayScene;

        private void Awake()
        {
            ServiceProvider.Instance.AddService<InputManager>(new InputManager());
            ServiceProvider.Instance.AddService<CentralizeEventSystem>(new CentralizeEventSystem());

            sceneLoader = new SceneLoader();

            ServiceProvider.Instance.GetService<CentralizeEventSystem>().AddListener<LoadGamplayScene>(OnLoadGameplay);
            ServiceProvider.Instance.GetService<CentralizeEventSystem>().AddListener<LoadMainManuScene>(OnLoadMainMenu);
        }

        private void Start()
        {
            OnLoadMainMenu();
        }

        private void OnLoadGameplay()
        {
            sceneLoader.UnloadAll();
            sceneLoader.LoadScene(_gameplayScene);
        }

        private void OnLoadMainMenu()
        {
            sceneLoader.UnloadAll();
            sceneLoader.LoadScene(_mainmenuScene);
        }
    }
}
