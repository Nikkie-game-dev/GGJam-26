using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.ServiceProvider
{
    internal class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            ServiceProvider.Instance.AddService<InputManager>(new InputManager());
        }
    }
}
