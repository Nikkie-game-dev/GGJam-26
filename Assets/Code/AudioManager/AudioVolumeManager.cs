using UnityEngine;
using UnityEngine.Audio;

// This class handles the functions to adjust the game sounds volume. Use it with sliders > Unity Events > Dynamic floats
namespace Code.AudioManager
{
    public class AudioVolumeManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;

        public void MasterVolumeChanged(float value)
        {
            if (value < 0.05f)
            {
                _audioMixer.SetFloat("master", -80f);
                return;
            }
            _audioMixer.SetFloat("master", Mathf.Log10(value) * 20);
        }
        public void SFXVolumeChanged(float value)
        {
            if (value < 0.05f)
            {
                _audioMixer.SetFloat("SFX", -80f);
                return;
            }
            _audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        }
        public void MusicVolumeChanged(float value)
        {   
            if (value < 0.05f)
            {
                _audioMixer.SetFloat("music", -80f);
                return;
            }
            _audioMixer.SetFloat("music", Mathf.Log10(value) * 20);
        }
    }
}
