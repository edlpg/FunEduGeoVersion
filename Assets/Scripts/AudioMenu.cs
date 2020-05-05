using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
  private static AudioMenu _instance;
  public AudioSource audioSource;
  public AudioClip celebration;
  public static AudioMenu Instance
  {
      get
      {
          if (_instance == null)
          {
              _instance = GameObject.FindObjectOfType<AudioMenu>();
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
  public void PlayCelebration()
  {
     audioSource.PlayOneShot(celebration,1f);
  }
}
