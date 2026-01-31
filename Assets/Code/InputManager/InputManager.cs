using Assets.Code.Service;
using UnityEngine;

public sealed class InputManager : IService
{
    private InputSystem_Actions inputSystem;

    public InputManager()
    {
        inputSystem = new InputSystem_Actions();
        inputSystem.Player.Enable();
    }

    bool IService.IsPersistance => true;

    public InputSystem_Actions InputSystem => inputSystem;
}
