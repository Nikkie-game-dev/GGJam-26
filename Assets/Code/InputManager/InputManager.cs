using Code.Service;

namespace Code.InputMG
{
    public sealed class InputManager : IService
    {
        private InputSystem_Actions _inputSystem;

        public InputManager()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Enable();
        }

        bool IService.IsPersistance => true;

        public InputSystem_Actions InputSystem => _inputSystem;
    }
}
