using Code.Service;
using Code.InputMG;
using Systems.CentralizeEventSystem;
using UnityEngine;
using Code.SceneManagerController;

namespace Code.Manager
{
    public sealed class GameManager : MonoBehaviour
    {
        SceneLoader sceneLoader;

        private void Awake()
        {
            ServiceProvider.Instance.AddService<InputManager>(new InputManager());
            ServiceProvider.Instance.AddService<CentralizeEventSystem>(new CentralizeEventSystem());

            sceneLoader = new SceneLoader();
        }
    }
}
