using System;
using Code.InputMG;
using Code.Service;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        private float _originalTimeScale = 1;
        private bool _isPaused = false;
        
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
            _originalTimeScale = Time.timeScale;
            Time.timeScale = 0;
            gameObject.SetActive(true);
            
            //if cursor is not visible or lock state is not none
            /*Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;*/

            _isPaused = true;
        }

        public void Unpause()
        {
            Time.timeScale = _originalTimeScale;
            gameObject.SetActive(false);
            
            //restore cursor state

            _isPaused = false;
        }
        
        public void OpenSettings() => throw new NotImplementedException();
        public void GoToMainMenu() => throw new NotImplementedException();
    }
}