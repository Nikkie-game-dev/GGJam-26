using Code.InputMG;
using Code.Manager;
using Code.Service;
using Systems.CentralizeEventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject settings;
        [SerializeField] private GameObject pause;
        
        private float _originalTimeScale = 1;
        private bool _isPaused;
        
        private void Start()
        {
            ServiceProvider.Instance.GetService<InputManager>().InputSystem.UI.Pause.started += PauseInput;
        }

        private void PauseInput(InputAction.CallbackContext _)
        {
            if(_isPaused)
                Pause();
            else
                Unpause();
        }

        private void Pause()
        {
            PauseTime();
            pause.SetActive(true);
            
            //if cursor is not visible or lock state is not none
            /*Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;*/

            _isPaused = true;
        }

        private void PauseTime()
        {
            _originalTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            UnpauseTime();
            pause.SetActive(false);
            
            //restore cursor state

            _isPaused = false;
        }

        private void UnpauseTime()
        {
            Time.timeScale = _originalTimeScale;
        }

        public void OpenSettings()
        {
            settings.SetActive(true);
            pause.SetActive(false);
        }


        public void GoToMainMenu()
        {
            UnpauseTime();
            ServiceProvider.Instance.GetService<CentralizeEventSystem>().Get<LoadMainManuScene>()?.Invoke();
        }

        public void ReturnToPause()
        {
            settings.SetActive(false);
            pause.SetActive(true);
        }
    }
}