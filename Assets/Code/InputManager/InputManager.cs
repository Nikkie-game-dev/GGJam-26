using Assets.Code.ServiceProvider;
using UnityEngine;

public class InputManager : IService
{
    private InputSystem_Actions inputSystem;

    public InputManager()
    {
        inputSystem = new InputSystem_Actions();
    }

    bool IService.IsPersistance => true;

    public InputSystem_Actions InputSystem => inputSystem;
}
