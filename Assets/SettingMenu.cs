using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour {

    public AudioMixer audioMixer;

   public void SetVolume (float volume)
   {
        audioMixer.SetFloat("Volume", volume);  
   }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

}
