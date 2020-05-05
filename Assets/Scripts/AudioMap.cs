using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMap : MonoBehaviour
{
    private static AudioMap _instance;
    public AudioSource audioSource;
    public static AudioMap Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioMap>();
            }

            return _instance;
        }
    }
    public void PlayLoop(){
      audioSource.Play();
    }

    public void StopLoop(){
      audioSource.Stop();
    }
}
