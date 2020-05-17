using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    public AudioSource MenuMusic;

    public void OnClick()
    {
        if (MenuMusic.volume > 0f)
        {
            MenuMusic.volume = 0f;
            PlayerPrefs.SetFloat("MusicVolume", 0f);
        }
        else
        {
            MenuMusic.volume = 0.8f;
            PlayerPrefs.SetFloat("MusicVolume", 0.8f);
        }
    }
}
