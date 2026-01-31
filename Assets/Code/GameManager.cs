using Assets.Code.Service;
using Code.InputMG;
using Systems.CentralizeEventSystem;
using UnityEngine;

namespace Assets.Code.Manager
{
    public sealed class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            ServiceProvider.Instance.AddService<InputManager>(new InputManager());
            ServiceProvider.Instance.AddService<InputManager>(new CentralizeEventSystem());
        }
    }
}
