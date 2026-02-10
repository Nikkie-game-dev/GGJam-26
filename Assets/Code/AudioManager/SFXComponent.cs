using System.Collections.Generic;
using Code.Service;
using UnityEngine;

public class SFXComponent : MonoBehaviour
{
    [SerializeField] 
    private List<AudioData> _audioData;

    private GlobalAudioManager _globalAudioManager => ServiceProvider.Instance.GetService<GlobalAudioManager>();
    

    public void PlaySFX(string audioName)
    {
        foreach (AudioData ind in _audioData)
        {
            if (ind.Name == audioName)
            {
                _globalAudioManager.PlayAudio(ind);
                return;
            }
        }
       
            Debug.LogWarning("Revisa el nombre del audio");
        
    }
}
