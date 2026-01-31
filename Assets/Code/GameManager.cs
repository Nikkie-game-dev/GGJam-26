using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Code.Service;
using Systems.CentralizeEventSystem;
using UnityEngine;

namespace Assets.Code.Manager
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
