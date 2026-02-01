using Code.Service;
using Code.InputMG;
using Systems.CentralizeEventSystem;
using UnityEngine;

namespace Code.Manager
{
    public sealed class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            ServiceProvider.Instance.AddService<InputManager>(new InputManager());
            ServiceProvider.Instance.AddService<CentralizeEventSystem>(new CentralizeEventSystem());
        }
    }
}
